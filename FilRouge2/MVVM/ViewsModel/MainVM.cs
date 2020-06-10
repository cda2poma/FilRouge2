using BO;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;

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

        public DateTime MinMinChoosableDate
        {
            get { return FilterDataM.Instance.MinMinChoosableDate; }
            set
            {
                FilterDataM.Instance.MinMinChoosableDate = value;
                RaisepropertyChanged();
            }
        }

        public DateTime MinMaxChoosableDate
        {
            get { return FilterDataM.Instance.MinMaxChoosableDate; }
            set
            {
                FilterDataM.Instance.MinMaxChoosableDate = value;
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

        public DateTime MaxMinChoosableDate
        {
            get { return FilterDataM.Instance.MaxMinChoosableDate; }
            set
            {
                FilterDataM.Instance.MaxMinChoosableDate = value;
                RaisepropertyChanged();
            }
        }

        public DateTime MaxMaxChoosableDate
        { 
            get { return FilterDataM.Instance.MaxMaxChoosableDate; }
            set
            {
                FilterDataM.Instance.MaxMaxChoosableDate = value;
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

        public bool AreThereTypesPosteInList
        { 
            get { return FilterDataM.Instance.AreThereTypesPosteInList; }
            set
            {
                FilterDataM.Instance.AreThereTypesPosteInList = value;
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

        public bool IsFilterLimitNull
        {
            get { return FilterDataM.Instance.IsFilterLimitNull; }
            set
            {
                FilterDataM.Instance.IsFilterLimitNull = value;
                RaisepropertyChanged();
            }
        }

        public string OffreDesc
        {
            get { return FilterDataM.Instance.Desc; }
            set
            {
                FilterDataM.Instance.Desc = value;
                RaisepropertyChanged();
            }
        }

        public int ConnectionState
        {
            get { return ConnectionDataM.Instance.ConnectionState; }
            set
            {
                ConnectionDataM.Instance.ConnectionState = value;
                RaisepropertyChanged();
            }
        }

        public event EventHandler<string> SelectedTypePosteDeletedEvent;

        public void ResetData()
        {
            OffreTitle = "";
            OffreDateMin = FilterDataM.Instance.MinMinChoosableDate;
            OffreDateMax = DateTime.Today;
            SelectedTypePoste = _listTypesPostes[0];
            SelectedTypeContrat = _listTypesContrat[0];
            SelectedRegion = _listRegions[0];
            SelectedFilterOrder = _listFilterOrder[0];
        }

        public async Task<bool> LoadData()
        {
            OffreDateMin = DateTime.Today;
            OffreDateMax = DateTime.Today;
            MinMinChoosableDate = DateTime.Today;
            MinMaxChoosableDate = DateTime.Today;
            MaxMinChoosableDate = DateTime.Today;
            MaxMaxChoosableDate = DateTime.Today;
            List<TypePoste> listTypesPostes = new List<TypePoste>();
            List<TypeContrat> listTypesContrat = new List<TypeContrat>();
            List<RegionFrancaise> listRegions = new List<RegionFrancaise>();
            try
            {
                listTypesPostes = await ConnectionDataM.Instance.GetAllTypesPostesPlusVoidValue();
                listTypesContrat = await ConnectionDataM.Instance.GetAllTypesContratsPlusVoidValue();
                listRegions = await ConnectionDataM.Instance.GetAllRegionsPlusVoidValue();
                await ConnectionDataM.Instance.GetMinDate();
            }
            catch (HubException)
            {
                ConnectionDataM.Instance.ConnectionEstablishedButDataLoadingFailed = true;
                return false;
            }
            MinMinChoosableDate = FilterDataM.Instance.MinMinChoosableDate;
            OffreDateMin = MinMinChoosableDate;
            foreach (TypePoste typePoste in listTypesPostes)
            { ListTypesPostes.Add(typePoste); }
            AreThereTypesPosteInList = ListTypesPostes.Count > 1;
            foreach (TypeContrat typeContrat in listTypesContrat)
            { ListTypesContrat.Add(typeContrat); }
            foreach (RegionFrancaise region in listRegions)
            { ListRegions.Add(region); }
            ListFilterOrder.Add(new FilterOrderObject("Du plus récent au plus ancien", 6, false, null));
            ListFilterOrder.Add(new FilterOrderObject("Du plus ancien au plus récent", 6, true, null));
            ListFilterOrder.Add(new FilterOrderObject("Ordre alphabétique", 1, false, null));
            ListFilterOrder.Add(new FilterOrderObject("Du Ordre alphabétique inversé", 1, true, null));
            ListFilterOrder.Add(new FilterOrderObject("Dix dernières offres", 6, false, 10));
            SelectedTypePoste = ListTypesPostes[0];
            SelectedTypeContrat = ListTypesContrat[0];
            int index = 0;
            if (RegionPreferenceM.Instance.PreferedRegion != null)
            {
                for (int i = 0; i < ListRegions.Count; i++)
                {
                    if (ListRegions[i].ID == RegionPreferenceM.Instance.PreferedRegion.ID)
                    {
                        index = i;
                        break;
                    }
                }
            }
            SelectedRegion = ListRegions[index];
            SelectedFilterOrder = ListFilterOrder[0];
            ConnectionDataM.Instance.NewOffreEvent += NewOffreEvent;
            ConnectionDataM.Instance.DeletedOffreEvent += DeletedOffreEvent;
            ConnectionDataM.Instance.UpdateOffreEvent += UpdateOffreEvent;
            ConnectionDataM.Instance.UpdateTypePosteEvent += UpdateTypePosteEvent;
            return true;
        }

        public void ReloadData()
        {
            OffreTitle = FilterDataM.Instance.Title;
            OffreDateMin = FilterDataM.Instance.DateMin;
            OffreDateMax = FilterDataM.Instance.DateMax;
            MinMinChoosableDate = FilterDataM.Instance.MinMinChoosableDate;
            MinMaxChoosableDate = FilterDataM.Instance.MinMaxChoosableDate;
            MaxMinChoosableDate = FilterDataM.Instance.MinMaxChoosableDate;
            MaxMaxChoosableDate = FilterDataM.Instance.MinMaxChoosableDate;
            foreach (TypePoste typePoste in FilterDataM.Instance.ListTypesPostes)
            { ListTypesPostes.Add(typePoste); }
            AreThereTypesPosteInList = ListTypesPostes.Count > 1;
            foreach (TypeContrat typeContrat in FilterDataM.Instance.ListTypesContrats)
            { ListTypesContrat.Add(typeContrat); }
            foreach (RegionFrancaise region in FilterDataM.Instance.ListRegions)
            { ListRegions.Add(region); }
            ListFilterOrder.Add(new FilterOrderObject("Du plus récent au plus ancien", 6, false, null));
            ListFilterOrder.Add(new FilterOrderObject("Du plus ancien au plus récent", 6, true, null));
            ListFilterOrder.Add(new FilterOrderObject("Ordre alphabétique", 1, false, null));
            ListFilterOrder.Add(new FilterOrderObject("Du Ordre alphabétique inversé", 1, true, null));
            ListFilterOrder.Add(new FilterOrderObject("Dix dernières offres", 6, false, 10));
            for (int i = 0; i < ListTypesPostes.Count; i++)
            { 
                if (ListTypesPostes[i].ID == FilterDataM.Instance.TypePoste.ID)
                { 
                    SelectedTypePoste = ListTypesPostes[i];
                    break;
                }
            }
            for (int i = 0; i < ListTypesContrat.Count; i++)
            {
                if (ListTypesContrat[i].ID == FilterDataM.Instance.TypeContrat.ID)
                {
                    SelectedTypeContrat = ListTypesContrat[i];
                    break;
                }
            }
            for (int i = 0; i < ListRegions.Count; i++)
            {
                if (ListRegions[i].ID == FilterDataM.Instance.Region.ID)
                {
                    SelectedRegion = ListRegions[i];
                    break;
                }
            }
            for (int i = 0; i < ListFilterOrder.Count; i++)
            {
                if (ListFilterOrder[i].Asc == FilterDataM.Instance.FilterOrder.Asc
                    && ListFilterOrder[i].ColumnNumber == FilterDataM.Instance.FilterOrder.ColumnNumber
                    && ListFilterOrder[i].Limit == FilterDataM.Instance.FilterOrder.Limit)
                { 
                    SelectedFilterOrder = ListFilterOrder[i];
                    break;
                }
            }
            FilterDataM.Instance.ReloadingPage = false;
        }

        public async Task<bool> FilterData()
        {
            if (ConnectionDataM.Instance.ConnectionEstablishedButDataLoadingFailed)
            { return await LoadData(); }
            try
            {
                await ConnectionDataM.Instance.GetFilteredListOffres(OffreTitle, SelectedTypePoste.ID, SelectedTypeContrat.ID, SelectedRegion.ID,
                    (DateTime)OffreDateMin, (DateTime)OffreDateMax, OffreDesc, FilterDataM.Instance.DescConfig, SelectedFilterOrder);
                return true;
            }
            catch (InvalidOperationException)
            { return false; }
        }

        public void UpdateDateTimePickerEnabling()
        {
            IsFilterLimitNull = SelectedFilterOrder.Limit == null;
            RaisepropertyChanged(nameof(ConnectionState));
        }

        public void NewOffreEvent(object sender, DTOoffre e)
        {
            if (e.TypePosteAssignationAffected)
            { AddTypePoste(e.OffreToTransfer.TYPEPOSTE); }
        }

        public void DeletedOffreEvent(object sender, DTOoffre e)
        {
            if (e.TypePosteAssignationAffected)
            { DeleteTypePoste(e.OffreToTransfer.TYPEPOSTE.ID); }
        }

        public void UpdateOffreEvent(object ender, List<DTOoffre> e)
        {
            if (e[0].TypePosteAssignationAffected)
            { DeleteTypePoste(e[0].OffreToTransfer.TYPEPOSTE.ID); }
            if (e[1].TypePosteAssignationAffected)
            { AddTypePoste(e[1].OffreToTransfer.TYPEPOSTE); }
        }

        public void AddTypePoste(TypePoste typePoste)
        {
            FilterDataM.Instance.ListTypesPostes.Add(typePoste);
            FilterDataM.Instance.ListTypesPostes = FilterDataM.Instance.ListTypesPostes.OrderBy(x => x.INTITULE).ToList();
            UpdateListTypesPostes();
        }

        public void DeleteTypePoste(int id)
        {
            for (int i = 1; i < ListTypesPostes.Count; i++)
            {
                if (ListTypesPostes[i].ID == id)
                {
                    FilterDataM.Instance.ListTypesPostes.RemoveAt(i);
                    break;
                }
            }
            UpdateListTypesPostes();
        }

        public void UpdateTypePosteEvent(object sender, TypePoste e)
        {
            for (int i = 1; i < ListTypesPostes.Count; i++)
            {
                if (ListTypesPostes[i].ID == e.ID)
                {
                    ListTypesPostes[i].INTITULE = e.INTITULE;
                    break;
                }
            }
            UpdateListTypesPostes();
        }

        private void UpdateListTypesPostes()
        {
            int id = SelectedTypePoste.ID;
            string oldSelectedName = SelectedTypePoste.INTITULE;
            bool idFound = false;
            ListTypesPostes.Clear();
            for (int i = 0; i < FilterDataM.Instance.ListTypesPostes.Count; i++)
            {
                ListTypesPostes.Add(FilterDataM.Instance.ListTypesPostes[i]);
                if (ListTypesPostes[i].ID == id)
                {
                    idFound = true;
                    SelectedTypePoste = ListTypesPostes[i];
                }
            }
            if (!idFound)
            { 
                SelectedTypePoste = ListTypesPostes[0];
                SelectedTypePosteDeletedEvent(this, oldSelectedName);
            }
            RaisepropertyChanged(nameof(SelectedTypePoste));
        }
    }
}
