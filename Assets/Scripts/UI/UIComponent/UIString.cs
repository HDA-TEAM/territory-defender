
public class UIString: Singleton<UIString>
{
    public string language = "en";

    public string startBtnText
    {
        get 
        { 
            switch (language)
            {
                case "vn": return "Bắt đầu";
                default: return "Start";
            };
        }
    }

    public string languageTitle 
    {
        get 
        { 
            switch (language)
            {
                case "vn": return "Ngôn ngữ";
                default: return "Language";
            };
        }
    }

    public string vietnameseBtnText
    {
        get 
        { 
            switch (language)
            {
                case "vn": return "Tiếng Việt";
                default: return "Vietnamese";
            };
        }
    }

    public string englishBtnText
    {
        get 
        { 
            switch (language)
            {
                case "vn": return "Tiếng Anh";
                default: return "English";
            };
        }
    }

    public string newsTitle
    {
        get {
            switch (language)
            {
                case "vn": return "Bản tin";
                default: return "Update";
            };
        }
    }

    public string waiterText
    {
        get 
        {
            switch(language)
            {
                case "vn": return "Chạm để tiếp tục";
                default: return "Tap to continue";
            };
        }
    }

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
            switch(language)
            {
                case "vn": return "Thoát game";
                default: return "Quit game";
            };
        }
    }

}