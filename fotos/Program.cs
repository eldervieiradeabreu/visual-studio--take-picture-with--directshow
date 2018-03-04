using System;
using System.IO;
using System.Windows.Forms;

namespace fotos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            int tempo = 1;
            int qtde = 1;
            string path = ".\\fotos\\";
            string nome = "";


            foreach (string arg in args)
            {
                switch (arg?.Split('=')[0]?.ToLower() ?? "")
                {
                    case "tempo":
                        int.TryParse(arg.Split('=')[1], out tempo);
                        break;
                    case "quantidade":
                        int.TryParse(arg.Split('=')[1], out qtde);
                        break;
                    case "path":
                        path = arg?.Split('=')[1]?.ToString()??"";

                        if(!path.EndsWith(@"\")){
                            path += @"\";
                        }
                        break;
                    case "nome":
                        nome = arg?.Split('=')[1]?.ToString() ?? "";
                        break;
                }
            }


            try
            {
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(tempo, qtde, path, nome));
        }
    }
}
