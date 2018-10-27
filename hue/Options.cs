using CommandLine;

namespace alteridem.hue.cli
{
    public class Options
    {

        [Option('a', "alert", HelpText = "Set an alert on the lights.")]
        public bool Alert { get; set; }

        [Option("bridges", HelpText = "List bridges on the network.")]
        public bool ListBridges { get; set; }

        [Option('b', "brightness", HelpText = "Set the brightness of a light, from 0 to 100 percent.")]
        public byte? Brightness { get; set; }

        [Option('i', "ip", Default = null, HelpText = "IP Address of the Hue Bridge. Will default to the first bridge found.")]
        public string IP { get; set; }

        [Option('l', "light", HelpText = "The light to perform an action on. If unset or 0, all lights.")]
        public uint Light { get; set; }

        [Option("lights", HelpText = "List lights.")]
        public bool Lights { get; set; }

        [Option('f', "off", HelpText = "Turn lights off.")]
        public bool Off { get; set; }

        [Option('o', "on", HelpText = "Turn lights on.")]
        public bool On { get; set; }

        [Option('r', "register", HelpText = "Register with a Hue Bridge. Registration usually happens automatically, you should only need to use to fix a broken registration.")]
        public bool Register { get; set; }

        [Option('w', "wait", HelpText = "Wait before exiting.")]
        public bool Wait { get; set; }
    }
}
