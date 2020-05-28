using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Object created in the BLL client-side
    /// </summary>
    [DataContract]
    public class FilterOrderObject
    {
        /// <summary>
        /// The description that appears in the ComboBox.
        /// </summary>
        [DataMember]
        public string Desc { get; set; }
        /// <summary>
        /// The number of the column affected in the SQL query.
        /// </summary>
        [DataMember]
        public int ColumnNumber { get; set; }
        /// <summary>
        /// True if ASC, false if DESC.
        /// </summary>
        [DataMember]
        public bool Asc { get; set; }

        /// <summary>
        /// Default constructor for serialization.
        /// </summary>
        public FilterOrderObject() { }

        /// <summary>
        /// The constructor of a filter order object.
        /// </summary>
        /// <param name="desc">The description that appears in the ComboBox.</param>
        /// <param name="name">The name of the column affected in the SQL query.</param>
        /// <param name="asc">True if ASC, false if DESC.</param>
        public FilterOrderObject(string desc, int columnNumber, bool asc)
        {
            Desc = desc;
            ColumnNumber = columnNumber;
            Asc = asc;
        }

        /*public static bool operator ==(FilterOrderObject f1, FilterOrderObject f2)
        {
            if ((object)f1 == null)
                return (object)f2 == null;

            return f1.Equals(f2);
        }

        public static bool operator !=(FilterOrderObject f1, FilterOrderObject f2)
        { return !(f1 == f2); }


        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            { return false; }
            else
            {
                FilterOrderObject compare = (FilterOrderObject)obj;
                return ColumnNumber == compare.ColumnNumber && Asc == compare.Asc;
            }
        }

        public override int GetHashCode()
        { return base.GetHashCode(); }*/

        public override string ToString()
        { return Desc; }
    }

}
