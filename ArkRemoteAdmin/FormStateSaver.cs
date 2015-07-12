using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SCBWebserviceCall
{
    public class FormStateSaver : Component
    {
        private Form form;
        private bool saveSize = true;
        private bool saveLocation = true;
        private bool saveWindowState = true;

        public Form FormToSave
        {
            get { return form; }
            set
            {
                RemoveEventsFromForm();
                form = value;
                AddEventsToForm();
            }
        }

        [DefaultValue(true)]
        public bool SaveSize
        {
            get { return saveSize; }
            set { saveSize = value; }
        }

        [DefaultValue(true)]
        public bool SaveLocation
        {
            get { return saveLocation; }
            set { saveLocation = value; }
        }

        [DefaultValue(true)]
        public bool SaveWindowState
        {
            get { return saveWindowState; }
            set { saveWindowState = value; }
        }

        private void AddEventsToForm()
        {
            if (!DesignMode && form != null)
            {
                form.Load += Form_Load;
                form.FormClosing += Form_FormClosing;
            }
        }

        private void RemoveEventsFromForm()
        {
            if (!DesignMode && form != null)
            {
                form.Load -= Form_Load;
                form.FormClosing -= Form_FormClosing;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadFormState();
        }

        private void Form_FormClosing(object sender, EventArgs e)
        {
            SaveFormState();
        }

        protected string GetXmlPath()
        {
            return Path.Combine(Application.LocalUserAppDataPath, form.Name + ".xml");
        }

        public void LoadFormState()
        {
            if (form == null || (!saveLocation && !saveSize && !saveWindowState))
                return;

            string path = GetXmlPath();
            if (File.Exists(path))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                if (xml["Form"] != null)
                {
                    XmlNode root = xml["Form"];
                    if (saveWindowState && root["WindowState"] != null)
                    {
                        form.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), root["WindowState"].InnerText);
                    }
                    if (saveLocation && root["Location"] != null)
                    {
                        if (root["Location"]["X"] != null)
                            form.Left = Convert.ToInt32(root["Location"]["X"].InnerText);
                        if (root["Location"]["Y"] != null)
                            form.Top = Convert.ToInt32(root["Location"]["Y"].InnerText);
                    }
                    if (saveSize && root["Size"] != null)
                    {
                        if (root["Size"]["Width"] != null)
                            form.Width = Convert.ToInt32(root["Size"]["Width"].InnerText);
                        if (root["Size"]["Height"] != null)
                            form.Height = Convert.ToInt32(root["Size"]["Height"].InnerText);
                    }
                }
            }
        }

        public void SaveFormState()
        {
            if (form == null || (!saveLocation && !saveSize && !saveWindowState))
                return;

            XmlDocument xml = new XmlDocument();
            XmlNode root = xml.AppendChild(xml.CreateElement("Form"));
            Rectangle bounds = form.Bounds;
            if (form.WindowState != FormWindowState.Normal)
            {
                bounds = form.RestoreBounds;
            }
            if (saveLocation)
            {
                XmlNode loc = root.AppendChild(xml.CreateElement("Location"));
                loc.AppendChild(xml.CreateElement("X")).InnerText = bounds.X.ToString();
                loc.AppendChild(xml.CreateElement("Y")).InnerText = bounds.Y.ToString();
            }
            if (saveSize)
            {
                XmlNode size = root.AppendChild(xml.CreateElement("Size"));
                size.AppendChild(xml.CreateElement("Width")).InnerText = bounds.Width.ToString();
                size.AppendChild(xml.CreateElement("Height")).InnerText = bounds.Height.ToString();
            }
            if (saveWindowState)
            {
                root.AppendChild(xml.CreateElement("WindowState")).InnerText = form.WindowState.ToString();
            }
            xml.Save(GetXmlPath());
        }
    }
}
