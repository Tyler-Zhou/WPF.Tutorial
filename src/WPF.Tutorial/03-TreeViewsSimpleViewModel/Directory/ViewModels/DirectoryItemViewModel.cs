﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfTreeView
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel:BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImageName => Type == DirectoryItemType.Drive ? "drive" : (Type == DirectoryItemType.File ? "file" : (IsExpanded ? "folder-open" : "folder-closed"));


        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return Type == DirectoryItemType.Drive ? FullPath : DirectoryStructure.GetFileFolderName(FullPath);
            }
        }

        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }
        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand
        {
            get
            {
                return Type != DirectoryItemType.File;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return Children?.Count(f => f != null) > 0;
            }
            set
            {
                if (value == true)
                {
                    Expand();
                }
                else
                {
                    ClearChildren();
                }
            }
        }
        #endregion

        #region Public Commands
        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of item</param>
        public DirectoryItemViewModel(string fullPath,DirectoryItemType type)
        {
            //Create commands
            ExpandCommand = new RelayCommand(Expand);

            //Set path and type
            FullPath = fullPath;
            Type = type;
            ClearChildren();
        } 
        #endregion

        #region Helper Methods
        /// <summary>
        /// Removes all children from the list ,adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            //Clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            //Show the expand arrow if we are not a file
            if (Type != DirectoryItemType.File)
            {
                Children.Add(null);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            //we cannot expand a file
            if (Type == DirectoryItemType.File)
                return;
            //Find all children
            var children=DirectoryStructure.GetDirectoryContents(FullPath);

            Children =new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath,content.Type)));
        } 
        #endregion
    }
}