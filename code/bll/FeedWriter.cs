using System;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using noi.votinginfoproject.dal;
using noi.votinginfoproject.businessentities;
using noi.votinginfoproject.interfaces;

namespace noi.votinginfoproject.bll
{
    public class FeedWriter
    {
        private string _sOutputFileNameFormat = "{0}vipFeed-{1}.xml";
        private XmlWriter _oXmlWriter;
        private IDataAccess _oDataAccess;

        #region XML Methods

        private XmlWriter CreateXmlWriter(NameValueCollection oXmlConfig, String sOutputFile)
        {
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = Boolean.Parse(oXmlConfig.Get("Indent"));
            xmlSettings.IndentChars = oXmlConfig.Get("IndentChars");
            xmlSettings.ConformanceLevel = oXmlConfig.Get("ConformanceLevel") == "Fragment" ? ConformanceLevel.Fragment : ConformanceLevel.Document;

            return XmlWriter.Create(sOutputFile, xmlSettings);
        }

        private string FormatFileName(string sFilePath, string sFIPS)
        {
            return String.Format(_sOutputFileNameFormat, sFilePath, sFIPS);
        }

        public void WriteStartElement(string sElementName)
        {
            _oXmlWriter.WriteStartElement(sElementName);
        }

        public void WriteEndElement()
        {
            _oXmlWriter.WriteEndElement();
            _oXmlWriter.Flush();
        }

        public void WriteHeader(NameValueCollection oVipConfig)
        {
            _oXmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            _oXmlWriter.WriteAttributeString("xsi", "noNamespaceSchemaLocation", null, oVipConfig.Get("SchemaURL"));
            _oXmlWriter.WriteAttributeString("schemaVersion", oVipConfig.Get("SchemaVer"));

            WriteStartElement("state");
            _oXmlWriter.WriteAttributeString("id", oVipConfig.Get("StateFIPS"));
            _oXmlWriter.WriteElementString("name", oVipConfig.Get("StateName"));
            WriteEndElement();

            WriteStartElement("source");
            _oXmlWriter.WriteAttributeString("id", "1");
            _oXmlWriter.WriteElementString("vip_id", oVipConfig.Get("StateFIPS"));
            _oXmlWriter.WriteElementString("name", oVipConfig.Get("SourceName"));
            _oXmlWriter.WriteElementString("datetime", oVipConfig.Get("ScriptStart"));
            _oXmlWriter.WriteElementString("description", oVipConfig.Get("Description"));
            _oXmlWriter.WriteElementString("organization_url", oVipConfig.Get("OrganizationURL"));
            WriteEndElement();
        }

        public void WriteElementFromConfig(String sElementName, NameValueCollection oConfig)
        {
            WriteStartElement(sElementName);

            try
            {
                _oXmlWriter.WriteAttributeString("id", oConfig.Get("id"));
                // clear the id so it's not used in the loop
                oConfig.Remove("id");
            }
            catch (ArgumentOutOfRangeException)
            {
                // skip it
            }

            foreach (String s in oConfig.AllKeys)
            {
                _oXmlWriter.WriteElementString(s, oConfig[s]);
            }

            WriteEndElement();
        }

        #endregion

        public void WritePollingPlaces()
        {
            PollingPlaceCollection oPollingPlaces = _oDataAccess.GetPollingPlaces();

            foreach (PollingPlace oPollingPlace in oPollingPlaces)
            {
                WriteStartElement("polling_location");

                _oXmlWriter.WriteAttributeString("id", oPollingPlace.Id.ToString());

                WriteStartElement("address");
                if (!String.IsNullOrEmpty(oPollingPlace.LocationName))
                {
                    _oXmlWriter.WriteElementString("location_name", oPollingPlace.LocationName);
                }
                _oXmlWriter.WriteElementString("line1", oPollingPlace.Address.Line1);
                _oXmlWriter.WriteElementString("city", oPollingPlace.Address.City);
                _oXmlWriter.WriteElementString("state", oPollingPlace.Address.State);
                _oXmlWriter.WriteElementString("zip", oPollingPlace.Address.Zip);
                WriteEndElement();

                if (!String.IsNullOrEmpty(oPollingPlace.Directions))
                {
                    _oXmlWriter.WriteElementString("directions", oPollingPlace.Directions);
                }
                if (!String.IsNullOrEmpty(oPollingPlace.PollingHours))
                {
                    _oXmlWriter.WriteElementString("polling_hours", oPollingPlace.PollingHours);
                }

                WriteEndElement();
            }
        }

        public void WriteStreetSegments()
        {
            StreetSegmentCollection oStreetSegments = _oDataAccess.GetStreetSegments();

            foreach (StreetSegment oStreetSegment in oStreetSegments)
            {
                WriteStartElement("street_segment");

                _oXmlWriter.WriteAttributeString("id", oStreetSegment.Id.ToString());

                _oXmlWriter.WriteElementString("start_house_number", oStreetSegment.StartHouseNumber.ToString());
                _oXmlWriter.WriteElementString("end_house_number", oStreetSegment.EndHouseNumber.ToString());

                if (!string.IsNullOrEmpty(oStreetSegment.OddEvenBoth))
                {
                    _oXmlWriter.WriteElementString("odd_even_both", oStreetSegment.OddEvenBoth);
                }
                
                WriteStartElement("non_house_address");
                if (!string.IsNullOrEmpty(oStreetSegment.StreetDirection))
                {
                    _oXmlWriter.WriteElementString("street_direction", oStreetSegment.StreetDirection);
                }
                _oXmlWriter.WriteElementString("street_name", oStreetSegment.StreetName);
                if (!string.IsNullOrEmpty(oStreetSegment.StreetSuffix))
                {
                    _oXmlWriter.WriteElementString("street_suffix", oStreetSegment.StreetSuffix);
                }
                if (!string.IsNullOrEmpty(oStreetSegment.AddressDirection))
                {
                    _oXmlWriter.WriteElementString("address_direction", oStreetSegment.AddressDirection);
                }
                _oXmlWriter.WriteElementString("state", oStreetSegment.Address.State);
                _oXmlWriter.WriteElementString("city", oStreetSegment.Address.City);
                if (!string.IsNullOrEmpty(oStreetSegment.Address.Zip))
                {
                    _oXmlWriter.WriteElementString("zip", oStreetSegment.Address.Zip);
                }

                WriteEndElement(); // end non_house_address
                
                _oXmlWriter.WriteElementString("precinct_id", oStreetSegment.PrecinctId.ToString());

                WriteEndElement(); // end street_segment
            }
        }

        public void WritePrecincts()
        {
            PrecinctCollection oPrecincts = _oDataAccess.GetPrecincts();

            foreach (Precinct oPrecinct in oPrecincts)
            {
                WriteStartElement("precinct");

                _oXmlWriter.WriteAttributeString("id", oPrecinct.Id.ToString());
                _oXmlWriter.WriteElementString("name", oPrecinct.Name);
                _oXmlWriter.WriteElementString("locality_id", oPrecinct.LocalityId.ToString());
                _oXmlWriter.WriteElementString("polling_location_id", oPrecinct.PollingLocationId.ToString());

                WriteEndElement();
            }
        }

        public void WritePrecinctSplits()
        {
            PrecinctSplitCollection oPrecinctSplits = _oDataAccess.GetPrecinctSplits();

            foreach (PrecinctSplit oPrecinctSplit in oPrecinctSplits)
            {
                WriteStartElement("precinct_split");

                _oXmlWriter.WriteAttributeString("id", oPrecinctSplit.Id.ToString());
                _oXmlWriter.WriteElementString("name", oPrecinctSplit.Name);
                _oXmlWriter.WriteElementString("precinct_id", oPrecinctSplit.PrecinctId.ToString());
                if (!string.IsNullOrEmpty(oPrecinctSplit.PollingLocationId.ToString()))
                {
                    _oXmlWriter.WriteElementString("polling_location_id", oPrecinctSplit.PollingLocationId.ToString());
                }

                WriteEndElement();
            }
        }

        public void WriteLocalities()
        {
            LocalityCollection oLocalities = _oDataAccess.GetLocalities();

            foreach (Locality oLocality in oLocalities)
            {
                WriteStartElement("locality");

                _oXmlWriter.WriteAttributeString("id", oLocality.Id.ToString());
                _oXmlWriter.WriteElementString("name", oLocality.Name);
                _oXmlWriter.WriteElementString("state_id", oLocality.StateId.ToString());
                _oXmlWriter.WriteElementString("type", oLocality.Type);

                WriteEndElement();
            }
        }

        public FeedWriter(string sFilePath, string sFIPS, NameValueCollection oWriterSettings)
        {
            _oXmlWriter = CreateXmlWriter(oWriterSettings, FormatFileName(sFilePath, sFIPS));
            switch (ConfigurationManager.ConnectionStrings["VotingInfoProject"].ProviderName)
            {
                case "System.Data.SqlClient":
                    _oDataAccess = new SqlDataAccess(ConfigurationManager.ConnectionStrings["VotingInfoProject"].ConnectionString);
                    break;
                default:
                    _oDataAccess = new SqlDataAccess(ConfigurationManager.ConnectionStrings["VotingInfoProject"].ConnectionString);
                    break;
            }
            
        }
    }
}
