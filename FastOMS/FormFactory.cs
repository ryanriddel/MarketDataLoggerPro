using FastOMS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MktSrvcAPI;
using FastOMS;
using System.Windows.Forms;
using FastOMS.Data_Structures;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using FastOMS.UI.Interfaces;
using DevExpress.XtraEditors;

namespace FastOMS
{
    public class FormFactory : ApplicationContext
    {
        ConcurrentDictionary<Type, List<IGenericMarketDataConsumer>> MarketDataConsumerListDict = new ConcurrentDictionary<Type, List<IGenericMarketDataConsumer>>();
        ConcurrentDictionary<Guid, IGenericMarketDataConsumer> MarketDataConsumerDict = new ConcurrentDictionary<Guid, IGenericMarketDataConsumer>();

        ConcurrentDictionary<Type, List<XtraForm>> TypeToFormsListDict = new ConcurrentDictionary<Type, List<XtraForm>>();

        public FormFactory()
        {
            //TypeToFormsListDict[typeof(WatchListForm)] = new List<XtraForm>();
            CreateMainForm().Show();
            Hub._formFactory = this;
        }

        public MainForm CreateMainForm()
        {
            MainForm newForm = new MainForm();

            return newForm;

        }

        public Level2Form CreateLevel2Form(InstrInfo[] instruments, string layoutXML = null)
        {
            Guid groupGuid = Guid.NewGuid();
            Level2Form newForm = new Level2Form(instruments, groupGuid);

            if (layoutXML != null)
            {
                newForm.StartPosition = FormStartPosition.Manual;
                newForm.LoadLayoutXML(layoutXML);
            }
            formLayoutList.Add(newForm);
            newForm.FormClosing += (object sender, FormClosingEventArgs e) => formLayoutList.Remove(newForm);
            newForm.Show();
            return newForm;
        }

        public SpreadForm CreateSpreadForm(InstrInfo[] instruments, string layoutXML = null)
        {
            Guid groupGuid = Guid.NewGuid();

            SpreadForm newSpreadForm = new SpreadForm(instruments, groupGuid);

            
            if (layoutXML != null)
            {
                newSpreadForm.StartPosition = FormStartPosition.Manual;
                newSpreadForm.LoadLayoutXML(layoutXML);
            }
            formLayoutList.Add(newSpreadForm);
            newSpreadForm.FormClosing += (object sender, FormClosingEventArgs e) => formLayoutList.Remove(newSpreadForm);
            newSpreadForm.Show();
            return newSpreadForm;
        }

        public OrderManagementForm CreateOrderManagementForm(string layoutXML = null)
        {
            Guid groupGuid = Guid.NewGuid();

            OrderManagementForm newForm = new OrderManagementForm();

            if (layoutXML != null)
            {
                newForm.StartPosition = FormStartPosition.Manual;
                newForm.LoadLayoutXML(layoutXML);
            }
            formLayoutList.Add(newForm);
            newForm.FormClosing += (object sender, FormClosingEventArgs e) => RemoveFormFromLayoutList(newForm);
            newForm.Show();
            return newForm;
        }

        public WatchListForm CreateWatchListForm(string layoutXML = null)
        {
            Guid newGuid = Guid.NewGuid();

            WatchListForm newWatchList = new WatchListForm();
            if (layoutXML != null)
            {
                newWatchList.StartPosition = FormStartPosition.Manual;
                newWatchList.LoadLayoutXML(layoutXML);
            }
            formLayoutList.Add(newWatchList);
            newWatchList.FormClosing += (object sender, FormClosingEventArgs e) => formLayoutList.Remove(newWatchList);

            newWatchList.Show();
            return newWatchList;
         }

        public void RemoveFormFromLayoutList(ILayoutSaveLoader form)
        {
            try
            {
                formLayoutList.Remove(form);
            }
            catch(Exception e)
            {

            }
        }

        //******************XML***************

        public List<ILayoutSaveLoader> formLayoutList = new List<ILayoutSaveLoader>();

        public void LoadLayoutXML(string filename)
        {
            if (File.Exists(filename))
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    bool hit = false;
                    while (!reader.EOF)
                    {
                        hit = false;
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Level2Form":
                                    if (reader["Instruments"] != null)
                                    {
                                        hit = true;
                                        InstrInfo[] formInsts = Utilities.OldOMSStringToInstrArray(reader["Instruments"]);
                                        CreateLevel2Form(formInsts, reader.ReadOuterXml());
                                    }
                                    break;
                                case "SpreadForm":
                                    if (reader["Instruments"] != null)
                                    {
                                        hit = true;
                                        InstrInfo[] formInsts = Utilities.OldOMSStringToInstrArray(reader["Instruments"]);
                                        CreateSpreadForm(formInsts, reader.ReadOuterXml());
                                    }
                                    break;
                                case "OrigSpreadForm":
                                    if (reader["Instruments"] != null)
                                    {
                                        hit = true;
                                        InstrInfo[] formInsts = Utilities.OldOMSStringToInstrArray(reader["Instruments"]);
                                       // CreateOrigSpreadForm(formInsts, reader.ReadOuterXml());
                                    }
                                    break;
                                case "WatchListForm":
                                    hit = true;
                                    CreateWatchListForm(reader.ReadOuterXml());
                                    break;
                                case "OrderManagementForm":
                                    hit = true;
                                    CreateOrderManagementForm(reader.ReadOuterXml());
                                    break;
                            }
                        }
                        if (!hit)
                        {
                            reader.Read();
                        }
                    }
                }
            }
        }

        public void SaveLayoutXML(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.OmitXmlDeclaration = true;
            using (XmlWriter xw = XmlWriter.Create(filename, settings))
            {
                xw.WriteStartElement("FastOMSLayout");

                foreach (ILayoutSaveLoader form in formLayoutList)
                {
                    xw.WriteRaw(form.GetLayoutXML());
                }
                xw.WriteEndElement();
            }
        }

    }
}
