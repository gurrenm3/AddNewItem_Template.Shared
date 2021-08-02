using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.IO;

namespace AddNewItem_Template.Shared
{
	public static class DTE2Extensions
	{
		public static string GetPathToSelectedItem(this DTE2 dte2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var selectedItems = dte2.SelectedItems;
            if (selectedItems.MultiSelect || selectedItems.Count > 0)
            { //Use either/or
                for (short i = 1; i <= selectedItems.Count; i++)
                {
                    //Get selected item
                    var selectedItem = selectedItems.Item(i);
                    //Get associated project item (selectedItem.ProjectItem  )
                    //If selectedItem is a project, then selectedItem.ProjectItem will be null,
                    //and selectedItem.Project will not be null.
                    var projectItem = selectedItem.ProjectItem;
                    if (projectItem != null)
                        return projectItem.Properties.Item("FullPath").Value.ToString();

                    // Or get project object if selectedItem is a project
                    var sproject = selectedItem.Project;
                    if (sproject != null)
                        return new FileInfo(sproject.FullName).Directory.FullName;
                }
            }
            return "";
        }
	}
}