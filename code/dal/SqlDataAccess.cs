using System;
using System.Collections;
using System.Data.SqlClient;
using noi.votinginfoproject.interfaces;
using noi.votinginfoproject.businessentities;

namespace noi.votinginfoproject.dal
{
    /// <summary>
    /// The SqlDataAccess class is responsible for interacting with a SQL Server database to retreive objects to populate the feed with.
    /// </summary>
    public class SqlDataAccess : IDataAccess
    {
        private string _sConnString;

        /// <summary>
        /// 
        /// </summary>
        public PollingPlaceCollection GetPollingPlaces()
        {
            SqlCommand oCmd = new SqlCommand(Queries.PollingPlaces, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<PollingPlaceCollection>(oCmd);
        }

        /// <summary>
        /// 
        /// </summary>
        public StreetSegmentCollection GetStreetSegments()
        {
            SqlCommand oCmd = new SqlCommand(Queries.StreetSegments, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<StreetSegmentCollection>(oCmd);
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public LocalityCollection GetLocalities()
        {
            SqlCommand oCmd = new SqlCommand(Queries.Localities, new SqlConnection(_sConnString));

            return GetCollectionFromCommand<LocalityCollection>(oCmd);
        }

        /// <summary>
        /// Generic method that returns an object based on the records from a database query.
        /// </summary>
        /// <typeparam name="T">An object that implements <see cref="IEnumerable"/> and <see cref="ICreateable"/> and can be instantiated.</typeparam>
        /// <param name="cmd">Database object that contains the records to create the return object with.</param>
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

        /// <summary>
        /// Constructor for the class to set the connection string.
        /// </summary>
        public SqlDataAccess(string sConnString)
        {
            _sConnString = sConnString;
        }
    }


}
