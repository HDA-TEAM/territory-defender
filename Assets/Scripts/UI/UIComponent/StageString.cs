
using UnityEngine.Serialization;

public class StageString: Singleton<StageString>
{
    public string _language = "en";

    public string SettingTitle
    {
        get 
        {
            switch(_language)
            {
                case "vn": return "Cài đặt";
                default: return "Settings";
            };
        }
    }
    public string QuitBtnText
    {
        get 
        { 
            switch (_language)
            {
                case "vn": return "Thoát";
                default: return "Quit";
            };
        }
    }
    public string RestartBtnText
    {
        get 
        { 
            switch (_language)
            {
                case "vn": return "Chơi lại";
                default: return "Restart";
            };
        }
    }
    

    public string soundBtnText
    {
        get 
        { 
            switch (_language)
            {
                case "vn": return "Âm thanh";
                default: return "Sound";
            };
        }
    }

    public string musicBtnText
    {
        get 
        { 
            switch (_language)
            {
                case "vn": return "Nhạc nền";
                default: return "Music";
            };
        }
    }

}