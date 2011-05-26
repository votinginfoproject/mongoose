using System;
using System.Configuration;
using System.Collections.Specialized;
using noi.votinginfoproject.bll;

namespace Builder {

    class Program {

        public static void Main(String[] args) {
            DateTime oScriptStart = DateTime.UtcNow;
            NameValueCollection oVipSettings = new NameValueCollection();
            NameValueCollection oXmlSettings = ConfigurationManager.GetSection("xmlWriterSettings") as NameValueCollection;            
            //NameValueCollection oElectionAdminSettings = ConfigurationManager.GetSection("electionOfficial") as NameValueCollection;

            oVipSettings.Add("ScriptStart", oScriptStart.ToString("yyyy-MM-ddTHH:mm:ss"));
            oVipSettings.Add(ConfigurationManager.GetSection("vipSettings") as NameValueCollection);

            FeedWriter oFeedWriter = new FeedWriter(
                oVipSettings.Get("FilePath"),
                oVipSettings.Get("StateFIPS"),
                oXmlSettings
            );

            oFeedWriter.WriteStartElement("vip_object");

            oFeedWriter.WriteHeader(oVipSettings);

            // this may not be necessary if the election_officials are stored in the db
            //oFeedWriter.WriteElementFromConfig("election_official", oElectionAdminSettings);
            
            try {

                    oFeedWriter.WritePollingPlaces();
                    oFeedWriter.WriteStreetSegments();
                    oFeedWriter.WritePrecincts();
		            oFeedWriter.WritePrecinctSplits();
                    oFeedWriter.WriteLocalities();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }

          
            oFeedWriter.WriteEndElement();
        }
    }
}
