using System;
using System.Windows;
using System.Windows.Controls;

namespace AddNewItem_Template.Shared
{
    /// <summary>
    /// Interaction logic for NewItemView.
    /// </summary>
    public partial class ItemAlreadyExistsView : UserControl
    {
        Action yesOverwriteAction;
        Action noOverwriteAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewItemView"/> class.
        /// </summary>
        public ItemAlreadyExistsView()
        {
            this.InitializeComponent();
        }

        private void ItemAlreadyExists_Loaded(object sender, RoutedEventArgs e) => yesButton.Focus();

        public void ShouldOverwrite(Action yes, Action no)
        {
            yesOverwriteAction = yes;
            noOverwriteAction = no;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            yesOverwriteAction?.Invoke();
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            noOverwriteAction?.Invoke();
        }
    }
}