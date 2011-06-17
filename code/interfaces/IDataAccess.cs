using System.Collections;
using System.Data.SqlClient;
using noi.votinginfoproject.businessentities;

namespace noi.votinginfoproject.interfaces
{

    /// <summary>
    /// Interface all data access classes in the application should implement.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Generic method that returns an object based on the records from a database query.
        /// </summary>
        /// <typeparam name="T">An object that implements <see cref="IEnumerable"/> and <see cref="ICreateable"/> and can be instantiated.</typeparam>
        /// <param name="cmd">Database object that contains the records to create the return object with.</param>
        T GetCollectionFromCommand<T>(SqlCommand cmd)
            where T : IEnumerable, ICreateable, new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PollingPlaceCollection GetPollingPlaces();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        StreetSegmentCollection GetStreetSegments();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PrecinctCollection GetPrecincts();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        PrecinctSplitCollection GetPrecinctSplits();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        LocalityCollection GetLocalities();
    }
}
