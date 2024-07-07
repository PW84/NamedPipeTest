namespace NamedPipeTest
{
    public class PipeEventArgs
    {
        public byte[] m_pData;
        public int m_nDataLen;

        public PipeEventArgs(byte[] pData, int nDataLen)
        {
            // is this a copy, or an alias copy? I can't remember right now.
            m_pData = pData;
            m_nDataLen = nDataLen;
        }
    }
}
