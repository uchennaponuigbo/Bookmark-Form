//The purpose of this project is to allow me to save web links to a text file.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Practice_2__Local_Web_Bookmark
{
    public partial class BookmarkForm : Form
    {
        public BookmarkForm()
        {
            InitializeComponent();
        }                
        private int number = 0;
        private int selectedNumber = 0;        

        private void Bookmark_Load(object sender, EventArgs e)
        {
            txtWebLink.Focus();
            numberlabel.Text = number.ToString();           
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(WebListBox.SelectedItems.Count != 0)
            {
                while (WebListBox.SelectedIndex != -1)
                {
                    WebListBox.Items.RemoveAt(WebListBox.SelectedIndex);
                }
            }
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {            
            WebListBox.Items.Add(this.txtWebLink.Text);
            txtWebLink.Text = "";
            number++;
            numberlabel.Text = number.ToString();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "*.txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt"; //filters to only text files
            string line = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //allows me to read a text file line by line
                StreamReader r = new StreamReader(openFileDialog1.FileName);
                while(line != null)
                {
                    line = r.ReadLine();
                    if(line != null)
                    {
                        WebListBox.Items.Add(line);
                        number++;
                        numberlabel.Text = number.ToString();
                    }             
                }

                string filename = openFileDialog1.FileName; // this code displays the name
                filedisplaylabel.Text = Path.GetFullPath(filename); //of the file that was opened.
                DateTime lastModified = File.GetLastWriteTime(filename);
                DateSavedLabel.Text = lastModified.ToString();
                r.Close();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string listboxitem = WebListBox.SelectedItem.ToString();
            try
            {
                Clipboard.SetText(listboxitem, TextDataFormat.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Copy Error");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WebListBox.SelectedItems.Count != 0)
            {
                while (WebListBox.SelectedIndex != -1)
                {
                    WebListBox.Items.RemoveAt(WebListBox.SelectedIndex);
                    number--;
                    numberlabel.Text = number.ToString();
                }
            }
        }

        private void DeleteButton_Click_1(object sender, EventArgs e)
        {
            if (WebListBox.SelectedItems.Count != 0)
            {
                while (WebListBox.SelectedIndex != -1)
                {
                    WebListBox.Items.RemoveAt(WebListBox.SelectedIndex);
                    number--;
                    numberlabel.Text = number.ToString();
                }
            }
        }

        private void ClearButton_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult =
                MessageBox.Show("Are you sure you want to delete all of the text and the file name from the form?",
                "Caution", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                WebListBox.Items.Clear();
                filedisplaylabel.Text = "N/A";
                DateSavedLabel.Text = "N/A";
                number = 0;
                numberlabel.Text = number.ToString();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) //TODO: Find a way to properly save over deleted contents
        {            
            var fileNameAndPath = filedisplaylabel.Text;

            if (string.IsNullOrEmpty((fileNameAndPath)))
            {
                OpenFileDialog openfiledialog1 = new OpenFileDialog();

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filedisplaylabel.Text = Path.GetFullPath(openFileDialog1.FileName); 
                    fileNameAndPath = filedisplaylabel.Text;
                    DateTime lastModified = File.GetLastWriteTime(fileNameAndPath);
                    DateSavedLabel.Text = lastModified.ToString();
                }
            }                        
                try
                {
                    using (FileStream fstream = new FileStream(fileNameAndPath,
                        FileMode.Open,
                            FileAccess.ReadWrite))
                    {
                        using (StreamWriter write = new StreamWriter(fstream))
                        {
                            foreach (var item in WebListBox.Items)
                                write.WriteLine(item.ToString());
                            write.Close();
                        }
                    DateTime lastModified = File.GetLastWriteTime(fileNameAndPath);
                    DateSavedLabel.Text = lastModified.ToString();
                    fstream.Close();
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "File not found error");
                }                                                                                 
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog1.Title = "Save as Text File";           
            var fileNameAndPath = filedisplaylabel.Text;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream S = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        using (StreamWriter st = new StreamWriter(S))
                        {
                            foreach (var aa in WebListBox.Items)
                                st.WriteLine(aa.ToString());
                            st.Close();
                        }
                        
                        DateSavedLabel.Text = DateTime.Now.ToString();
                        string filename = saveFileDialog1.FileName; //displays the name
                        filedisplaylabel.Text = Path.GetFullPath(filename); //of the file,                                   
                        DateTime lastModified = File.GetLastWriteTime(filename);
                        DateSavedLabel.Text = lastModified.ToString();
                        S.Close();
                        //with the full path of the file, I can save it to the exact designation
                    }
                }
                catch (IOException ex)
                {                   
                   MessageBox.Show(ex.Message, "Unable to Save File");                   
                }
            }
        }

        private void visitURLToolStripMenuItem_Click(object sender, EventArgs e) //TODO: make a method in the class that will check for "www"
        {                       
            string listboxitem = WebListBox.SelectedItem.ToString(); //converts selected index to string            
            if (WebValidator.URLIsValid(listboxitem)) 
            {
                System.Diagnostics.Process.Start(WebListBox.SelectedItem.ToString());
            }               
            
        }

        private void WebListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebListBox.HorizontalScrollbar = true;
            UI_Validator.IsSelected(WebListBox);
            selectedNumber = WebListBox.SelectedIndex + 1;
            //maybe use a while loop in the same manner as the one up there...
            //find a way to not display 0 as a part of the line number
            if (WebListBox.SelectedIndex > 0)
                currentLineLabel.Text = selectedNumber.ToString();
            //else if (WebListBox.SelectedIndex == 0 && number > 0)
            //{
            //    //WebListBox.SelectedIndex = 0;
            //    //currentLineLabel.Text = WebListBox.SelectedIndex.ToString();
            //    //currentLineLabel.Text = "1";
            //}

        }

        private void WebListBox_DoubleClick(object sender, EventArgs e)
        {
            string listboxitem = WebListBox.SelectedItem.ToString();
            if (WebValidator.URLIsValid(listboxitem))
            {
                System.Diagnostics.Process.Start(WebListBox.SelectedItem.ToString());
            }
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Info = new Info();
            Info.Show();
        }
    }
}
