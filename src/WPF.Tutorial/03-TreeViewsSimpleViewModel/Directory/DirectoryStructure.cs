using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfTreeView
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public  static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //Get every logical drive on the machine
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }


        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            //Create empty list
            var items=new List<DirectoryItem>();

            #region Get Folders
            //Try and get directories from the folder
            //ignoring any issues doing so

            try
            {
                var dirs = Directory.GetDirectories(fullPath).Where(item=> (new FileInfo(item).Attributes & FileAttributes.Hidden) == 0).ToArray();
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir=>new DirectoryItem() {FullPath=dir,Type=DirectoryItemType.Folder }));

            }
            catch
            {

            }
            #endregion

            #region Get Files

            //Try and get files from the folder
            //ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath).Where(item => (new FileInfo(item).Attributes & FileAttributes.Hidden) == 0).ToArray();
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem() { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch
            {

            }
            #endregion

            return items;
        }

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
    }
}
