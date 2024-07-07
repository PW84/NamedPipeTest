using NamedPipeTest;
using System.Text;

namespace NP.Client
{
    internal class Program
    {
        private static ClientPipe clientPipe;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            clientPipe = new ClientPipe(".", "abc");
            clientPipe.ReadDataEvent += DataRecieved;
            clientPipe.PipeClosedEvent += PipeClosed;
            clientPipe.Connect();


            do
            {
                var text = Console.ReadLine();
                if (text != null)
                {
                    Task t = clientPipe.WriteString(text);
                }

            } while (true);

            

        }

        private static void DataRecieved(object? sender, PipeEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.m_pData, 0, e.m_nDataLen);
            Console.WriteLine("Got command from server: " + data);
        }

        private static void PipeClosed(object? sender, EventArgs e)
        {
            Console.WriteLine("Pipe has been closed");
        }


    }



    



    //public class Client
    //{

    //    SynchronizationContext _context;
    //    Thread m_pThread = null;
    //    volatile bool m_bDieThreadDie;
    //    ServerPipe m_pServerPipe;




    //    private static void M_pServerPipe_ReadDataEvent(object sender, PipeEventArgs e)
    //    {
    //        // this gets called on an anonymous thread

    //        byte[] pBytes = e.m_pData;
    //        string szBytes = Misc.BytesToString(pBytes, e.m_pData.Length);
    //        PipeCommandPlusString pCmd = JsonConvert.DeserializeObject<PipeCommandPlusString>(szBytes);
    //        string szValue = pCmd.GetTransmittedString();

    //        if (szValue == "CONNECT")
    //        {
    //            Debug.WriteLine("Got command from client: " + pCmd.GetCommand() + "-" + pCmd.GetTransmittedString() + ", writing command back to client");
    //            PipeCommandPlusString pCmdToSend = new PipeCommandPlusString("SERVER", "CONNECTED");
    //            // fire off an async write
    //            Task t = m_pServerPipe.SendCommandAsync(pCmdToSend);
    //        }
    //    }


    //    private void Form1_Load(object sender, EventArgs e)
    //    {
    //        _context = SynchronizationContext.Current;

    //        m_pServerPipe = new ServerPipe("SQUALL_PIPE", 0);
    //        m_pServerPipe.ReadDataEvent += M_pServerPipe_ReadDataEvent;
    //        m_pServerPipe.PipeClosedEvent += M_pServerPipe_PipeClosedEvent;

    //        // m_pThread = new Thread(StaticThreadProc);
    //        // m_pThread.Start( this );
    //    }

    //    private void M_pServerPipe_PipeClosedEvent(object sender, EventArgs e)
    //    {
    //        Debug.WriteLine("Server: Pipe was closed, shutting down");

    //        // have to post this on the main thread
    //        _context.Post(delegate
    //        {
    //            Close();
    //        }, null);
    //    }

    //    private void M_pServerPipe_ReadDataEvent(object sender, PipeEventArgs e)
    //    {
    //        // this gets called on an anonymous thread

    //        byte[] pBytes = e.m_pData;
    //        string szBytes = Misc.BytesToString(pBytes, e.m_pData.Length);
    //        PipeCommandPlusString pCmd = JsonConvert.DeserializeObject<PipeCommandPlusString>(szBytes);
    //        string szValue = pCmd.GetTransmittedString();

    //        if (szValue == "CONNECT")
    //        {
    //            Debug.WriteLine("Got command from client: " + pCmd.GetCommand() + "-" + pCmd.GetTransmittedString() + ", writing command back to client");
    //            PipeCommandPlusString pCmdToSend = new PipeCommandPlusString("SERVER", "CONNECTED");
    //            // fire off an async write
    //            Task t = m_pServerPipe.SendCommandAsync(pCmdToSend);
    //        }
    //    }

    //    static void StaticThreadProc(Object o)
    //    {
    //        Form1 pThis = o as Form1;
    //        pThis.ThreadProc();
    //    }

    //    void ThreadProc()
    //    {
    //        m_pClientPipe = new ClientPipe(".", "SQUALL_PIPE");
    //        m_pClientPipe.ReadDataEvent += PClientPipe_ReadDataEvent;
    //        m_pClientPipe.PipeClosedEvent += M_pClientPipe_PipeClosedEvent;
    //        m_pClientPipe.Connect();

    //        PipeCommandPlusString pCmd = new PipeCommandPlusString("CLIENT", "CONNECT");
    //        int Counter = 1;
    //        while (Counter++ < 10)
    //        {
    //            Debug.WriteLine("Counter = " + Counter);
    //            m_pClientPipe.SendCommandAsync(pCmd);
    //            Thread.Sleep(3000);
    //        }

    //        while (!m_bDieThreadDie)
    //        {
    //            Thread.Sleep(1000);
    //        }

    //        m_pClientPipe.ReadDataEvent -= PClientPipe_ReadDataEvent;
    //        m_pClientPipe.PipeClosedEvent -= M_pClientPipe_PipeClosedEvent;
    //        m_pClientPipe.Close();
    //        m_pClientPipe = null;
    //    }

        

        

    //    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    //    {
    //        if (m_pThread != null)
    //        {
    //            m_bDieThreadDie = true;
    //            m_pThread.Join();
    //            m_bDieThreadDie = false;
    //        }

    //        m_pServerPipe.ReadDataEvent -= M_pServerPipe_ReadDataEvent;
    //        m_pServerPipe.PipeClosedEvent -= M_pServerPipe_PipeClosedEvent;
    //        m_pServerPipe.Close();
    //        m_pServerPipe = null;

    //    }

    //}
}
