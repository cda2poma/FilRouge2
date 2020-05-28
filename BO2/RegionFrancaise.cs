using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A geographical region of France.
    /// </summary>
    [DataContract]
    public class RegionFrancaise
    {
        /// <summary>
        /// The region's name.
        /// </summary>
        [DataMember]
        public string NOM { get; set; }
        /// <summary>
        /// The region's internal ID.
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public RegionFrancaise() { }

        /// <summary>
        /// The constructor to create a new region object.
        /// </summary>
        /// <param name="id">The region's ID</param>
        /// <param name="nom">The region's name</param>
        public RegionFrancaise(int id, string nom)
        {
            ID = id;
            NOM = nom;
        }

        public override string ToString()
        { return NOM; }
    }
}
