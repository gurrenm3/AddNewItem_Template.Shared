using EnvDTE80;
using System;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;

namespace AddNewItem_Template.Shared
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("06c4da79-dcf9-4c16-bee0-c55ef312d5e2")]
    public class AddItemToolWindow : ToolWindowPane, IStart
    {
        public AddItemUserControl UserControl { get; set; }
        public static DTE2 dte2;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItemToolWindow"/> class.
        /// </summary>
        public AddItemToolWindow() : base(null)
        {
            this.Caption = $"Add New Item";
            UserControl = new AddItemUserControl();
            this.Content = UserControl;
        }

        public void Start()
        {
            UserControl.Start(this, MyExtensionInfo.itemName);
        }
    }
}
