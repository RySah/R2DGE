
using System.Runtime.InteropServices;

namespace IR2DGE.Setup
{
    public struct SetupSln
    {
        private const string _pwsSetupScript = "setup_sln.ps1";
        private const string _bashSetupScript = "setup_sln.sh";

        private string _baseDirectoryName;
        private string _solutionNameName;
        private string _engineProjectNameName;
        private string _userProjectNameName;
        private string _engineProjectTemplatePathName;

        public string BaseDirectory = string.Empty;
        public string SolutionName = string.Empty;
        public string EngineProjectName = "R2DGE";
        public string EngineProjectTemplatePath = string.Empty;

        public SetupSln()
        {
            _baseDirectoryName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "$baseDirectory" : "base_directory";
            _solutionNameName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "$solutionName" : "solution_name";
            _engineProjectNameName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "$engineProjectName" : "engine_project_name";
            _userProjectNameName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "$userProjectName" : "user_project_name";
            _engineProjectTemplatePathName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "$engineProjectTemplatePath" : "engine_project_template_path";
        }
    }
}
