using System;
using System.Data.Common;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    /// <summary>
    /// 
    /// </summary>
    public class Locality : ICreateable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Assigns its properties with a record from a database.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate itself with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
        public void Create(DbDataReader oDataReader)
        {
            int iId;

            if (Int32.TryParse(oDataReader["id"].ToString(), out iId))
            {
                Id = iId;
            }
            else
            {
                throw new InvalidCastException("Error converting id to an integer");
            }

            if (oDataReader["name"] != DBNull.Value)
            {
                Name = oDataReader["name"].ToString();
            }
            else
            {
                throw new NullReferenceException("Name is null");
            }

            if (Int32.TryParse(oDataReader["state_id"].ToString(), out iId))
            {
                StateId = iId;
            }
            else
            {
                throw new InvalidCastException("Error converting state id to an integer");
            }

            Type = oDataReader["type"].ToString();
        }
    }
}
