using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProjectFile {

    private string _Name;
    private string _PhysicalPath;
    [JsonIgnore]
    private Project _Project;

    public string Name {
        get { return _Name; }
        set { _Name = value; }
    }

    public string PhysicalPath {
        get { return _PhysicalPath; }
        set { _PhysicalPath = value; }
    }

    public string DownloadPath {
        get {
            string downPath = "";
            if (Project != null) {
                downPath = Project.Key + @"\";
            }
            downPath += Name;
            return downPath;
        }
    }
    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    public Project Project {
        get { return _Project; }
        set { _Project = value; }
    }


}
