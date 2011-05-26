using System;
using System.Data.Common;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class PollingPlace : ICreateable
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public Address Address { get; set; }
        public string Directions { get; set; }
        public string PollingHours { get; set; }

        public PollingPlace()
        {
            Address = new Address();
        }

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

            if (oDataReader["location_name"] != DBNull.Value)
            {
                LocationName = oDataReader["location_name"].ToString();
            }

            Address.Line1 = oDataReader["line1"].ToString();
            Address.City = oDataReader["city"].ToString();
            Address.State = oDataReader["state"].ToString();
            Address.Zip = oDataReader["zip"].ToString();

            if (oDataReader["directions"] != DBNull.Value)
            {
                Directions = oDataReader["directions"].ToString();
            }

            if (oDataReader["polling_hours"] != DBNull.Value)
            {
                PollingHours = oDataReader["polling_hours"].ToString();
            }
        }
    }
}
