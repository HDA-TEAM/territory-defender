
public class UIString: Singleton<UIString>
{
    public string language = "en";

    public string StartBtnText
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

    public string LanguageTitle 
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

    public string VietnameseBtnText
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

    public string EnglishBtnText
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

    public string NewsTitle
    {
        get {
            switch (language)
            {
                case "vn": return "Bản tin";
                default: return "Update";
            };
        }
    }

    public string WaiterText
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

    public string SettingTitle
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

    public string QuitBtnText
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