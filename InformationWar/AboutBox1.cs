using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InformationWar
{
    partial class AboutBox1 : Form
    {
        public AboutBox1(string lang)
        {
            InitializeComponent();
            if (lang == "en")
            {
                this.Text = String.Format("About project {0}", AssemblyTitle);
                this.labelProductName.Text = "Information war";
                this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
                this.labelCopyright.Text = "Melikhova Victoria";
                this.labelCompanyName.Text = "MPEI";
                this.textBoxDescription.Text = "The computer model can be used to simplify the process of analyzing information war and to determine characteristics, the management of which can stimulate the course of the fight in the direction necessary for the participant.";
            }
            else
            {
                this.Text = String.Format("О программе {0}", AssemblyTitle);
                this.labelProductName.Text = "Информационная борьба";
                this.labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
                this.labelCopyright.Text = "Мелихова Виктория";
                this.labelCompanyName.Text = "НИУ МЭИ";
                this.textBoxDescription.Text = "Компьютерная модель может использоваться для упрощения процесса анализа информационной борьбы и для определения содержательных характеристик, управление которыми может стимулировать протекание борьбы в нужном для участника направлении.";
            }
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
