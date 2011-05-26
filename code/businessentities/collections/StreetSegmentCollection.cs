using System.Data.Common;
using System.Collections.ObjectModel;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.businessentities
{
    public class StreetSegmentCollection : Collection<StreetSegment>, ICreateable
    {

        public void Create(DbDataReader oDataReader)
        {
            StreetSegment oStreetSegment;

            if (oDataReader.HasRows)
            {
                while (oDataReader.Read())
                {
                    oStreetSegment = new StreetSegment();
                    oStreetSegment.Create(oDataReader);
                    this.Add(oStreetSegment);
                }
            }
        }
    }
}
