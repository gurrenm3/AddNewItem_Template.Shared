using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace AddNewItem_Template.Shared
{
    public static class TextSelectionExtensions
    {
        public static void Clear(this TextSelection textSelection)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            textSelection.SelectAll();
            textSelection.Delete();
        }

        /// <summary>
        /// Find a line that contains the textToFind
        /// </summary>
        /// <param name="textSelection"></param>
        /// <param name="textToFind"></param>
        /// <returns>Returns the line number of the textToFind, or -1 if it fails</returns>
        public static int FindLine(this TextSelection textSelection, string textToFind)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var lines = textSelection.Text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(textToFind))
                    return i;
            }

            return -1;
        }
    }
}
