using UnityEngine;

public class Shop : MonoBehaviour
{
    ShopManager itemsManager = null;
    [SerializeField] private ShopItem itemViewTemplate;
    [SerializeField] private GameObject itemPosition;
    private int indexOfShownElement = 5;


    void Start()
    {
        itemsManager = ShopManager.getInstance();
        itemsManager.OnShopListChanged += refreshView;
    }

    public void scrollLeft()
    {
        Debug.Log("pressed on scroll left");
        if (indexOfShownElement > 0) indexOfShownElement--;
        refreshView(itemsManager.shopList);
    }
    public void scrollRight()
    {
        Debug.Log("pressed on scroll right");
        if (indexOfShownElement < itemsManager.shopList.Length - 1) indexOfShownElement++;
        refreshView(itemsManager.shopList);
    }

    private void refreshView(IBuyable[] newList)
    {
        var children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform toBeDestroyed in children)
        {
            if (toBeDestroyed.GetComponent<ShopItem>() != null) Destroy(toBeDestroyed.gameObject);
        }

        GameObject child = Instantiate(itemViewTemplate.gameObject, itemPosition.transform.position, Quaternion.identity);
        child.transform.SetParent(transform);
        var shopItem = child.GetComponent<ShopItem>();
        shopItem.price = newList[indexOfShownElement].getPrice();
        shopItem.image = newList[indexOfShownElement].getImage();
        shopItem.shop = this;
    }
}
