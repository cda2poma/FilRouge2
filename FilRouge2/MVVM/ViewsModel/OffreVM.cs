using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class OffreVM : ViewModelBase
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

        public event EventHandler<Offre> EditedOffre_Event;

        public event EventHandler DeletedOffre_Event;

        public void Suscribe()
        {
            OffreDataM.Instance.ViewingSingleOffre = true;
            ConnectionDataM.Instance.UpdateOffreEvent += UpdateOffreEvent;
            ConnectionDataM.Instance.DeletedOffreEvent += DeletedOffreEvent;
            ConnectionDataM.Instance.UpdateTypePosteEvent += UpdateTypePosteEvent;
        }

        public void Unsuscribe()
        {
            OffreDataM.Instance.ViewingSingleOffre = false;
            ConnectionDataM.Instance.UpdateOffreEvent += UpdateOffreEvent;
            ConnectionDataM.Instance.DeletedOffreEvent += DeletedOffreEvent;
            ConnectionDataM.Instance.UpdateTypePosteEvent += UpdateTypePosteEvent;
        }

        private void UpdateOffreEvent(object sender, List<DTOoffre> e)
        {
            if (OffreDataM.Instance.ViewingSingleOffre && e[1].OffreToTransfer.ID == OffreDataM.Instance.Offre.ID)
            {
                Title = e[1].OffreToTransfer.TITRE;
                TypePoste = e[1].OffreToTransfer.TYPEPOSTE;
                TypeContratTitle = e[1].OffreToTransfer.TYPECONTRAT.INTITULE;
                RegionName = e[1].OffreToTransfer.REGION.NOM;
                PublicationDate = e[1].OffreToTransfer.DATEPUBLICATION;
                LastEditionDate = (DateTime)e[1].OffreToTransfer.DATEDERNIEREMAJ;
                Desc = e[1].OffreToTransfer.TEXTEDESC;
                Url = e[1].OffreToTransfer.LIENWEB;
                EditedOffre_Event(this, e[1].OffreToTransfer);
            }
        }

        private void DeletedOffreEvent(object sender, DTOoffre e)
        {
            if (e.OffreToTransfer.ID == OffreDataM.Instance.Offre.ID)
            { DeletedOffre_Event(this, new EventArgs()); }
        }

        private void UpdateTypePosteEvent(object sender, TypePoste e)
        { TypePoste = e; }
    }
}
