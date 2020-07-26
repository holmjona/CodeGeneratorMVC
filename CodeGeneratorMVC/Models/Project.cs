using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Project {
    private string _Key;
    private List<ProjectFile> _Files;

    public string Key {
        get { return _Key; }
        set { _Key = value; }
    }


    public List<ProjectFile> Files {
        get {
            if (_Files == null) _Files = new List<ProjectFile>();
            return _Files; 
        }
        set { _Files = value; }
    }

    private List<String> _ConversionMessages;

    public List<String> ConversionMessages {
        get {
            if (_ConversionMessages == null) _ConversionMessages = new List<string>();
            return _ConversionMessages;
        }
        set { _ConversionMessages = value; }
    }


}

