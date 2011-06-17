using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    /// <summary>
    /// 
    /// </summary>
    public class PrecinctSplitCollection : Collection<PrecinctSplit>, ICreateable
    {
        /// <summary>
        /// Populates itself with a database data reader.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate itself with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
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
