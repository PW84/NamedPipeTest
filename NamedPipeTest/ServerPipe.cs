using System.Diagnostics;
using System.IO.Pipes;

namespace NamedPipeTest
{
    public class ServerPipe : BasicPipe
    {
        public event EventHandler<EventArgs> GotConnectionEvent;

        NamedPipeServerStream m_pPipe;
        int m_nPipeId;

        public ServerPipe(string szPipeName, int nPipeId)
            : base("Server")
        {
            m_szPipeName = szPipeName;
            m_nPipeId = nPipeId;
            m_pPipe = new NamedPipeServerStream(
                szPipeName,
                PipeDirection.InOut,
                NamedPipeServerStream.MaxAllowedServerInstances,
                PipeTransmissionMode.Message,
                PipeOptions.Asynchronous);
            base.SetPipeStream(m_pPipe);
            m_pPipe.BeginWaitForConnection(new AsyncCallback(StaticGotPipeConnection), this);
        }

        static void StaticGotPipeConnection(IAsyncResult pAsyncResult)
        {
            ServerPipe pThis = pAsyncResult.AsyncState as ServerPipe;
            pThis.GotPipeConnection(pAsyncResult);
        }

        void GotPipeConnection(IAsyncResult pAsyncResult)
        {
            m_pPipe.EndWaitForConnection(pAsyncResult);

            Debug.WriteLine("Server Pipe " + m_szPipeName + " got a connection");

            if (GotConnectionEvent != null)
            {
                GotConnectionEvent(this, new EventArgs());
            }

            // lodge the first read request to get us going
            //
            StartReadingAsync();
        }

        internal override int PipeId() { return m_nPipeId; }
    }
}
