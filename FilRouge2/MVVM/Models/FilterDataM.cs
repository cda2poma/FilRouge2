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

        public event EventHandler<DateTime> MinDateChangeEvent;

        public string Title { get; set; }
        private DateTime _minChoosableDate;

        public DateTime MinChoosableDate
        {
            get { return _minChoosableDate; }
            set
            {
                _minChoosableDate = value;
                MinDateChangeEvent(null, value);
            }
        }
        public List<TypePoste> ListTypesPostes { get; set; }
        public List<TypeContrat> ListTypesContrats { get; set; }
        public List<RegionFrancaise> ListRegions { get; set; }
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }
        public TypePoste TypePoste { get; set; }
        public TypeContrat TypeContrat { get; set; }
        public RegionFrancaise Region { get; set; }
        public string Desc { get; set; }
        public int DescConfig { get; set; }
        public FilterOrderObject FilterOrder { get; set; }

        public bool DataTransferFilterIsDefault(DTOfilter filter)
        {
            return filter.TITRE == "" && filter.DESC == "" && filter.IDTYPEPOSTE == 0 && filter.IDTYPECONTRAT == 0 && filter.IDREGION == 0 && filter.DATEPUBLICATIONMIN == _minChoosableDate
                  && filter.DATEPUBLICATIONMAX == DateTime.Today && FilterOrderObjectIsDefault(filter.FilterOrder);
        }

        private bool FilterOrderObjectIsDefault(FilterOrderObject filter)
        { return filter.ColumnNumber == 6 && filter.Asc == false; }
    }
}
