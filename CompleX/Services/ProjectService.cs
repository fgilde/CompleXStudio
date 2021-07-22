using System.Collections.Generic;

namespace CompleX.Services
{
    public static class ProjectService
    {
        public static bool IsProjectOpen
        {
            get { return CompleX_Studio.Instance.projectExplorer.IsProjectOpen; }
        }

        public static void OpenProject(string fileName)
        {
            CompleX_Studio.Instance.projectExplorer.LoadProject(fileName);
        }
        
        public static void CloseProject(bool closeOpenFiles)
        {
            CompleX_Studio.Instance.projectExplorer.CloseProject(closeOpenFiles);
        }

        public static IEnumerable<string> ProjectFiles
        {
            get { return CompleX_Studio.Instance.projectExplorer.ProjectFiles; }
        }

    }
}