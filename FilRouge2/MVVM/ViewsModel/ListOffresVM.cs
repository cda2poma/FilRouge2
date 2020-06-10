using BO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                if (!_listCleared && !OffreDataM.Instance.ViewingSingleOffre)
                {
                    OffreDataM.Instance.Offre = value;
                    Title = value.TITRE;
                    TypePoste = value.TYPEPOSTE;
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

        public TypePoste TypePoste
        {
            get { return OffreDataM.Instance.TypePoste; }
            set
            {
                OffreDataM.Instance.TypePoste = value;
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

        public event EventHandler<string> SelectedOffreDeletedEvent;

        public event EventHandler GoBackToMainPageEvent;

        public void UpdateListOffres()
        {
            ListOffres.Clear();
            foreach (Offre offre in OffreDataM.Instance.ListOffres)
            { ListOffres.Add(offre); }
            if (ListOffres.Count > 0)
            { 
                SelectedOffre = ListOffres[0];
                if (!OffreDataM.Instance.ListOffresSuscribed)
                {
                    ConnectionDataM.Instance.NewOffreEvent += NewOffreEvent;
                    ConnectionDataM.Instance.UpdateOffreEvent += UpdateOffreEvent;
                    ConnectionDataM.Instance.DeletedOffreEvent += DeletedOffreEvent;
                    ConnectionDataM.Instance.UpdateTypePosteEvent += UpdateTypePosteEvent;
                    OffreDataM.Instance.ListOffresSuscribed = true;
                }
            }
            else
            {
                if (OffreDataM.Instance.ListOffresSuscribed)
                {
                    ConnectionDataM.Instance.NewOffreEvent -= NewOffreEvent;
                    ConnectionDataM.Instance.UpdateOffreEvent -= UpdateOffreEvent;
                    ConnectionDataM.Instance.DeletedOffreEvent -= DeletedOffreEvent;
                    ConnectionDataM.Instance.UpdateTypePosteEvent -= UpdateTypePosteEvent;
                    OffreDataM.Instance.ListOffresSuscribed = false;
                }
                GoBackToMainPageEvent(this, new EventArgs());
            }
        }

        private bool _listCleared;

        public void UpdateListOffres(int id)
        {
            bool offreFound = false;
            string oldOffreName = SelectedOffre.TITRE;
            _listCleared = true;
            ListOffres.Clear();
            _listCleared = false;
            for (int i = 0; i < OffreDataM.Instance.ListOffres.Count; i++)
            {
                ListOffres.Add(OffreDataM.Instance.ListOffres[i]);
                if (ListOffres[i].ID == id)
                { 
                    SelectedOffre = ListOffres[i];
                    offreFound = true;
                }
            }
            if (!offreFound)
            {
                if (ListOffres.Count == 0)
                { GoBackToMainPageEvent(this, new EventArgs()); }
                else
                {
                    SelectedOffre = ListOffres[0];
                    if (!OffreDataM.Instance.ViewingSingleOffre)
                    { SelectedOffreDeletedEvent(this, oldOffreName); }
                    RaisepropertyChanged(nameof(SelectedOffre));
                }
            }
        }

        public void SetSelectedOffre()
        {
            TypePoste = SelectedOffre.TYPEPOSTE;
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
                OffreDataM.Instance.ListOffres.Add(e.OffreToTransfer);
                FilterListOffres();
                UpdateListOffres(SelectedOffre.ID);
            }
        }

        public void UpdateOffreEvent(object sender, List<DTOoffre> e)
        {
            for (int i = 0; i < OffreDataM.Instance.ListOffres.Count; i++)
            {
                if (OffreDataM.Instance.ListOffres[i].ID == SelectedOffre.ID)
                { 
                    OffreDataM.Instance.ListOffres.RemoveAt(i);
                    break;
                }
            }
            if (FilterDataM.Instance.OffreMatchesFilter(e[1].OffreToTransfer))
            { 
                OffreDataM.Instance.ListOffres.Add(e[1].OffreToTransfer);
                FilterListOffres();
            }
            UpdateListOffres(SelectedOffre.ID);
            
        }

        public void DeletedOffreEvent(object sender, DTOoffre e)
        {
            for (int i = 0; i < OffreDataM.Instance.ListOffres.Count; i++)
            {
                if (OffreDataM.Instance.ListOffres[i].ID == SelectedOffre.ID)
                {
                    OffreDataM.Instance.ListOffres.RemoveAt(i);
                    UpdateListOffres(SelectedOffre.ID);
                    break;
                }
            }
        }

        private void UpdateTypePosteEvent(object sender, TypePoste e)
        {
            if (TypePoste.ID == e.ID)
            { TypePoste = e; }
        }

        public void FilterListOffres()
        {
            FilterOrderObject filterOrder = FilterDataM.Instance.FilterOrder;
            if (filterOrder.ColumnNumber == 1)
            {
                if (filterOrder.Asc)
                { OffreDataM.Instance.ListOffres = OffreDataM.Instance.ListOffres.OrderBy(t => t.TITRE).ToList(); }
                else
                { OffreDataM.Instance.ListOffres = OffreDataM.Instance.ListOffres.OrderByDescending(t => t.TITRE).ToList(); }
            }
            else if (filterOrder.ColumnNumber == 6)
            {
                if (filterOrder.Asc)
                { OffreDataM.Instance.ListOffres = OffreDataM.Instance.ListOffres.OrderBy(t => t.DATEPUBLICATION).ToList(); }
                else
                { OffreDataM.Instance.ListOffres = OffreDataM.Instance.ListOffres.OrderByDescending(t => t.DATEPUBLICATION).ToList(); }
            }
        }

        public void Unsuscribe()
        {
            ConnectionDataM.Instance.NewOffreEvent -= NewOffreEvent;
            ConnectionDataM.Instance.UpdateOffreEvent -= UpdateOffreEvent;
            ConnectionDataM.Instance.DeletedOffreEvent -= DeletedOffreEvent;
            ConnectionDataM.Instance.UpdateTypePosteEvent -= UpdateTypePosteEvent;
            OffreDataM.Instance.ListOffresSuscribed = false;
        }
    }
}
