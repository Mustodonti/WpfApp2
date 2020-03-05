using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using ASD;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        string _selectPathToWatch;
        string _selectPathToDelete;
        int _periodDays;
        string _targetPath;
        public MainWindow()
        {
            InitializeComponent();
            DatePicker.SelectedDate = new DateTime(2020, 1, 23);
            browsePathTB.Text = "";
            browsePathToDeleteTB.Text = "";
            CopyMove(new object(), new RoutedEventArgs());
            Delete(new object(), new RoutedEventArgs());
        }

        void BrowseForWatch(object sender, RoutedEventArgs e) =>  browsePathTB.Text = WorkWithDirectoryWWD.WayToFolder();

        void BrowseForDelete(object sender, RoutedEventArgs e) => browsePathToDeleteTB.Text = WorkWithDirectoryWWD.WayToFolder();       

        void CopyMove(object sender, RoutedEventArgs e)
        {
            TextBlock_Column2.Text = "";
            var date = DatePicker.SelectedDate;
            _periodDays = DatePicker.SelectedDate.HasValue ? (int)(DateTime.Now - DatePicker.SelectedDate.Value).TotalDays : Convert.ToInt32(periodDaysTB.Text);
            if (periodDaysTB.Text != "" && browsePathTB.Text != "")
            {
                _selectPathToWatch = browsePathTB.Text;                

                _targetPath = "C:\\Будет удалено" + DateTime.Now.AddDays(_periodDays).ToString("dd.MM.yyyy");
                WorkWithDirectoryWWD.CreateDirectoryToTargetPathWWD001(_targetPath);

                TextBlock_Column2.Text += "\nУстаревшие Каталоги:\n";
                List<string> DirectoriesList = WorkWithDirectoryWWD.GetDirectoriesOlderDatetimeWWD002(_selectPathToWatch, _periodDays);
                foreach (string i in DirectoriesList)
                {
                    TextBlock_Column2.Text += i + "\n";
                }

                TextBlock_Column2.Text += "\nУстаревшие Файлы:\n";//+string.Join("",Work_With_DirectoryWWD.GetFiles_Older_Datetime_WWD003(SPstr, Period_Days));
                List<string> FilesList = WorkWithDirectoryWWD.GetFilesOlderDatetimeWWD003(_selectPathToWatch, _periodDays);
                FilesList.ForEach(i => TextBlock_Column2.Text += i + "\n");

                /*Movement old files to destination folder */
                WorkWithDirectoryWWD.MovementFilesWWD005(_targetPath, FilesList);
                TextBlock_Column2.Text += $"\nФайлы успешно перемещены: \n{_targetPath}";
                WorkWithDirectoryWWD.MovementDirectoriesWWD004(_targetPath, DirectoriesList);
                TextBlock_Column2.Text += $"\nКаталоги успешно перемещены: \n{_targetPath}";
            }
        }
        
        void Delete(object sender, RoutedEventArgs e)
        {
            TextBlock_Column2.Text = "\nУдаленные Каталоги:\n";
            WorkWithDirectoryWWD.GetDirectoriesInDyrectoriesWWD006(_selectPathToDelete).ForEach(i => TextBlock_Column2.Text += i + "\n");
       
            TextBlock_Column2.Text += "\nУдаленные Файлы:\n";
            WorkWithDirectoryWWD.GetFilesInDirectoriesWWD007(_selectPathToDelete).ForEach(i => TextBlock_Column2.Text += i + "\n"); ;
            Directory.Delete(_selectPathToDelete, true);
        }
    }
}