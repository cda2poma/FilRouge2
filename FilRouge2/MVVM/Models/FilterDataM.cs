using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class FilterDataM
    {
        private static volatile FilterDataM instance;
        private static readonly object syncRoot = new object();

        private FilterDataM()
        {
            ListTypesPostes = new List<TypePoste>();
            ListTypesContrats = new List<TypeContrat>();
            ListRegions = new List<RegionFrancaise>();
            ListFilterOrder = new List<FilterOrderObject>();
        }

        public static FilterDataM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { instance = new FilterDataM(); }
                    }
                }
                return instance;
            }
        }

        public DateTime MinMinChoosableDate { get; set; }

        public DateTime MinMaxChoosableDate { get; set; }

        public DateTime MaxMinChoosableDate { get; set; }

        public DateTime MaxMaxChoosableDate { get; set; }

        public List<TypePoste> ListTypesPostes { get; set; }
        public bool AreThereTypesPosteInList { get; set; }
        public List<TypeContrat> ListTypesContrats { get; set; }
        public List<RegionFrancaise> ListRegions { get; set; }
        public List<FilterOrderObject> ListFilterOrder { get; set; }

        public string Title { get; set; }
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }
        public TypePoste TypePoste { get; set; }
        public TypeContrat TypeContrat { get; set; }
        public RegionFrancaise Region { get; set; }
        public string Desc { get; set; }
        public int DescConfig { get; set; }
        public FilterOrderObject FilterOrder { get; set; }
        public bool IsFilterLimitNull { get; set; }

        public bool ReloadingPage { get; set; }

        public bool DataTransferFilterIsDefault(DTOfilter filter)
        {
            return filter.TITRE == "" && filter.DESC == "" && filter.IDTYPEPOSTE == 0 && filter.IDTYPECONTRAT == 0 && filter.IDREGION == 0 && filter.DATEPUBLICATIONMIN == MinMinChoosableDate
                  && filter.DATEPUBLICATIONMAX == DateTime.Today && FilterOrderObjectIsDefault(filter.FilterOrder);
        }

        private bool FilterOrderObjectIsDefault(FilterOrderObject filter)
        { return filter.ColumnNumber == 6 && filter.Asc == false && filter.Limit == null; }

        public bool OffreMatchesFilter(Offre offre)
        {
            if (!string.IsNullOrEmpty(offre.TEXTEDESC) && !string.IsNullOrEmpty(Desc))
            {
                if (DescConfig < 0 && !Desc.Split(' ').Any(s => offre.TEXTEDESC.ToLower().Contains(s.ToLower())))
                { return false; }
                else if (DescConfig == 0)
                {
                    string[] words = Desc.Split();
                    foreach (string word in words)
                    {
                        if (!offre.TEXTEDESC.ToLower().Contains(word.ToLower()))
                        { return false; }
                    }
                }
            }
            if (!(offre.DATEPUBLICATION >= DateMin)) return false;
            if (!(offre.DATEPUBLICATION <= DateMax)) return false;
            if (!(string.IsNullOrEmpty(Title)))
            { if (!offre.TITRE.ToLower().Contains(Title)) return false; }
            if (!(TypePoste.ID == 0 || TypePoste.ID == offre.TYPEPOSTE.ID)) return false;
            if (!(TypeContrat.ID == 0 || TypeContrat.ID == offre.TYPECONTRAT.ID)) return false;
            if (!(Region.ID == 0 || Region.ID == offre.REGION.ID)) return false;
            return true;
        }
    }
}
