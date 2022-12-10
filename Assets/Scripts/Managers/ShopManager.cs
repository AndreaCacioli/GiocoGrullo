using UnityEngine;

public class ShopManager
{
    public delegate void onShopListChangedDelegate(IBuyable[] newList);
    public event onShopListChangedDelegate OnShopListChanged;
    public delegate void onItemBoughtDelegate(IBuyable bought);
    public event onItemBoughtDelegate onItemBought;
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

    public void clearShopList()
    {
        shopList = null;
    }

    public void buy(IBuyable desiredItem)
    {
        if (ResourcesManager.getInstance().gold < desiredItem.getPrice())
        {
            Debug.Log("You lack the required resource: gold");
            return;
        }
        ResourcesManager.getInstance().gold -= desiredItem.getPrice();
        onItemBought?.Invoke(desiredItem);
    }

}
