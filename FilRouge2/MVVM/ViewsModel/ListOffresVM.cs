using BO;
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
                Title = value.TITRE;
                TypePosteTitle = value.TYPEPOSTE.INTITULE;
                TypeContratTitle = value.TYPECONTRAT.INTITULE;
                RegionName = value.REGION.NOM;
                PublicationDate = value.DATEPUBLICATION;
                LastEditionDate = (DateTime)value.DATEDERNIEREMAJ;
                Desc = value.TEXTEDESC;
                Url = value.LIENWEB;
                SetSelectedOffre();
                RaisepropertyChanged();
            }
        }

        private bool suscribed;

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

        public DateTime PublicationDate
        {
            get { return OffreDataM.Instance.PublicationDate; }
            set
            {
                OffreDataM.Instance.PublicationDate = value;
                RaisepropertyChanged();
            }
        }

        public DateTime LastEditionDate
        {
            get { return OffreDataM.Instance.LastEditionDate; }
            set
            {
                OffreDataM.Instance.LastEditionDate = value;
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
            SelectedOffre = ListOffres[0];
            if (!suscribed)
            {
                suscribed = true;
                ConnectionDataM.Instance.NewOffreEvent += NewOffreEvent;
                ConnectionDataM.Instance.UpdateOffreEvent += UpdateOffreEvent;
                ConnectionDataM.Instance.DeletedOffreEvent += DeletedOffreEvent;
            }
            
        }

        public void SetSelectedOffre()
        {
            TypePosteTitle = SelectedOffre.TYPEPOSTE.INTITULE;
            TypeContratTitle = SelectedOffre.TYPECONTRAT.INTITULE;
            RegionName = SelectedOffre.REGION.NOM;
            PublicationDate = SelectedOffre.DATEPUBLICATION;
            LastEditionDate = (DateTime)SelectedOffre.DATEDERNIEREMAJ;
            Desc = SelectedOffre.TEXTEDESC;
            Url = SelectedOffre.LIENWEB;
        }

        public void NewOffreEvent(object sender, DTOoffre e)
        {
            if (FilterDataM.Instance.OffreMatchesFilter(e.OffreToTransfer))
            { 
                ListOffres.Add(e.OffreToTransfer);
                FilterListOffres();
            }
            
        }

        public void UpdateOffreEvent(object sender, List<DTOoffre> e)
        {
            if(ListOffres.Contains(e[0].OffreToTransfer))
            { ListOffres.Remove(e[0].OffreToTransfer); }
            if (FilterDataM.Instance.OffreMatchesFilter(e[1].OffreToTransfer))
            {
                ListOffres.Add(e[1].OffreToTransfer);
                FilterListOffres();
            }
        }

        public void DeletedOffreEvent(object sender, DTOoffre e)
        {
            if (ListOffres.Contains(e.OffreToTransfer))
            { ListOffres.Remove(e.OffreToTransfer); }
        }

        public void FilterListOffres()
        {
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
        }
    }
}
