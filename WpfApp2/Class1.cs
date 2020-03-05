using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ASD
{
    class MyLib
    {
        static public string Way_To_Folder(string textbox)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return textbox = FBD.SelectedPath;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Вы не выбрали директорию");
                return textbox = "";
            }
        }
    }
}