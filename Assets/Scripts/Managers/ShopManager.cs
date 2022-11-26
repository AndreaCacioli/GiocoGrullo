public class ShopManager
{
    public delegate void onShopListChangedDelegate(IBuyable[] newList);
    public event onShopListChangedDelegate OnShopListChanged;
    private static ShopManager instance;

    private IBuyable[] _shopList;
    public IBuyable[] shopList
    {
        get { return _shopList; }
        set
        {
            _shopList = value;
            OnShopListChanged?.Invoke(value);
        }
    }

    private ShopManager() { }

    public static ShopManager getInstance()
    {
        if (instance == null)
        {
            instance = new ShopManager();
        }
        return instance;
    }

}
