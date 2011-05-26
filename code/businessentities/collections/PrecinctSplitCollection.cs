using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class PrecinctSplitCollection : Collection<PrecinctSplit>, ICreateable
    {

        public void Create(DbDataReader oDataReader)
        {
            PrecinctSplit oPrecinct;

            if (oDataReader.HasRows)
            {
                while (oDataReader.Read())
                {
                    oPrecinct = new PrecinctSplit();
                    oPrecinct.Create(oDataReader);
                    this.Add(oPrecinct);
                }
            }
        }
    }
}
