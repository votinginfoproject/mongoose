using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    /// <summary>
    /// 
    /// </summary>
    public class PrecinctCollection : Collection<Precinct>, ICreateable
    {
        /// <summary>
        /// Populates itself with a database data reader.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate itself with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
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
