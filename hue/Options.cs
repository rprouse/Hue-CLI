using CommandLine;

namespace hue
{
    public class Options
    {
        [Option('b', "bridges", HelpText = "List bridges on the network")]
        public bool ListBridges { get; set; }

        [Option('r', "register", HelpText = "Register with a Hue Bridge. Registration usually happens automatically, you should only need to use to fix a broken registration.")]
        public bool Register { get; set; }

        [Option('i', "ip", Default = null, HelpText = "IP Address of the Hue Bridge. Will default to the first bridge found.")]
        public string IP { get; set; }

        [Option('w', "wait", HelpText = "Wait before exiting.")]
        public bool Wait { get; set; }

        [Option("lights", HelpText = "List lights")]
        public bool Lights { get; set; }
    }
}
