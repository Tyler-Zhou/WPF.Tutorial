namespace WpfTreeView
{
    /// <summary>
    /// The type of a directory item
    /// </summary>
    public enum DirectoryItemType
    {
        /// <summary>
        /// UnKnown
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// A logical drive
        /// </summary>
        Drive = 1,
        /// <summary>
        /// A phyiscal file
        /// </summary>
        File = 2,
        /// <summary>
        /// A folder
        /// </summary>
        Folder = 3,
    }
}
