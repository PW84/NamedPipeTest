using System.Diagnostics;
using System.IO.Pipes;

namespace NamedPipeTest
{
    //public interface PipeSender
    //{
    //    Task SendCommandAsync(PipeCommandPlusString pCmd);
    //}

    public class ClientPipe : BasicPipe
    {
        NamedPipeClientStream m_pPipe;

        public ClientPipe(string szServerName, string szPipeName)
            : base("Client")
        {
            m_szPipeName = szPipeName; // debugging
            m_pPipe = new NamedPipeClientStream(szServerName, szPipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
            base.SetPipeStream(m_pPipe); // inform base class what to read/write from
        }

        public void Connect()
        {
            Debug.WriteLine("Pipe " + FullPipeNameDebug() + " connecting to server");
            m_pPipe.Connect(); // doesn't seem to be an async method for this routine. just a timeout.
            StartReadingAsync();
        }

        // the client's pipe index is always 0
        internal override int PipeId() { return 0; }
    }
}
