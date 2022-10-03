using System;
using System.Collections.Generic;
using System.Linq;

namespace InformationWar
{
    public class Model
    {
        public double n00;
        public double n10;
        public double n20;
        public double alpha1;
        public double alpha2;
        public double beta1;
        public double beta2;

        public double c = 0;
        public double h = 0.01;

        public List<double> n1;
        public List<double> n2;

        public void euler_n1_list()
        {
            double eps = 0.001;

            n1 = new List<double>();
            n2 = new List<double>();
            n1.Add(n10);
            n2.Add(n20);

            c = (alpha2 + beta2 * n20) / Math.Pow((alpha1 + beta1 * n10), beta2 / beta1);

            while ((n00-n1.Last()-n2.Last())>eps)
            {
                n1.Add(h * (alpha1 + beta1 * n1.Last()) * (n00 - n1.Last() - ((c / beta2 * Math.Pow((alpha1 + beta1 * n1.Last()), beta2 / beta1) - alpha2 / beta2))) + n1.Last());
                n2.Add(analit_n2(n1.Last()));
            }
        }

        public double analit_n2(double current_n1)
        {
            double n2;
            n2 = c/beta2 * Math.Pow((alpha1 + beta1*current_n1), beta2/beta1) - alpha2/beta2;
            return n2;
        }

        public bool wrong_number()
        {
            if ((n10 + n20) > n00) return true;
            return false;
        }
    }
}
