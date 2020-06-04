using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A job offer.
    /// </summary>
    [DataContract]
    public class Offre
    {
        /// <summary>
        /// The job offer's internal ID.
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// The job's title.
        /// </summary>
        [DataMember]
        public string TITRE { get; set; }
        /// <summary>
        /// A description of the job presented by the offer.
        /// </summary>
        [DataMember]
        public string TEXTEDESC { get; set; }
        /// <summary>
        /// The offer's type of job.
        /// </summary>
        [DataMember]
        public TypePoste TYPEPOSTE { get; set; }
        /// <summary>
        /// The offer's type of contract.
        /// </summary>
        [DataMember]
        public TypeContrat TYPECONTRAT { get; set; }
        /// <summary>
        /// The offer's geographical region.
        /// </summary>
        [DataMember]
        public RegionFrancaise REGION { get; set; }
        /// <summary>
        /// The offer's date of publication.
        /// </summary>
        [DataMember]
        public DateTime DATEPUBLICATION { get; set; }
        /// <summary>
        /// The offer's date of last edit, if edited at least once.
        /// </summary>
        [DataMember]
        public DateTime? DATEDERNIEREMAJ { get; set; }
        /// <summary>
        /// URL of the offer.
        /// </summary>
        [DataMember]
        public string LIENWEB { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public Offre() { }

        /// <summary>
        /// The constructor to create a new job offer object.
        /// </summary>
        public Offre(int id, string titre, string textedesc, TypePoste typeposte, TypeContrat typecontrat, RegionFrancaise region, DateTime datepublication, DateTime? datedernieremaj, string lienweb)
        {
            ID = id;
            TITRE = titre;
            TEXTEDESC = textedesc;
            TYPEPOSTE = typeposte;
            TYPECONTRAT = typecontrat;
            REGION = region;
            DATEPUBLICATION = datepublication;
            DATEDERNIEREMAJ = datedernieremaj;
            LIENWEB = lienweb;
        }

        public override string ToString()
        { return TITRE; }

        public static bool operator ==(Offre offre1, Offre offre2)
        {
            if (offre1 == null)
            { return offre2 == null; }
            return offre1.Equals(offre2);
        }

        public static bool operator !=(Offre offre1, Offre offre2)
        { return !(offre1 == offre2); }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            { return false; }
            else
            {
                Offre compare = (Offre)obj;
                return ID == compare.ID && TITRE == compare.TITRE && TEXTEDESC == compare.TEXTEDESC && TYPEPOSTE.ID == compare.TYPEPOSTE.ID
                    && TYPECONTRAT.ID == compare.TYPECONTRAT.ID && REGION.ID == compare.REGION.ID && DATEPUBLICATION == compare.DATEPUBLICATION
                    && DATEDERNIEREMAJ == compare.DATEDERNIEREMAJ && LIENWEB == compare.LIENWEB;
            }
        }

        public override int GetHashCode()
        { return base.GetHashCode(); }
    }
}
