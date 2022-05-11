public static class Storage
{
    private static User _user;
    private static SoundSettings _soundSettings;
    private static BindsSettings _bindsSettings;
    private static VideoSettings _videoSettings;
    public static User? user
    {
        get { return _user; }
        set
        {
            if (value?.id == null) return;
            if (value?.nickname == null) return;
            if (value?.login == null) return;


            _user = (User)value;
        }
    }

    public static SoundSettings? soundSettings
    {
        get { return _soundSettings; }
        set
        {
            if (value?.soundvalue == null) return;
            if (value?.musicvalue == null) return;



            _soundSettings = (SoundSettings)value;
        }
    }

    public static BindsSettings? bindsSettings
    {
        get { return _bindsSettings; }
        set
        {
            if (value?.attack == null) return;
            if (value?.activeitem == null) return;
            if (value?.movefront == null) return;
            if (value?.moveback == null) return;
            if (value?.moveleft == null) return;
            if (value?.moveright == null) return;



            _bindsSettings = (BindsSettings)value;
        }
    }

    public static VideoSettings? videoSettings
    {
        get { return _videoSettings; }
        set
        {
            //if (value?. == null) return;
            // if (value?. == null) return;



            _videoSettings = (VideoSettings)value;
        }
    }
}

