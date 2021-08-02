using System.Windows.Controls;

namespace AddNewItem_Template.Shared
{
    public static class CheckBoxExtensions
    {
        public static bool IsChecked(this CheckBox checkBox)
        {
            return checkBox.IsChecked.HasValue && checkBox.IsChecked.Value;
        }
    }
}
