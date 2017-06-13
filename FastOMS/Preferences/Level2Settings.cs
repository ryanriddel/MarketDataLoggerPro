using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FastOMS.Preferences
{
    class Level2Settings
    {
        public bool Level2CopyPrice { get; set; } = true;
        public bool Level2CopySize { get; set; } = true;
        public bool Level2CopyDest { get; set; } = true;

        public bool ShowOrderFlyout { get; set; } = true;

        public Level2Settings()
        {

        }
        
        public Level2Settings(string xml)
        {

        }

        public void LoadFromXML(XmlReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Level2Settings":
                                if (reader["ShowOrderFlyout"] != null)
                                {
                                    ShowOrderFlyout = reader["ShowOrderFlyout"].Equals("True");
                                }
                                break;
                            case "Lvl2SelectionCopy":
                                if (reader["Price"] != null)
                                {
                                    Level2CopyPrice = reader["Price"].Equals("True");
                                }
                                if (reader["Size"] != null)
                                {
                                    Level2CopySize = reader["Size"].Equals("True");
                                }
                                if (reader["Dest"] != null)
                                {
                                    Level2CopyDest = reader["Dest"].Equals("True");
                                }
                                break;
                        }
                    }
                }
            }
        }

        public string GetXML()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                settings.OmitXmlDeclaration = true;
                using (XmlWriter xw = XmlWriter.Create(sw, settings))
                {
                    xw.WriteStartElement("Level2Settings");

                    xw.WriteAttributeString("ShowOrderFlyout", "" + ShowOrderFlyout);

                    xw.WriteStartElement("Lvl2SelectionCopy");
                    xw.WriteAttributeString("Price", "" + Level2CopyPrice);
                    xw.WriteAttributeString("Size", "" + Level2CopySize);
                    xw.WriteAttributeString("Dest", "" + Level2CopyDest);
                    xw.WriteEndElement();

                    xw.WriteEndElement();
                }
                return sw.ToString();
            }
        }

        private void SaveDefaultLayoutXML()
        {
            File.WriteAllText("DefaultLevel2Settings.xml", GetXML());
        }
    }
}
