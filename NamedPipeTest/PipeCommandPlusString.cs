namespace NamedPipeTest
{
    /******************************************************************************
     * if we're just going to send a string back and forth, then we can use this
     * class. It it allows us to get the bytes as a string. sort of silly.
     ******************************************************************************/

    [Serializable]
    public class PipeCommandPlusString
    {
        public string m_szCommand;  // must be public to be serialized
        public string m_szString;   // ditto

        public PipeCommandPlusString(string sz, string szString)
        {
            m_szCommand = sz;
            m_szString = szString;
        }

        public string GetCommand()
        {
            return m_szCommand;
        }

        public string GetTransmittedString()
        {
            return m_szString;
        }
    }
}
