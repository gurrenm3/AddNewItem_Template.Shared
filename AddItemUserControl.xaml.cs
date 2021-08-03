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
            this.contentPanel.Content = new NewItemView();
            this.Loaded += AddItemUserControl_Loaded;
        }

        private void AddItemUserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        ~AddItemUserControl() => Dispose();

        public void Start(ToolWindowPane toolWindow, string toolName)
        {
            _toolName = toolName;
            _toolWindow = toolWindow;
            _toolWindow.Caption = $"Add New {_toolName}";
            this.contentPanel.Content = new NewItemView();
            //((NewItemView)this.contentPanel.Content).isPublic.Content = checkboxText; 
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