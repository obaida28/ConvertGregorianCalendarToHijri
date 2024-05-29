using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace ConvertGregorianCalendarToHijri
{
    public partial class Form1 : Form
    {
        //2 – 5 – 7 – 10 – 13 – 16 – 18 – 21 – 24 – 26 – 29
        readonly int[] arr = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 10, 10, 10, 11 };
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var inputDate = Validation();
                var numberOfDays = (inputDate - new DateTime(622, 7, 16)).TotalDays;
                // Just this
                numberOfDays -= 2;
                // count of 30 years
                int count30s = Convert.ToInt32(numberOfDays) / 10631;
                int year = (count30s * 30) + 1;
                int remindDays1 = Convert.ToInt32(numberOfDays) % 10631;
                year += (remindDays1 / 354);
                int remindDays2 = remindDays1 % 354;
                remindDays2 -= arr[(remindDays1 / 354) % 30];
                int m = ((remindDays2 / 59) * 2) + 1;
                int d = remindDays2 % 59;
                if (d >= 29) m++;
                if (d >= 29) d -= 29;
                //d++;
                Result.Text = year + "/" + m + "/" + d;
            }
            catch
            {
                button3_Click(sender, e);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Year.Text = Month.Text = Day.Text = Result.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private DateTime Validation()
        {
            int year = int.Parse(Year.Text);
            int month = int.Parse(Month.Text);
            int day = int.Parse(Day.Text);
            var inputDate = new DateTime(year, month, day);
            if ((inputDate.Year < 622) || (inputDate.Year == 622 && inputDate.Month < 7) ||
                (inputDate.Year == 622 && inputDate.Month == 7 && inputDate.Day < 16))
                throw new Exception("This Date is before Hijar");
            return inputDate;
        }
    }
}
