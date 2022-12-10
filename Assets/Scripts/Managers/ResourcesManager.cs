public class ResourcesManager
{
    public delegate void onGoldChangeDelegate(double newValue);
    public event onGoldChangeDelegate OnGoldValueChanged;

    private static ResourcesManager instance = null;
    private double _gold = 1000;

    public double gold
    {
        get { return _gold; }
        set
        {
            instance._gold = value;
            OnGoldValueChanged?.Invoke(value);
        }
    }

    private ResourcesManager()
    {
    }

    public static ResourcesManager getInstance()
    {
        if (instance == null)
        {
            instance = new ResourcesManager();
        }
        return instance;
    }
}
