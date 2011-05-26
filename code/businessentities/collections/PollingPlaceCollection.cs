using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class PollingPlaceCollection : Collection<PollingPlace>, ICreateable
    {

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
