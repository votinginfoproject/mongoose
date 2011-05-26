using System;
using System.Collections;
using System.Data.SqlClient;
using noi.votinginfoproject.interfaces;
using noi.votinginfoproject.businessentities;

namespace noi.votinginfoproject.dal
{
    public class SqlDataAccess : IDataAccess
    {
        private string _sConnString;

        public PollingPlaceCollection GetPollingPlaces()
        {
            SqlCommand oCmd = new SqlCommand(Queries.PollingPlaces, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<PollingPlaceCollection>(oCmd);
        }

        public StreetSegmentCollection GetStreetSegments()
        {
            SqlCommand oCmd = new SqlCommand(Queries.StreetSegments, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<StreetSegmentCollection>(oCmd);
        }

        public PrecinctCollection GetPrecincts()
        {
            SqlCommand oCmd = new SqlCommand(Queries.Precincts, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<PrecinctCollection>(oCmd);
        }

        public PrecinctSplitCollection GetPrecinctSplits()
        {
            SqlCommand oCmd = new SqlCommand(Queries.PrecinctSplits, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<PrecinctSplitCollection>(oCmd);
        }

        public LocalityCollection GetLocalities()
        {
            SqlCommand oCmd = new SqlCommand(Queries.Localities, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<LocalityCollection>(oCmd);
        }

        public T GetCollectionFromCommand<T>(SqlCommand cmd)
            where T : IEnumerable, ICreateable, new()
        {
            T oItem = new T();

            using (cmd.Connection)
            {
                cmd.Connection.Open();

                using (SqlDataReader oDataReader = cmd.ExecuteReader())
                {
                    if (oDataReader.HasRows)
                    {
                        oItem.Create(oDataReader);
                    }
                }
            }

            return oItem;
        }

        public SqlDataAccess(string sConnString)
        {
            _sConnString = sConnString;
        }
    }


}
