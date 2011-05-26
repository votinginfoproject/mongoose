using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class LocalityCollection : Collection<Locality>, ICreateable
    {

        public void Create(DbDataReader oDataReader)
        {
            Locality oLocality;

            if (oDataReader.HasRows)
            {
                while (oDataReader.Read())
                {
                    oLocality = new Locality();
                    oLocality.Create(oDataReader);
                    this.Add(oLocality);
                }
            }
        }
    }
}
