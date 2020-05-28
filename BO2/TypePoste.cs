using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A type of job.
    /// </summary>
    [DataContract]
    public class TypePoste
    {
        /// <summary>
        /// The job's name.
        /// </summary>
        [DataMember]
        public string INTITULE { get; set; }
        /// <summary>
        /// The job kind's internal ID.
        /// </summary>
        [DataMember]
        public int ID { get; set; }
        /// <summary>
        /// Whether there's at least one job offer with 
        /// </summary>
        [DataMember]
        public bool Assignated { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public TypePoste() { }

        /// <summary>
        /// Creation of a kind of job object.
        /// </summary>
        /// <param name="id">The identifier of the kind of job in the database.</param>
        /// <param name="intitule">The job's name.</param>
        public TypePoste(int id, string intitule, bool assignated)
        {
            ID = id;
            INTITULE = intitule;
            Assignated = assignated;
        }

        public override string ToString()
        { return INTITULE; }
    }
}
