using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.Shell;

namespace AddNewItem_Template.Shared
{
    /// <summary>
    /// Interaction logic for NewItemView.
    /// </summary>
    public partial class NewItemView : UserControl
    {
        internal static NewItemView instance;
        bool tryingToCreateFile = false;
        AddItemUserControl uc;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewItemView"/> class.
        /// </summary>
        public NewItemView()
        {
            this.InitializeComponent();
            uc = AddItemUserControl.instance;
            instance = this;

            if (!string.IsNullOrEmpty(MyExtensionInfo.checkboxText))
                isPublic.Content = MyExtensionInfo.checkboxText;
            else
                isPublic.Visibility = Visibility.Hidden;
        }

        private void NewItemView_Loaded(object sender, RoutedEventArgs e)
        {
            itemNameTB.Focus();
        }

        string savePath;
        public void TryCreateFile()
        {
            string itemName = itemNameTB.Text;
            
            if (string.IsNullOrEmpty(itemName))
                return;

            tryingToCreateFile = true;
            string fileExtension = itemName.EndsWith(MyExtensionInfo.fileExtension) ? "" : MyExtensionInfo.fileExtension;
            savePath = $"{uc.sourceFolder}\\{itemName}{fileExtension}";

            if (File.Exists(savePath))
            {
                var view = new ItemAlreadyExistsView();
                uc.itemExistsPanel.Content = view;
                view.ShouldOverwrite(() =>
                {
                    File.Delete(savePath);
                    CreateFile(savePath);
                }, () =>
                {
                    itemNameTB.Focus();
                    tryingToCreateFile = false;
                });
                return;
            }

            CreateFile(savePath);
            
            uc.itemExistsPanel.Content = null;
            tryingToCreateFile = false;
        }

        private void CreateFile(string savePath)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            File.WriteAllText(savePath, MyExtensionInfo.GenerateFileText(uc.sourceFolder, GetSolutionFullName(), itemNameTB.Text, ref isPublic));
            itemNameTB.Text = "";
            uc._toolWindow.Close();
        }

        private string GetSolutionFullName()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return new FileInfo(uc.dte.Solution.FullName).Directory.FullName;
        }

        private void addButton_Click(object sender, RoutedEventArgs e) => TryCreateFile();

        private void cancelButton_Click(object sender, RoutedEventArgs e) => uc._toolWindow.Close();

        private void AddItemUserControl_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter && !tryingToCreateFile)
                TryCreateFile();
        }
    }
}