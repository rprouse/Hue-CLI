using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Bridge;
using Q42.HueApi.Models.Groups;
using Console = Colorful.Console;

namespace alteridem.hue.cli
{
    class HueController
    {
        string _key;
        ILocalHueClient _client;

        public async Task ListBridges()
        {
            Console.WriteLine("Found bridges...", Color.White);

            IBridgeLocator locator = new HttpBridgeLocator();
            IEnumerable<LocatedBridge> bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
            foreach(var bridge in bridges)
            {
                Console.WriteLine($"{bridge.BridgeId} - {bridge.IpAddress}", Color.Gray);
            }
        }

        public async Task<bool> Register(string ip = null)
        {
            return await ConnectToBridge(ip, false);
        }

        public async Task<bool> ConnectToBridge(string ip = null, bool loadKey = true)
        {
            IBridgeLocator locator = new HttpBridgeLocator();
            IEnumerable<LocatedBridge> bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
            LocatedBridge bridge = null;
            if (ip == null)
                bridge = bridges.FirstOrDefault();
            else
                bridge = bridges.FirstOrDefault(b => b.IpAddress == ip);

            if (bridge == null)
            {
                Console.WriteLine("Hue Bridge not found", Color.Red);
                return false;
            }

            var manager = new KeyManager(bridge.IpAddress);

            if (loadKey)
                _key = manager.LoadKey();

            if (_key == null)
            {
                _key = await Register(bridge);
                if (_key == null)
                    return false;

                if (!manager.SaveKey(_key))
                    return false;
            }

            _client = Initialize(bridge);
            return true;
        }

        async Task<string> Register(LocatedBridge bridge)
        {
            Console.WriteLine("Press the button on your bridge then press ENTER", Color.Green);
            Console.ReadLine();

            try
            {
                ILocalHueClient client = new LocalHueClient(bridge.IpAddress);
                return await client.RegisterAsync("Alteridem.Hue.CLI", Environment.MachineName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to register with the bridge.", Color.Red);
                Console.WriteLine(e.Message, Color.Red);
            }
            return null;
        }

        ILocalHueClient Initialize(LocatedBridge bridge)
        {
            var client = new LocalHueClient(bridge.IpAddress);
            client.Initialize(_key);
            return client;

        }

        public async Task ListLights()
        {
            Console.WriteLine("Found lights...", Color.White);
            var lights = await _client.GetLightsAsync();
            foreach (var light in lights)
            {
                Console.WriteLine($"{light.Id} - {light.Name} ({(light.State.On ? $"On {(light.State.Brightness * 100 / 255)}%" : "Off")})", Color.Gray);
            }
        }

        public LightCommand GetCommand(Options options)
        {
            var command = new LightCommand();
            if (options.On)
                command.On = true;
            else if (options.Off)
                command.On = false;

            if (options.Alert)
            {
                command.Alert = Alert.Multiple;
            }

            return command;
        }

        public IEnumerable<string> GetLights(Options options)
        {
            if (options.Light > 0)
                return new[] { options.Light.ToString() };

            return null;
        }

        public Task<HueResults> SendCommand(LightCommand command, IEnumerable<string> lights = null) =>
            _client.SendCommandAsync(command, lights);
    }
}
