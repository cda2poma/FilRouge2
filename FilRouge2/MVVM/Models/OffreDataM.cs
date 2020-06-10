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

        private OffreDataM()
        { ListOffres = new List<Offre>(); }

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
        public TypePoste TypePoste { get; set; }
        public string TypeContratTitle { get; set; }
        public string RegionName { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime LastEditionDate { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }

        public bool ListOffresSuscribed { get; set; }

        public bool ViewingSingleOffre { get; set; }

        public string GetStringFromDate(DateTime date)
        {
            StringBuilder sb = new StringBuilder($"{Convert.ToString(date.Day)} ");
            switch (date.Month)
            {
                case 1:
                    sb.Append("janvier ");
                    break;
                case 2:
                    sb.Append("février ");
                    break;
                case 3:
                    sb.Append("mars ");
                    break;
                case 4:
                    sb.Append("avril ");
                    break;
                case 5:
                    sb.Append("mai ");
                    break;
                case 6:
                    sb.Append("juin ");
                    break;
                case 7:
                    sb.Append("juillet ");
                    break;
                case 8:
                    sb.Append("août ");
                    break;
                case 9:
                    sb.Append("septembre ");
                    break;
                case 10:
                    sb.Append("octobre ");
                    break;
                case 11:
                    sb.Append("novembre ");
                    break;
                case 12:
                    sb.Append("décembre ");
                    break;
            }
            sb.Append(Convert.ToString(date.Year));
            return sb.ToString();
        }

        public string GetStringFromDate(DateTime? date)
        {
            if (date == null)
            { return ""; }
            else
            { return GetStringFromDate((DateTime)date); }
        }
    }
}
