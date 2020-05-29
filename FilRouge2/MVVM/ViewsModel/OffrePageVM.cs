using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class OffrePageVM : ViewModelBase
    {
        public string Title
        {
            get { return OffreDataM.Instance.Title; }
            set
            {
                OffreDataM.Instance.Title = value;
                RaisepropertyChanged();
            }
        }

        public string TypePosteTitle
        {
            get { return OffreDataM.Instance.TypePosteTitle; }
            set
            {
                OffreDataM.Instance.TypePosteTitle = value;
                RaisepropertyChanged();
            }
        }

        public string TypeContratTitle
        {
            get { return OffreDataM.Instance.TypeContratTitle; }
            set
            {
                OffreDataM.Instance.TypeContratTitle = value;
                RaisepropertyChanged();
            }
        }

        public string RegionName
        {
            get { return OffreDataM.Instance.RegionName; }
            set
            {
                OffreDataM.Instance.RegionName = value;
                RaisepropertyChanged();
            }
        }

        public string DateMin
        {
            get { return OffreDataM.Instance.DateMin; }
            set
            {
                OffreDataM.Instance.DateMin = value;
                RaisepropertyChanged();
            }
        }

        public string DateMax
        {
            get { return OffreDataM.Instance.DateMax; }
            set
            {
                OffreDataM.Instance.DateMax = value;
                RaisepropertyChanged();
            }
        }

        public string Desc
        {
            get { return OffreDataM.Instance.Desc; }
            set
            {
                OffreDataM.Instance.Desc = value;
                RaisepropertyChanged();
            }
        }

        public string Url
        {
            get { return OffreDataM.Instance.Url; }
            set
            {
                OffreDataM.Instance.Url = value;
                RaisepropertyChanged();
            }
        }
    }
}
