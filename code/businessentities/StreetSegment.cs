using System;
using System.Data.Common;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    /// <summary>
    /// 
    /// </summary>
    public class StreetSegment : ICreateable
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public string OddEvenBoth { get; set; }
        public string StreetDirection { get; set; }
        public string StreetName { get; set; }
        public string StreetSuffix { get; set; }
        public string AddressDirection { get; set; }
        public int PrecinctId { get; set; }
        private int _iStartHouseNumber { get; set; }
        private int _iEndHouseNumber { get; set; }

        public int StartHouseNumber { 
            get {
                if (_iStartHouseNumber == default(int)) {
                    return 1;
                }
                else {
                    return _iStartHouseNumber;
                }
            }
            set {
                _iStartHouseNumber = value;
            }
        }

        public int EndHouseNumber { 
            get {
                if (_iEndHouseNumber == default(int)) {
                    return 999999;
                }
                else {
                    return _iEndHouseNumber;
                }
            }
            set {
                _iEndHouseNumber = value;
            }
        }

        public StreetSegment()
        {
            Address = new Address();
        }

        /// <summary>
        /// Assigns its properties with a record from a database.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate itself with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
        public void Create(DbDataReader oDataReader)
        {
            int iId;
            int iHouseNumber;

            if (oDataReader["precinct_id"] != DBNull.Value && Int32.TryParse(oDataReader["precinct_id"].ToString(), out iId))
            {
                PrecinctId = iId;

                if (Int32.TryParse(oDataReader["id"].ToString(), out iId))
                {
                    Id = iId;
                }
                else
                {
                    throw new InvalidCastException("Error converting id to an integer");
                }

                if (Int32.TryParse(oDataReader["start_house_number"].ToString(), out iHouseNumber))
                {
                    StartHouseNumber = iHouseNumber;
                }
                else
                {
                    throw new InvalidCastException("Error converting start house number to an integer");
                }

                if (Int32.TryParse(oDataReader["end_house_number"].ToString(), out iHouseNumber))
                {
                    EndHouseNumber = iHouseNumber;
                }
                else
                {
                    throw new InvalidCastException("Error converting end house number to an integer");
                }

                if (oDataReader["odd_even_both"] != DBNull.Value)
                {
                    OddEvenBoth = oDataReader["odd_even_both"].ToString();
                }

                if (oDataReader["street_direction"] != DBNull.Value)
                {
                    StreetDirection = oDataReader["street_direction"].ToString();
                }

                StreetName = oDataReader["street_name"].ToString();

                if (oDataReader["street_suffix"] != DBNull.Value)
                {
                    AddressDirection = oDataReader["street_suffix"].ToString();
                }

                if (oDataReader["address_direction"] != DBNull.Value)
                {
                    AddressDirection = oDataReader["address_direction"].ToString();
                }

                Address.City = oDataReader["city"].ToString();
                Address.State = oDataReader["state"].ToString();

                if (oDataReader["zip"] != DBNull.Value)
                {
                    Address.Zip = oDataReader["zip"].ToString();
                }
            }
        }
    }
}
