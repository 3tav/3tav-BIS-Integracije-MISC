using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PripravljalecPrognozClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
            {
                Application.Run(new Form1());
                return;
            }


            Console.WriteLine("RUN: " + string.Join(" ", args));

            var method = string.Empty;
            var arg = string.Empty;

            try
            {
                method = args[0];
                arg = args[1];

                var f = new Form1();
                f.Init();
                f.DispatchMethod(method, arg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, string.Format("Napaka zagonu metode {0}", method));
            }

            Console.WriteLine("OK");
        }
    }
}
