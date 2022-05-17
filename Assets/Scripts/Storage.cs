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

<<<<<<< HEAD
=======
    public static AudioSettings? audiosettings
    {
        get { return _settings.audiosettings; }
        set
        {
            if (value?.soundvalue == null) return;
            if (value?.musicvalue == null) return;



            _settings.audiosettings = (AudioSettings)value;
        }

    }

    public static ControlSettings? controlsettings
    {
        get { return _settings.controlsettings; }
        set
        {
            if (value?.attack == null) return;
            if (value?.activeitem == null) return;
            if (value?.movefront == null) return;
            if (value?.moveback == null) return;
            if (value?.moveleft == null) return;
            if (value?.moveright == null) return;



            _settings.controlsettings = (ControlSettings)value;
        }
    }

    public static VideoSettings? videosettings
    {
        get { return _settings.videosettings; }
        set
        {
            //if (value?. == null) return;
            // if (value?. == null) return;



            _settings.videosettings = (VideoSettings)value;
        }
    }
>>>>>>> 9cd2a4d80a09224a9f56eeae33f587d76cc7c7a7
}

