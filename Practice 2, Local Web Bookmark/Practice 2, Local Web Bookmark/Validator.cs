using System;
using Practice_2__Local_Web_Bookmark;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_2__Local_Web_Bookmark
{
    public static class UI_Validator
	{
		private static string title = "Entry Error";     

        public static string Title
		{
			get { return title; }
			set { title = value;}
        }

		public static bool IsPresent(TextBox textBox)
		{           
			if (textBox.Text == "")
			{
				MessageBox.Show(textBox.Tag + " is a required field.", Title);
				textBox.Focus();
				return false;
			}
			return true;
		}

        //This is worthless but I'm keeping it for my own notes.
        public static bool protocolIsPresent(TextBox textBox, string http, string https, string www)
        {
            http = "http";
            https = "https";
            www = "www";
            if (textBox.Text.Contains(http) == false || textBox.Text.Contains(https) == false
                || textBox.Text.Contains(www) == false)
            {
                MessageBox.Show("You must have http, https, or www by the link",
                    "Missing Protocol");
                return false;
            }
            return true;
        }

        public static bool IsDouble(TextBox textBox)
        {
            double number = 0;
            if (Double.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be a decimal value.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox)
        {
            int number = 0;
            if (Int32.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be an integer.", Title);
                textBox.Focus();
                return false;
            }
        }

		public static bool IsWithinRange(TextBox textBox, double min, double max)
		{
			double number = Convert.ToDouble(textBox.Text);
			if (number < min || number > max)
			{
				MessageBox.Show(textBox.Tag + " must be between " + min
					+ " and " + max + ".", Title);
				textBox.Focus();
				return false;
			}
			return true;
		}

        //checks for a single range
        public static bool IsGreaterThan(TextBox textBox, double max)
        {
            double number = Convert.ToDouble(textBox.Text);
            if (number > max)
            {
                MessageBox.Show(textBox.Tag + "must be less than " +
                    max + ".", Title);
                return false;
            }
            return true;
        }

        //checks for a single range
        public static bool IsLessThan(TextBox textBox, double min)
        {
            double number = Convert.ToDouble(textBox.Text);
            if(number < min)
            {
                MessageBox.Show(textBox.Tag + "must be greater than " +
                    min + ".", Title);
                textBox.Text = "";
                return false;
            }
            return true;
        }

        public static bool IsChecked(RadioButton radioButton)
        {
            if(radioButton.Checked == false)
            {
                MessageBox.Show("You must select a radio button in the field.", Title);
             //   radioButton.Tag;
                return false;
            }
            return true;
        }

        public static bool IsSelected(ListBox listBox)
        {
            if (listBox.SelectedItem == null)
            {
                return false;
            }
            else
                return true;
        }
    }
}
