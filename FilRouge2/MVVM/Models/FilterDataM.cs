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

        private FilterDataM() { }

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

        public string Title { get; set; }
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }
        public TypePoste TypePoste { get; set; }
        public TypeContrat TypeContrat { get; set; }
        public RegionFrancaise Region { get; set; }
        public string Desc { get; set; }
        public int DescConfig { get; set; }
        public FilterOrderObject FilterOrder { get; set; }
    }
}
