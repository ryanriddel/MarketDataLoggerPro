using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MktSrvcAPI;
using FastOMS.UI.Controls;
using FastOMS.Data_Structures;
using System.IO;
using System.Xml;
using FastOMS.UI.Interfaces;

namespace FastOMS.UI
{
    public partial class SpreadForm : DevExpress.XtraEditors.XtraForm, ILayoutSaveLoader
    {
        public InstrInfo[] Instruments;

        public SpreadForm()
        {
            InitializeComponent();
            throw new Exception("Very very bad, Mr. Seinfeld.");
        }

        public SpreadForm(InstrInfo[] instruments, Guid groupGuid) 
        {
            InitializeComponent();
            spreadLevel2PanelControl.instrumentArray = instruments;
            spreadLevel2PanelControl.ControlID = groupGuid;
            spreadLevel2PanelControl.InitializeAllChildControls(groupGuid, instruments);
            Hub._marketDataFeed.AddQuoteConsumer(instruments, spreadLevel2PanelControl);

            for (int i = 0; i < instruments.Length; i++)
            {
                Level2PanelControl newPanel = new Level2PanelControl(groupGuid, new InstrInfo[] { instruments[i] });
                newPanel.InitializeAllChildControls(groupGuid, new InstrInfo[] { instruments[i] });
                spreadDockPanel1.DockLevel2PanelControl(newPanel);
                Hub._marketDataFeed.AddQuoteConsumer(new InstrInfo[] { instruments[i] }, newPanel);
                Hub._marketDataFeed.AddTradeConsumer(new InstrInfo[] { instruments[i] }, newPanel);
            }

            InfraConnector<Data_Structures.QuoteBook> cnct = new InfraConnector<Data_Structures.QuoteBook>(
                spreadDockPanel1, spreadLevel2PanelControl);


            Text = Utilities.InstrToStr(instruments);
            Instruments = instruments;

            Utilities.log.Info("Spread Form Created: " + Utilities.InstrToStr(instruments));


        }

        public void LoadLayoutXML(string layoutXML)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(layoutXML)))
            {
                int loadedLevel2Layouts = 0;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "SpreadForm":
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
                                if (loadedLevel2Layouts == 0)
                                {
                                    spreadLevel2PanelControl.LoadLayoutXML(reader.ReadSubtree());
                                }
                                else
                                {
                                    spreadDockPanel1._level2PanelList[loadedLevel2Layouts - 1].LoadLayoutXML(reader.ReadSubtree());
                                }
                                loadedLevel2Layouts++;
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
                        xw.WriteStartElement("SpreadForm");
                        xw.WriteAttributeString("Location", this.DesktopLocation.X + "," + this.DesktopLocation.Y);
                        xw.WriteAttributeString("Size", this.Size.Width + "," + this.Size.Height);
                        xw.WriteAttributeString("Instruments", Utilities.InstrArrayToOldOMSString(Instruments));

                        xw.WriteRaw(spreadLevel2PanelControl.GetLayoutXML());
                        for (int i = 0; i < spreadDockPanel1._level2PanelList.Count; i++)
                        {
                            xw.WriteRaw(spreadDockPanel1._level2PanelList[i].GetLayoutXML());
                        }

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

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SpreadForm_Load(object sender, EventArgs e)
        {
        }
    }
}