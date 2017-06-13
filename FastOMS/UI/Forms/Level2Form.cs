using FastOMS.UI.Interfaces;
using System;
using System.IO;
using System.Xml;
using MktSrvcAPI;
using System.Threading;

namespace FastOMS.UI
{
    
        public partial class Level2Form : DevExpress.XtraEditors.XtraForm, ILayoutSaveLoader
    {
        public Guid FormID;
        public static int Level2FormCount = -1;
        public InstrInfo[] Instruments;

        public Level2Form()
        {
            InitializeComponent();
            throw new Exception("The null initializer should not be used.  It is only here so the VS designer will work");
        }

        public Level2Form(InstrInfo[] instruments, Guid groupGuid)
        {

            InitializeComponent();
            FormID = groupGuid;
            level2PanelControl.ControlID = groupGuid;
            level2PanelControl.orderEntryControl.ControlID = groupGuid;
            level2PanelControl.timeAndSalesControl.ControlID = groupGuid;
            level2PanelControl.orderEntryControl.Initialize(instruments);
            level2PanelControl.instrumentArray = instruments;

            Text = Utilities.InstrToStr(instruments);
            Instruments = instruments;
            
           // Utilities.log.Info("Level2 Form Created: " + Utilities.InstrToStr(instruments));
        }

        public void LoadLayoutXML(string layoutXML)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(layoutXML)))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Level2Form":
                                if (reader["Location"] != null)
                                {
                                    string[] pointSplit = reader["Location"].Split(',');
                                    DesktopLocation = new System.Drawing.Point(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                if (reader["Size"] != null)
                                {
                                    string[] pointSplit = reader["Size"].Split(',');
                                    Size = new System.Drawing.Size(Convert.ToInt32(pointSplit[0]), Convert.ToInt32(pointSplit[1]));
                                }
                                break;
                            case "Level2PanelControl":
                                level2PanelControl.LoadLayoutXML(reader.ReadSubtree());
                                break;
                        }
                    }
                }
            }
        }

        public string GetLayoutXML()
        {
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.OmitXmlDeclaration = true;
                    using (XmlWriter xw = XmlWriter.Create(sw, settings))
                    {
                        xw.WriteStartElement("Level2Form");
                        xw.WriteAttributeString("Location", this.DesktopLocation.X + "," + this.DesktopLocation.Y);
                        xw.WriteAttributeString("Size", this.Size.Width + "," + this.Size.Height);
                        xw.WriteAttributeString("Instruments", Utilities.InstrArrayToOldOMSString(Instruments));

                        xw.WriteRaw(level2PanelControl.GetLayoutXML());

                        xw.WriteEndElement();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return sw.ToString();
            }
        }

        private void Level2Form_Load(object sender, EventArgs e)
        {
            

            
           
        }

        
    }
}