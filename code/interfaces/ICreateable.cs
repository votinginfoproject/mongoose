using System.Data.Common;

namespace noi.votinginfoproject.interfaces
{
    public interface ICreateable
    {
        /// <summary>
        /// Populate properties with records from a data reader.
        /// </summary>
        /// <param name="oDataReader">Data reader that holds the elements to populate an object with. Uses the abstract class <see cref="DbDataReader"/> to work with data readers for different DBMSs.</param>
        void Create(DbDataReader oDataReader);
    }
}
