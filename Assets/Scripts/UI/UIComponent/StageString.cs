
public class StageString: Singleton<StageString>
{
    public string language = "en";

    public string settingTitle
    {
        get 
        {
            switch(language)
            {
                case "vn": return "Cài đặt";
                default: return "Settings";
            };
        }
    }
    public string quitBtnText
    {
        get 
        { 
            switch (language)
            {
                case "vn": return "Thoát";
                default: return "Quit";
            };
        }
    }
    public string restartBtnText
    {
        get 
        { 
            switch (language)
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
            switch (language)
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
            switch (language)
            {
                case "vn": return "Nhạc nền";
                default: return "Music";
            };
        }
    }

}