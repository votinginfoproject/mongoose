using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    /// <summary>
    /// 
    /// </summary>
    public class PollingPlaceCollection : Collection<PollingPlace>, ICreateable
    {
        /// <summary>
        /// Populates itself with a database data reader.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate itself with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
        public void Create(DbDataReader oDataReader)
        {
            PollingPlace oPollingPlace;

            if (oDataReader.HasRows)
            {
                while (oDataReader.Read())
                {
                    oPollingPlace = new PollingPlace();
                    oPollingPlace.Create(oDataReader);
                    this.Add(oPollingPlace);
                }
            }
        }
    }
}
