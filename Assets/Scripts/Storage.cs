public static class Storage
{
    private static User _user;
    public static User? user
    {
        get { return _user; }
        set
        {
            if (value?.id == null) return;
            if (value?.name == null) return;

            _user = (User)value;
        }
    }
}
