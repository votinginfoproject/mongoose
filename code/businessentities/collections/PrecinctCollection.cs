using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class PrecinctCollection : Collection<Precinct>, ICreateable
    {

        public void Create(DbDataReader oDataReader)
        {
            Precinct oPrecinct;

            if (oDataReader.HasRows)
            {
                while (oDataReader.Read())
                {
                    oPrecinct = new Precinct();
                    oPrecinct.Create(oDataReader);
                    this.Add(oPrecinct);
                }
            }
        }
    }
}
