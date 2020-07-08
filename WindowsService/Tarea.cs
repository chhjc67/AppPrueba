using System;
using System.IO;
using System.Reflection;

namespace WindowsService
{
    public class Tarea
    {
        public static void Escribir(string mensaje)
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StreamWriter ws = new StreamWriter(dir + @"\Service.txt", true);
            ws.WriteLine(DateTime.Now.ToString() + ": " + mensaje);
            ws.Close();
        }
    }
}
