using UnityEngine;

public class Shop : MonoBehaviour
{
    ShopManager itemsManager = null;
    [SerializeField] private ShopItem itemViewTemplate;
    [SerializeField] private GameObject itemPosition;
    [SerializeField] private float itemScale = .6f;
    private int indexOfShownElement = 0;

    void OnEnable()
    {
        setSelectablesActive(false);
    }

    void OnDisable()
    {
        setSelectablesActive(true);
    }

    private void setSelectablesActive(bool value)
    {
        var selectables = FindObjectsOfType<Selectable>(true);
        foreach (Selectable selectable in selectables)
        {
            selectable.enabled = value;
        }
    }

    void Start()
    {
        itemsManager = ShopManager.getInstance();
        itemsManager.OnShopListChanged += refreshView;
    }

    public void scrollLeft()
    {
        if (indexOfShownElement > 0) indexOfShownElement--;
        refreshView(itemsManager.shopList);
    }
    public void scrollRight()
    {
        if (indexOfShownElement < itemsManager.shopList.Length - 1) indexOfShownElement++;
        refreshView(itemsManager.shopList);
    }

    private void refreshView(IBuyable[] newList)
    {
        if (newList == null) return;
        var children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform toBeDestroyed in children)
        {
            if (toBeDestroyed.GetComponent<ShopItem>() != null) Destroy(toBeDestroyed.gameObject);
        }

        GameObject child = Instantiate(itemViewTemplate.gameObject, itemPosition.transform.position, Quaternion.identity);
        child.transform.SetParent(transform);
        child.transform.localScale = new Vector3(itemScale, itemScale, 1);
        var shopItem = child.GetComponent<ShopItem>();
        shopItem.price = newList[indexOfShownElement].getPrice();
        shopItem.image = newList[indexOfShownElement].getImage();
    }
}
