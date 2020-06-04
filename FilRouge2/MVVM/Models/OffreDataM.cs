using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge2
{
    class OffreDataM
    {
        private static volatile OffreDataM instance;
        private static readonly object syncRoot = new object();

        private OffreDataM() { }

        public static OffreDataM Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        { instance = new OffreDataM(); }
                    }
                }
                return instance;
            }
        }

        public List<Offre> ListOffres { get; set; }
        public Offre Offre { get; set; }
        public string Title { get; set; }
        public string TypePosteTitle { get; set; }
        public string TypeContratTitle { get; set; }
        public string RegionName { get; set; }
        public string PublicationDate { get; set; }
        public string LastEditionDate { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }
    }
}
