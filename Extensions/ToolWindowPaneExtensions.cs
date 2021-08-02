using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Threading.Tasks;

namespace AddNewItem_Template.Shared
{
    public static class ToolWindowPaneExtensions
    {
        /// <summary>
        /// Close this ToolWindow
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static int Close(this ToolWindowPane window, __FRAMECLOSE closeOption = __FRAMECLOSE.FRAMECLOSE_NoSave)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return ((IVsWindowFrame)window.Frame).CloseFrame((uint)closeOption);
        }

        /// <summary>
        /// Get a service from the ToolWindowPane
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="window"></param>
        /// <returns></returns>
        public static T GetService<T>(this ToolWindowPane window) where T : class
        {
            return (T)window.GetService<T,T>();
        }

        /// <summary>
        /// Get the path of the selected item in Visual Studio that opened the tool window
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        /// <remarks>Taken from: https://stackoverflow.com/a/15406802/11787265 </remarks>
        public static string GetSourceFilePath(this ToolWindowPane window, out ProjectItem projItem)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            EnvDTE80.DTE2 _applicationObject = window.GetService<DTE>() as EnvDTE80.DTE2;
            UIHierarchy uih = _applicationObject.ToolWindows.SolutionExplorer;
            Array selectedItems = (Array)uih.SelectedItems;

            if (selectedItems != null)
                foreach (UIHierarchyItem selItem in selectedItems)
                {
                    projItem = (ProjectItem)selItem.Object;
                    return projItem.Properties.Item("FullPath").Value.ToString();
                }

            projItem = null;
            return string.Empty;
        }

        /// <summary>
        /// Get the path of the selected item in Visual Studio that opened the tool window
        /// </summary>
        /// <returns></returns>
        /// <remarks>Taken from: https://stackoverflow.com/a/64558307/11787265 </remarks>
        public static async Task<string> GetSourceFilePath(this ToolWindowPane window)
        {
            var dte2 = await ServiceProvider.GetGlobalServiceAsync(typeof(SDTE)) as DTE2;
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
                        return projectItem?.Properties?.Item("FullPath")?.Value?.ToString();

                    // Or get project object if selectedItem is a project
                    var sproject = selectedItem.Project;
                    if (sproject != null)
                        return sproject.Properties?.Item("ProjectFolder")?.Value.ToString();
                }
            }

            return null;
        }
    }
}
