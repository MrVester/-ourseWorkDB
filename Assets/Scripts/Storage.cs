public static class Storage
{
    private static User _user;
    private static AudioSettings _audiosettings;
    private static ControlSettings _controlsettings;
    private static VideoSettings _videosettings;
    private static Settings _settings;


    public static User? user
    {
        get { return _user; }
        set
        {
            _user = (User)value;
        }
    }

    public static Settings? settings
    {
        get { return _settings; }
        set
        {
            if (value?.audiosettings == null) return;
            if (value?.controlsettings == null) return;
            if (value?.videosettings == null) return;

            _settings = (Settings)value;
        }
    }

}

