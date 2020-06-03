using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class MainVM : ViewModelBase
    {
        public string OffreTitle
        {
            get { return FilterDataM.Instance.Title; }
            set
            {
                FilterDataM.Instance.Title = value;
                RaisepropertyChanged();
            }
        }

        public DateTime OffreDateMin
        {
            get { return FilterDataM.Instance.DateMin; }
            set
            {
                FilterDataM.Instance.DateMin = value;
                RaisepropertyChanged();
            }
        }

        public DateTime OffreDateMax
        {
            get { return FilterDataM.Instance.DateMax; }
            set
            {
                FilterDataM.Instance.DateMax = value;
                RaisepropertyChanged();
            }
        }

        private readonly ObservableCollection<TypePoste> _listTypesPostes = new ObservableCollection<TypePoste>();

        public ObservableCollection<TypePoste> ListTypesPostes
        { get { return _listTypesPostes; } }

        public TypePoste SelectedTypePoste
        { 
            get { return FilterDataM.Instance.TypePoste; }
            set
            {
                FilterDataM.Instance.TypePoste = value;
                RaisepropertyChanged();
            }
        }

        private readonly ObservableCollection<TypeContrat> _listTypesContrat = new ObservableCollection<TypeContrat>();

        public ObservableCollection<TypeContrat> ListTypesContrat
        { get { return _listTypesContrat; } }

        public TypeContrat SelectedTypeContrat
        {
            get { return FilterDataM.Instance.TypeContrat; }
            set
            {
                FilterDataM.Instance.TypeContrat = value;
                RaisepropertyChanged();
            }
        }

        private readonly ObservableCollection<RegionFrancaise> _listRegions = new ObservableCollection<RegionFrancaise>();

        public ObservableCollection<RegionFrancaise> ListRegions
        { get { return _listRegions; } }

        public RegionFrancaise SelectedRegion
        {
            get { return FilterDataM.Instance.Region; }
            set
            {
                FilterDataM.Instance.Region = value;
                RaisepropertyChanged();
            }
        }

        public int DescConfig
        {
            get { return FilterDataM.Instance.DescConfig; }
            set { FilterDataM.Instance.DescConfig = value; }
        }

        private readonly ObservableCollection<FilterOrderObject> _listFilterOrder = new ObservableCollection<FilterOrderObject>();

        public ObservableCollection<FilterOrderObject> ListFilterOrder
        { get { return _listFilterOrder; } }

        public FilterOrderObject SelectedFilterOrder
        {
            get { return FilterDataM.Instance.FilterOrder; }
            set
            {
                FilterDataM.Instance.FilterOrder = value;
                RaisepropertyChanged();
            }
        }

        private string _offreDesc;

        public string OffreDesc
        {
            get { return FilterDataM.Instance.Desc; }
            set
            {
                FilterDataM.Instance.Desc = value;
                RaisepropertyChanged();
            }
        }

        private bool _isConnecting;

        public bool IsConnecting
        {
            get { return ConnectionDataM.Instance.IsConnecting; }
            set
            {
                ConnectionDataM.Instance.IsConnecting = value;
                RaisepropertyChanged();
            }
        }

        public async Task ConnectAsync()
        {
            IsConnecting = true;

        }

        public void ResetData()
        {
            OffreTitle = "";
            OffreDateMin = ConnectionDataM.Instance.MinDate;
            OffreDateMax = DateTime.Today;
            SelectedTypePoste = _listTypesPostes[0];
            SelectedTypeContrat = _listTypesContrat[0];
            SelectedRegion = _listRegions[0];
            SelectedFilterOrder = _listFilterOrder[0];
        }

        public void FilterData()
        {
            ConnectionDataM.Instance.FilterData();
        }
    }
}
