using System;
using System.Data.Common;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class PrecinctSplit : ICreateable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrecinctId { get; set; }
        public int PollingLocationId { get; set; }

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

            Name = oDataReader["name"].ToString();

            if (Int32.TryParse(oDataReader["precinct_id"].ToString(), out iId))
            {
                PrecinctId = iId;
            }
            else
            {
                throw new InvalidCastException("Error converting locality id to an integer");
            }

            if (Int32.TryParse(oDataReader["polling_location_id"].ToString(), out iId))
            {
                PollingLocationId = iId;
            }
            else
            {
                throw new InvalidCastException("Error converting polling location id to an integer");
            }
        }
    }
}
