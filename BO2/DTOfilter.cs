using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Object created in the client side BLL and sent to the server side BLL to filter SQL requests.
    /// </summary>
    [DataContract]
    public class DTOfilter
    {
        /// <summary>
        /// Check if the offer's title contains what's passed in this argument.
        /// </summary>
        [DataMember]
        public string TITRE { get; set; }
        /// <summary>
        /// Check if the offer's description text contains what's passed in this argument.
        /// </summary>
        [DataMember]
        public string DESC { get; set; }
        /// <summary>
        /// Check if the offer's kind of job identifier is what's passed in this argument.
        /// </summary>
        [DataMember]
        public int IDTYPEPOSTE { get; set; }
        /// <summary>
        /// Check if the offer's kind of contract identifier is what's passed in this argument.
        /// </summary>
        [DataMember]
        public int IDTYPECONTRAT { get; set; }
        /// <summary>
        /// Check if the offer's region identifier is what's passed in this argument.
        /// </summary>
        [DataMember]
        public int IDREGION { get; set; }
        /// <summary>
        /// Check if the offer's publication date is no earlier than what's being passed in this argument.
        /// </summary>
        [DataMember]
        public DateTime DATEPUBLICATIONMIN { get; set; }
        /// <summary>
        /// Check if the offer's publication date is no later than what's being passed in this argument.
        /// </summary>
        [DataMember]
        public DateTime DATEPUBLICATIONMAX { get; set; }
        /// <summary>
        /// How the description text must be handled: at least one word if negavite, all words if zero, exact expression if positive.
        /// </summary>
        [DataMember]
        public int DescConfig { get; set; }
        /// <summary>
        /// The order by which the results must be ordered.
        /// </summary>
        [DataMember]
        public FilterOrderObject FilterOrder { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public DTOfilter()
        { }

        public DTOfilter(string titre, string desc, int idtypeposte, int idtypecontrat, int idregion, DateTime datepublicationmin, DateTime datepublicationmax, int descconfig, FilterOrderObject filterOrder)
        {
            TITRE = titre;
            DESC = desc;
            IDTYPEPOSTE = idtypeposte;
            IDTYPECONTRAT = idtypecontrat;
            IDREGION = idregion;
            DATEPUBLICATIONMIN = datepublicationmin;
            DATEPUBLICATIONMAX = datepublicationmax;
            DescConfig = descconfig;
            FilterOrder = filterOrder;
        }

        public static bool operator ==(DTOfilter f1, DTOfilter f2)
        {
            if ((object)f1 == null)
                return (object)f2 == null;

            return f1.Equals(f2);
        }

        public static bool operator !=(DTOfilter f1, DTOfilter f2)
        { return !(f1 == f2); }


        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            { return false; }
            else
            {
                DTOfilter compare = (DTOfilter)obj;
                return TITRE == compare.TITRE && DESC == compare.DESC && IDTYPEPOSTE == compare.IDTYPEPOSTE && IDTYPECONTRAT == compare.IDTYPECONTRAT && IDREGION == compare.IDREGION && DATEPUBLICATIONMIN == compare.DATEPUBLICATIONMIN && DATEPUBLICATIONMAX == compare.DATEPUBLICATIONMAX && DescConfig == compare.DescConfig && FilterOrder == compare.FilterOrder;
            }
        }

        public override int GetHashCode()
        { return base.GetHashCode(); }
    }
}
