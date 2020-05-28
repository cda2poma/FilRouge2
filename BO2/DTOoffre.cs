using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// The data transfer object that allows to transfer both a job offer added to the database and to indicate whether the kind of job is newly assigned.
    /// </summary>
    [DataContract]
    public class DTOoffre
    {
        /// <summary>
        /// The job offer to transfer to the client.
        /// </summary>
        [DataMember]
        public Offre OffreToTransfer { get; set; }
        /// <summary>
        /// Whether the kind of job is newly assignated or not.
        /// </summary>
        [DataMember]
        public bool TypePosteAssignationAffected { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public DTOoffre() { }

        /// <summary>
        /// The constructor to transfer only one job offer.
        /// </summary>
        /// <param name="offreToTransfer">The job offer to transfer.</param>
        /// <param name="newlyAssignated">Whether the kind of job of the new offer is newly assignated.</param>
        public DTOoffre(Offre offreToTransfer, bool assignationData)
        {
            OffreToTransfer = offreToTransfer;
            TypePosteAssignationAffected = assignationData;
        }
    }
}
