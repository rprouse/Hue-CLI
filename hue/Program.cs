﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using CommandLine;
using Console = Colorful.Console;

namespace hue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Options options = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => options = o);

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
                }
            }
            finally
            {
                Wait(options);
            }
        }

        static void Wait(Options o)
        {
            if(o.Wait == true)
            {
                Console.WriteLine();
                Console.WriteLine("Press ENTER to Exit.", Color.Yellow);
                Console.ReadLine();
            }
        }
    }
}
