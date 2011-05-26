using System.Collections;
using System.Data.SqlClient;
using noi.votinginfoproject.businessentities;

namespace noi.votinginfoproject.interfaces
{
    public interface IDataAccess
    {

        T GetCollectionFromCommand<T>(SqlCommand cmd)
            where T : IEnumerable, ICreateable, new();

        PollingPlaceCollection GetPollingPlaces();

        StreetSegmentCollection GetStreetSegments();

        PrecinctCollection GetPrecincts();

        PrecinctSplitCollection GetPrecinctSplits();

        LocalityCollection GetLocalities();
    }
}
