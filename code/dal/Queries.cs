using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using noi.votinginfoproject.businessentities;

namespace noi.votinginfoproject.dal
{
    /// <summary>
    /// Defines the queries to get the appropriate information
    /// </summary>
    public abstract class Queries
    {
        public const string PollingPlaces = "select id, location_name, line1, city, state, zip, directions, polling_hours from [Table]";
        public const string StreetSegments = "select id, precinct_id, start_house_number, end_house_number, odd_even_both, street_direction, street_name, street_suffix, address_direction, city, state, zip from [Table]";
        public const string Precincts = "select id, name, locality_id, polling_location_id from [Table]";
        public const string PrecinctSplits = "select id, name, precinct_id, polling_location_id from [Table]";
        public const string Localities = "select id, name, state_id, type from [Table]";
    }
}
