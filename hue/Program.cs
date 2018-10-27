using System;
using System.Drawing;
using System.Threading.Tasks;
using CommandLine;
using Q42.HueApi;
using Console = Colorful.Console;

namespace alteridem.hue.cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Options options = null;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => options = o);

            if (options == null)
                return;

            try
            {
                var controller = new HueController();

                if (options.ListBridges)
                {
                    await controller.ListBridges();
                    return;
                }

                if (options.Register)
                {
                    if (await controller.Register(options.IP) == false)
                        return;
                }
                else
                {
                    if (await controller.ConnectToBridge(options.IP) == false)
                        return;
                }

                if (options.Lights)
                {
                    await controller.ListLights();
                    return;
                }

                var command = controller.GetCommand(options);
                var lights = controller.GetLights(options);
                var result = await controller.SendCommand(command, lights);
            }
            finally
            {
                Wait(options);
            }
        }

        static void Wait(Options o)
        {
            if(o.Wait)
            {
                Console.WriteLine();
                Console.WriteLine("Press ENTER to Exit.", Color.Yellow);
                Console.ReadLine();
            }
        }
    }
}
