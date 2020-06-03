﻿using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class ListOffresVM : ViewModelBase
    {
        public ListOffresVM() { }

        private readonly ObservableCollection<Offre> _listOffres = new ObservableCollection<Offre>();
        public ObservableCollection<Offre> ListOffres
        { get { return _listOffres; } }

        public Offre SelectedOffre
        {
            get { return OffreDataM.Instance.Offre; }
            set
            {
                OffreDataM.Instance.Offre = value;
                RaisepropertyChanged();
            }
        }

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

        public void UpdateListOffres()
        {
            List<Offre> updatedListOffres = OffreDataM.Instance.ListOffres;
            for (int i = ListOffres.Count - 1; i >= 0; i--)
            {
                if (!updatedListOffres.Contains(ListOffres[i]))
                { ListOffres.RemoveAt(i); }
            }
            foreach (Offre offre in updatedListOffres)
            {
                if (!ListOffres.Contains(offre))
                { ListOffres.Add(offre); }
            }
            FilterOrderObject filterOrder = FilterDataM.Instance.FilterOrder;
            if (filterOrder.ColumnNumber == 1)
            { 
                if (filterOrder.Asc)
                { ListOffres.OrderBy(t => t.TITRE); }
                else
                { ListOffres.OrderByDescending(t => t.TITRE); }
            }
            else if (filterOrder.ColumnNumber == 6)
            {
                if (filterOrder.Asc)
                { ListOffres.OrderBy(t => t.DATEPUBLICATION); }
                else
                { ListOffres.OrderByDescending(t => t.DATEPUBLICATION); }
            }
            RaisepropertyChanged(nameof(SelectedOffre));
        }
    }
}
