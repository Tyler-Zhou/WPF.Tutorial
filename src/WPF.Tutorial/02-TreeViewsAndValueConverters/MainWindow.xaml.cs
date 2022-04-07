using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTreeView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constrcutor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                //Create a new tree for it
                var item = new TreeViewItem()
                {
                    //set tree header and path
                    Header = drive,
                    //Add the full path
                    Tag = drive,
                };

                //Add a dumy item
                item.Items.Add(null);

                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;
                //Add it to the main tree-view
                tvFolder.Items.Add(item);
            }
        }


        #endregion

        #region Folder Expanded
        /// <summary>
        /// When a folder is expanded,find the sub folders/files
        /// </summary>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Initial Checks
                var item = (TreeViewItem)sender;
                //If the item only contains the dumy data
                if (item.Items.Count != 1 && item.Items[0] != null)
                    return;
                //Clear dumy data
                item.Items.Clear();

                //Get full path
                var fullPath = (string)item.Tag; 
                #endregion

                #region Get Folders
                var directories = new List<string>();

                //Try and get directories from the folder
                //ignoring any issues doing so

                try
                {
                    var dirs = Directory.GetDirectories(fullPath);
                    if (dirs.Length > 0)
                        directories.AddRange(dirs);
                }
                catch
                {

                }

                directories.ForEach(directoryPath =>
                {
                    //Create directory item
                    var subItem = new TreeViewItem()
                    {
                        //Set header as folder name
                        Header = GetFileFolderName(directoryPath),
                        //Add tag as full path
                        Tag = directoryPath,
                    };
                    subItem.Items.Add(null);
                    //Handle expanding
                    subItem.Expanded += Folder_Expanded;

                    //Add this item to the parent
                    item.Items.Add(subItem);
                });
                #endregion

                #region Get Files
                var files = new List<string>();

                //Try and get files from the folder
                //ignoring any issues doing so

                try
                {
                    var fs = Directory.GetFiles(fullPath);
                    if (fs.Length > 0)
                        files.AddRange(fs);
                }
                catch
                {

                }

                files.ForEach(filePath =>
                {
                    //Create file item
                    var subItem = new TreeViewItem()
                    {
                        //Set header as file name
                        Header = GetFileFolderName(filePath),
                        //Add tag as full path
                        Tag = filePath,
                    };
                    //Add this item to the parent
                    item.Items.Add(subItem);
                });
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            //Make all slashes back slashes
            var normailzedPath = path.Replace('/', '\\');

            //Find the last backslash in the path
            var lastIndex = normailzedPath.LastIndexOf('\\');

            //If we don't find a bckslash,return the path itself
            if (lastIndex <= 0)
                return path;

            //return the name after the last back slash
            return path.Substring(lastIndex + 1);
        } 
        #endregion
    }
}
