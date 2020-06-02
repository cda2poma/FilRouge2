using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    public class ConnectionDataM
    {
        private static volatile ConnectionDataM instance;
        private static readonly object syncRoot = new object();

        private ConnectionDataM() { }

        public static ConnectionDataM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { instance = new ConnectionDataM(); }
                    }
                }
                return instance;
            }
        }

        public bool TryReconnect;

        public bool IsConnecting;
    }
}
