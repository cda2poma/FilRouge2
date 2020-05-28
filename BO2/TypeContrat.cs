using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A type of employment contract.
    /// </summary>
    [DataContract]
    public class TypeContrat
    {
        /// <summary>
        /// The contract's name.
        /// </summary>
        [DataMember]
        public string INTITULE { get; set; }
        /// <summary>
        /// The contract kind's internal ID.
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public TypeContrat() { }

        /// <summary>
        /// The constructor to create a new kind of contract object.
        /// </summary>
        /// <param name="id">The identifier of the kind of contract in the database.</param>
        /// <param name="intitule">The contract's name.</param>
        public TypeContrat(int id, string intitule)
        {
            ID = id;
            INTITULE = intitule;
        }

        public override string ToString()
        { return INTITULE; }
    }
}
