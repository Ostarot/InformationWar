using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InformationWar
{
    public partial class Form1 : Form
    {
        Model model;
        Model exercise;
        Model example1;
        bool rus = true;
        public Form1()
        {
            model = new Model();
            exercise = new Model();
            example1 = new Model();

            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("ru");
            InitializeComponent();
            label10.Text = "";
            label12.Text = "";
            label13.Text = "";
            label21.Text = "";
            label22.Text = "";
            label23.Text = "";

            textBox1.Text = "20000";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "1";
            textBox5.Text = "0,1";
            textBox6.Text = "0,002";
            textBox7.Text = "0,0045";

            model.n00 = 20000;
            model.n10 = 0;
            model.n20 = 0;
            model.alpha1 = 1;
            model.alpha2 = 0.1;
            model.beta1 = 0.002;
            model.beta2 = 0.0045;

            model.euler_n1_list();
            chart_output(model, chart1);

            textBox14.Text = "0,1";
            textBox12.Text = "0,1";
            textBox9.Text = "0,0001";
            textBox8.Text = "0,0001";

            exercise.n00 = 20000;
            exercise.n10 = 0;
            exercise.n20 = 0;
            exercise.alpha1 = 0.01;
            exercise.alpha2 = 0.01;
            exercise.beta1 = 0.0001;
            exercise.beta2 = 0.0001;

            exercise.euler_n1_list();
            chart_output(exercise, chart4);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1(System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            aboutBox.Show();
        }

        public void chart_output(Model model, Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            chart.Palette = ChartColorPalette.Berry; //SeaGreen
            chart.Legends[0].Enabled = true;
            chart.Series[0].BorderWidth = 3;
            chart.Series[1].BorderWidth = 3;
            chart.Series[0].LegendText = "N1(t)";
            chart.Series[1].LegendText = "N2(t)";

            Axis ay = new Axis();
            if (rus) ay.Title = "число «адептов», принявших информацию";
            else ay.Title = "number of adherents";
            chart.ChartAreas[0].AxisY = ay;

            for (int i = 0; i < model.n1.Count; i++)
            {
                chart.Series[0].Points.AddXY(i * model.h, model.n1[i]);
                chart.Series[1].Points.AddXY(i * model.h, model.n2[i]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            example1_func();
        }

        #region смена языков
        private void русскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            rus = true;
            языкToolStripMenuItem.Text = "Язык";
            русскийToolStripMenuItem.Text = "Русский";
            английскийToolStripMenuItem.Text = "Английский";
            оПрограммеToolStripMenuItem.Text = "О программе";
        }

        private void английскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyResources(resources, this.Controls);
            rus = false;
            языкToolStripMenuItem.Text = "Language";
            русскийToolStripMenuItem.Text = "Russian";
            английскийToolStripMenuItem.Text = "English";
            оПрограммеToolStripMenuItem.Text = "About program";
        }

        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
            }
        }

        #endregion

        #region примеры
        public void example1_func()
        {

            example1.n00 = 20000;
            example1.n10 = 0;
            example1.n20 = 0;
            example1.alpha1 = 1;
            example1.alpha2 = 0.1;
            example1.beta1 = 0.0035;
            example1.beta2 = 0.0016;

            example1.euler_n1_list();

            chart_output(example1, chart2);
        }
        #endregion

        public double beta_cond(double a, double b, double n)
        {
            return b/Math.Log(1+b*n/(2*a));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = Convert.ToString(trackBar1.Value / 100.0);
            model.alpha1 = trackBar1.Value / 100.0;
            main_graph();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = Convert.ToString(trackBar2.Value / 100.0);
            model.alpha2 = trackBar2.Value / 100.0;
            main_graph();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = Convert.ToString(trackBar3.Value / 10000.0);
            model.beta1 = trackBar3.Value / 10000.0;
            main_graph();
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox7.Text = Convert.ToString(trackBar4.Value / 10000.0);
            model.beta2 = trackBar4.Value / 10000.0;
            main_graph();
        }

        public void main_graph()
        {
            try
            {
                model.n00 = Convert.ToDouble(textBox1.Text);
                model.n10 = Convert.ToDouble(textBox2.Text);
                model.n20 = Convert.ToDouble(textBox3.Text);
            }
            catch (Exception ex)
            {
                if (rus) MessageBox.Show("Неверный формат входных данных", "Ошибка");
                else MessageBox.Show("Wrong data format", "Error");
                return;
            }
            if (model.wrong_number())
            {
                if (rus) MessageBox.Show("Неверные значения входных данных", "Ошибка");
                else MessageBox.Show("Wrong values", "Error");
                return;
            }

            model.euler_n1_list();
            chart_output(model, chart1);
            label10.Text = Convert.ToString(model.n1.Last());
            label12.Text = Convert.ToString(model.n2.Last());
            if (model.n1.Last() > model.n2.Last())
            {
                label13.Text = "1";
            }
            else label13.Text = "2";
        }


        public void exetcize_graph()
        {
            exercise.euler_n1_list();
            chart_output(exercise, chart4);
            label23.Text = Convert.ToString(exercise.n1.Last());
            label22.Text = Convert.ToString(exercise.n2.Last());
            if (exercise.n1.Last() > exercise.n2.Last())
            {
                label21.Text = "1";
            }
            else label21.Text = "2";

            if ((exercise.beta1 < exercise.beta2) && (exercise.alpha1 > exercise.alpha2) && (beta_cond(exercise.alpha1, exercise.beta1, exercise.n00) > beta_cond(exercise.alpha2, exercise.beta2, exercise.n00)))
            {
                if (rus) label33.Text = "Упражнение выполнено!";
                else label33.Text = "The exercise is complete!";
            }
            else if (rus) label33.Text = "Упражнение не выполнено";
            else label33.Text = "The exercise is not complete";
        }

        public void example_graph()
        {
            example1.euler_n1_list();
            chart_output(example1, chart2);
            label35.Text = Convert.ToString(example1.n1.Last());
            label34.Text = Convert.ToString(example1.n2.Last());
            if (example1.n1.Last() > example1.n2.Last())
            {
                label17.Text = "1";
            }
            else label17.Text = "2";

            if ((example1.n1.Last() > example1.n2.Last()) && (example1.n1[1] > example1.n2[1]))
            {
                if (rus) label14.Text = "Победа И1 достигается в режиме постоянного доминирования.";
                else label14.Text = "The victory of I1 is achieved in the mode of constant dominance.";
            }
            else
            {
                if ((example1.n2.Last() > example1.n1.Last()) && (example1.n2[1] > example1.n1[1]))
                {
                    if (rus) label14.Text = "Победа И2 достигается в режиме постоянного доминирования.";
                    else label14.Text = "The victory of I2 is achieved in the mode of constant dominance.";
                }
                else
                {
                    if (rus) label14.Text = "Происходит смена лидерства.";
                    else label14.Text = "The change of leadership has occurred.";
                }
            }
        }

        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            textBox14.Text = Convert.ToString(trackBar8.Value / 100.0);
            exercise.alpha1 = trackBar8.Value / 100.0;
            exetcize_graph();
        }
        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            textBox12.Text = Convert.ToString(trackBar7.Value / 100.0);
            exercise.alpha2 = trackBar7.Value / 100.0;
            exetcize_graph();
        }
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            textBox9.Text = Convert.ToString(trackBar6.Value / 10000.0);
            exercise.beta1 = trackBar6.Value / 10000.0;
            exetcize_graph();
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            textBox8.Text = Convert.ToString(trackBar5.Value / 10000.0);
            exercise.beta2 = trackBar5.Value / 10000.0;
            exetcize_graph();
        }

        private void trackBar11_Scroll(object sender, EventArgs e)
        {
            textBox23.Text = Convert.ToString(trackBar11.Value / 100.0);
            example1.alpha1 = trackBar11.Value / 100.0;
            example_graph();
        }

        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            textBox22.Text = Convert.ToString(trackBar10.Value / 100.0);
            example1.alpha2 = trackBar10.Value / 100.0;
            example_graph();
        }

    }
}
