using EnvDTE;
using EnvDTE80;
using System;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;
namespace AddNewItem_Template.Shared
{
    /// <summary>
    /// Interaction logic for AddInterfaceToolWindowControl.
    /// </summary>
    public partial class AddItemUserControl : UserControl, IDisposable
    {
        public static AddItemUserControl instance;
        public ToolWindowPane _toolWindow;
        public string sourceFolder;
        public string _toolName;
        public DTE dte;
        public DTE2 dte2;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddItemUserControl"/> class.
        /// </summary>
        public AddItemUserControl()
        {
            this.InitializeComponent();
            instance = this;
            this.DataContext = new NewItemViewModel();
        }

        ~AddItemUserControl() => Dispose();

        public void Start(ToolWindowPane toolWindow, string toolName)
        {
            _toolName = toolName;
            _toolWindow = toolWindow;
            _toolWindow.Caption = $"Add New {_toolName}";
            dte2 = AddItemToolWindow.dte2;
            dte = _toolWindow.GetService<DTE>();
            sourceFolder = dte2.GetPathToSelectedItem();
        }

        public void Dispose()
        {
            instance = null;
            GC.SuppressFinalize(this);
        }
    }
}