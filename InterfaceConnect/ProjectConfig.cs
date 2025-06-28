using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceConnect
{
    public class ProjectConfig
    {
        private static ProjectConfig _instance;
        private ProjectConfig()
        {
            Load();
        }
        public static ProjectConfig Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ProjectConfig();
                }
                return _instance;
            }
        }

        public string ProjectName { get; set; }
        public string HospitalName { get; set; }
        public string ProjectDataBaseConnection { get; set; }

        private static string _configFolder = "InterfaceConfig";

        private static string _fileName = "ProjectConfig.json";

        private void Load()
        {
            var project_content = FileTool.ReadFromFile(_configFolder + "/" + _fileName);
            _instance = JsonTool.DeserializeObject<ProjectConfig>(project_content);
        }

        public bool Save()
        {
            var project_content = JsonTool.SerializeObject(this);
            return FileTool.SaveToFile(_configFolder + "/" + _fileName, project_content);
        }
    }
}
