using NamedPipeTest;
using System.Text;

namespace NP.Server
{
    internal class Program
    {
        private static ServerPipe serverPipe;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            serverPipe = new ServerPipe("abc", 1);
            serverPipe.ReadDataEvent += DataRecieved;
            serverPipe.PipeClosedEvent += PipeClosed;


            do
            {
                var text = Console.ReadLine();
                if (text != null)
                {
                    Task t = serverPipe.WriteString(text);
                }

            } while (true);



        }


        private static void DataRecieved(object? sender, PipeEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.m_pData, 0, e.m_nDataLen);
            Console.WriteLine("Got command from client: " + data);
        }

        private static void PipeClosed(object? sender, EventArgs e)
        {
            Console.WriteLine("* Server: Pipe has been closed");
        }
    }
}
