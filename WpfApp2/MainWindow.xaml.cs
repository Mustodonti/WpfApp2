using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using ASD;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        string SelectPathToWatch;
        string SelectPathToDelete;
        int Period_Days;
        string Target_Path;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SelectPathToWatch = textBox1.Text = Work_With_DirectoryWWD.Way_To_Folder();
            //string SPstr1 = DatePicker1.Text;
            //MessageBox.Show(SPstr1);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            /* If repeated clicks will be,then need to perform below blocks*/
            TextBlock_Column2.Text = "";

            /* Check: input data are correct?*/
            if (textBox2.Text != "" && textBox1.Text != "")
            {
                /*Take data from textboxes*/
                SelectPathToWatch = textBox1.Text;
                try
                {
                    Period_Days = Convert.ToInt32(textBox2.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                /*Create destination folder for old files */

                Target_Path = "C:\\Будет удалено" + DateTime.Now.AddDays(Period_Days).ToString("dd.MM.yyyy");
                Work_With_DirectoryWWD.CreateDirectory_to_TargetPath_WWD001(Target_Path);

                /*Analysis of files in selected folder */

                TextBlock_Column2.Text += "\nУстаревшие Каталоги:\n";
                List<string> DirectoriesList = Work_With_DirectoryWWD.GetDirectoriesOlderDatetimeWWD002(SelectPathToWatch, Period_Days);
                foreach (string i in DirectoriesList)
                {
                    TextBlock_Column2.Text += i + "\n";
                }

                TextBlock_Column2.Text += "\nУстаревшие Файлы:\n";//+string.Join("",Work_With_DirectoryWWD.GetFiles_Older_Datetime_WWD003(SPstr, Period_Days));
                List<string> FilesList = Work_With_DirectoryWWD.GetFilesOlderDatetimeWWD003(SelectPathToWatch, Period_Days);
                foreach (string i in FilesList)
                {
                    TextBlock_Column2.Text += i + "\n";
                }


                /*Movement old files to destination folder */
                Work_With_DirectoryWWD.MovementFilesWWD005(Target_Path, FilesList);
                TextBlock_Column2.Text += $"\nФайлы успешно перемещены: \n{Target_Path}";
                Work_With_DirectoryWWD.MovementDirectoriesWWD004(Target_Path, DirectoriesList);
                TextBlock_Column2.Text += $"\nКаталоги успешно перемещены: \n{Target_Path}";

            }
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            SelectPathToDelete = textBox3.Text = Work_With_DirectoryWWD.Way_To_Folder();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            /* If repeated clicks will be,then need to perform below blocks*/
            TextBlock_Column2.Text = "";
            /*Delete of files in selected folder */
            TextBlock_Column2.Text += "\nУдаленные Каталоги:\n";
            List<string> DirectoriesList = Work_With_DirectoryWWD.GetDirectoriesInDyrectoriesWWD006(SelectPathToDelete);
            foreach (string i in DirectoriesList)
            {
                TextBlock_Column2.Text += i + "\n";
            }

            TextBlock_Column2.Text += "\nУдаленные Файлы:\n";//+string.Join("",Work_With_DirectoryWWD.GetFiles_Older_Datetime_WWD003(SPstr, Period_Days));
            List<string> FilesList = Work_With_DirectoryWWD.GetFilesInDirectoriesWWD007(SelectPathToDelete);
            foreach (string i in FilesList)
            {
                TextBlock_Column2.Text += i + "\n";
            }
            Directory.Delete(SelectPathToDelete, true);
        }
    }
}
