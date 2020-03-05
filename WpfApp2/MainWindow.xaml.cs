using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime;
using ASD;
using Path = System.IO.Path;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        string SPstr;
        string Str;
        int Period_Days;
        string Target_Path;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            SPstr = textBox1.Text = MyLib.Way_To_Folder("textBox1");
            TextBlock_Column2.Text = "Hello";
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_Column2.Text = "";
            Str = "";
            /* Check: input data are correct?*/

            if (textBox2.Text != "" && textBox1.Text != "")
            {
                /*Take data from textboxes*/
                SPstr = textBox1.Text;
                try
                {
                    Period_Days = Convert.ToInt32(textBox2.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                /*Create data */

                try
                {
                    //DateTime date = new DateTime();
                    //MessageBox.Show(dt.ToString("dd.MM.yyyy"));
                    //DateTime date1 = new DateTime();// 01.01.0001 0:00:00
                    //date1.AddDays(Period_Days);
                    //MessageBox.Show(date.ToString());
                    //TimeSpan tSpan= new TimeSpan(Period_Days,0,0,0);
                    //MessageBox.Show(tSpan.ToString());
                    //DateTime total_date = date.Add(tSpan);
                    //MessageBox.Show(total_date.ToString());


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                /*Create destination folder for old files */

                try
                {
                    Target_Path = "C:\\Будет удалено" + DateTime.Now.AddDays(Period_Days).ToString("dd.MM.yyyy");
                    DirectoryInfo dirInfo = new DirectoryInfo(Target_Path);//Работа с Директорией
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                        TextBlock_Column2.Text += ("\nCreate destination folder for old files\n------------------------------Successful---------------------------------\n");
                    }
                    else MessageBox.Show("Папка уже существует,прежде удалите ее");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



                /*Analysis of files in selected folder */

                try
                {
                    TextBlock_Column2.Text += "Каталоги\n" + Str;
                    string[] files = Directory.GetFiles(SPstr);
                    foreach (string s in files)
                    {
                        string date = DateTime.Now.ToString("dd.MM.yyyy");//Работа с датой
                        DateTime dt = Directory.GetLastAccessTime(s);
                        //MessageBox.Show(s);
                        Str += s + "\t" + "\t" + dt;
                        if (dt < DateTime.Now.AddDays(-Period_Days))
                        {
                            
                            Str += "@@@\n";
                        }

                    }
                    TextBlock_Column2.Text = "Файлы\n" + Str;
                    Str = "";
                    string[] dir = Directory.GetDirectories(SPstr);
                    foreach (string s in dir)
                    {
                        DateTime dt = Directory.GetLastAccessTime(s);
                        Str += s + "\t" + "\t" + dt;
                        if (dt < DateTime.Now.AddDays(-Period_Days))
                        {
                            Str += "@@@\n";
                        }

                    }
                    TextBlock_Column2.Text += "\n------------------------------Successful---------------------------------\n";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



                /*Movement old files to destination folder */
                try
                {
                    string sourcePath = SPstr;                  //Папка с исходными файлами
                    string targetPath = Target_Path;
                    MessageBox.Show(Target_Path);
                    DirectoryInfo sourceDir = new DirectoryInfo(sourcePath);
                    DirectoryInfo targetDir = new DirectoryInfo(targetPath);

                    if (!targetDir.Exists) targetDir.Create();

                    var filesInTargetDir = targetDir.GetFiles().Select(f => f.Name);
                    foreach (FileInfo file in sourceDir.GetFiles())
                    {
                        string fileName = file.Name;
                        int i = 1;
                        while (filesInTargetDir.Contains(fileName))
                        {
                            i++;
                            fileName = string.Format("{0} ({1}).{2}", Path.GetFileNameWithoutExtension(file.FullName), i, file.Extension);
                        }
                        file.MoveTo(Path.Combine(targetPath, fileName));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               


                // To move an entire directory. To programmatically modify or combine
                // path strings, use the System.IO.Path class.
                // System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private");

                // Point the DirectoryInfo reference to the new directory.
                //di = new DirectoryInfo("NewTempDir");

                // Delete the directory.
                //di.Delete(true);



                //DateTime dt = Directory.GetLastAccessTime("C:\\Users\\Йонас\\Desktop\\DiplomВ5.txt");//Возвращает время и дату последней операции записи в указанный файл или каталог.
                //MessageBox.Show($"The last write time for this file was {dt}.");



                // MessageBox.Show(SPstr);
                // Form2 newForm = new Form2();
                // newForm.Show();

                // Process.Start(@"C:\Users\Йонас\Desktop\ConsoleApp2\ConsoleApp2\bin\Debug\netcoreapp3.1\ConsoleApp2.exe", "");
                //  FileInfo file = new FileInfo(@"C: \Users\Йонас\Desktop\vbbook.txt");
                //if (file.Exists == false) //Если файл не существует
                // {
                //     file.Create(); //Создаем
                // }
                // else MessageBox.Show("Файл уже создан!");

                //string Path = "C:\\Будет удалено" + DateTime.Now.AddDays(FR_Compare).ToString("dd.MM.yyyy");
                //DirectoryInfo dirInfo = new DirectoryInfo(Path);//Работа с Директорией
                //if (!dirInfo.Exists)
                //{
                //    dirInfo.Create();
                //    Console.WriteLine("------------------------------Successful---------------------------------");
                //}
                //else MessageBox.Show("Папка уже существует,прежде удалите ее");


                //string[] files = Directory.GetFiles(SPstr);
                //foreach (string s in files)
                //{

                //    string date = DateTime.Now.ToString("dd.MM.yyyy");//Работа с датой
                //    DateTime dt = Directory.GetLastAccessTime(s);
                //    //MessageBox.Show(s);
                //    Str += s + "\t" + "\t" + dt + "\n";
                //    if (dt < DateTime.Now.AddDays(-Difference))
                //    {
                //        //FileInfo fInfo = new FileInfo(s);
                //        //fInfo.MoveTo(@"C: \Users\Йонас\Desktop\2");
                //        Str += "@@@\n";
                //    }

                //}
                //label2.Text = "Файлы\n" + Str;
                //Str = "";
                //string[] dir = Directory.GetDirectories(SPstr);
                //foreach (string s in dir)
                //{
                //    DateTime dt = Directory.GetLastAccessTime(s);
                //    //MessageBox.Show(s);
                //    if (dt < DateTime.Now.AddDays(-Difference))
                //    {

                //        Str += "@@@\n";
                //    }
                //    Str += s + "\t" + "\t" + dt + "\n";
                //}
                //label2.Text += "Каталоги\n" + Str;
            }

        }
    }
}
