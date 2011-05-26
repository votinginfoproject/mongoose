using System.Data.Common;

namespace noi.votinginfoproject.interfaces
{
    public interface ICreateable
    {
        void Create(DbDataReader oDataReader);
    }
}
