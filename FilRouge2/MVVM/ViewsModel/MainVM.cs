using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class MainVM : ViewModelBase
    {
        private string _offreDesc;

        public string OffreDesc
        {
            get { return _offreDesc; }
            set
            {
                _offreDesc = value;
                RaisepropertyChanged();
            }
        }
    }
}
