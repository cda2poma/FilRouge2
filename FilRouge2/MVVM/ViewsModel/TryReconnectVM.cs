using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class TryReconnectVM : ViewModelBase
    {
        public TryReconnectVM() { }
        
        public bool Result
        {
            get { return ConnectionDataM.Instance.TryReconnect; }
            set
            {
                ConnectionDataM.Instance.TryReconnect = value;
                RaisepropertyChanged();
            }
        }
    }
}
