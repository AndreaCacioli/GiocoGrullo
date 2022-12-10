using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopItem itemViewTemplate;
    [SerializeField] private GameObject itemPosition;
    [SerializeField] private float itemScale = .6f;
    private int indexOfShownElement = 0;

    void OnEnable()
    {
        setSelectablesActive(false);
        refreshView(ShopManager.getInstance().shopList);
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
        ShopManager.getInstance().OnShopListChanged += (value) => { indexOfShownElement = 0; };
        ShopManager.getInstance().OnShopListChanged += refreshView;
    }

    public void scrollLeft()
    {
        if (indexOfShownElement > 0) indexOfShownElement--;
        refreshView(ShopManager.getInstance().shopList);
    }
    public void scrollRight()
    {
        if (indexOfShownElement < ShopManager.getInstance().shopList.Length - 1) indexOfShownElement++;
        refreshView(ShopManager.getInstance().shopList);
    }

    private void refreshView(IBuyable[] newList)
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform toBeDestroyed in children)
        {
            if (toBeDestroyed.GetComponent<ShopItem>() != null) Destroy(toBeDestroyed.gameObject);
        }
        if (newList == null) return;
        GameObject child = Instantiate(itemViewTemplate.gameObject, itemPosition.transform.position, Quaternion.identity);
        child.transform.SetParent(transform);
        child.transform.localScale = new Vector3(itemScale, itemScale, 1);
        ShopItem shopItem = child.GetComponent<ShopItem>();
        shopItem.init(newList[indexOfShownElement]);
    }

    public void buy()
    {
        ShopManager.getInstance().buy(ShopManager.getInstance().shopList[indexOfShownElement]);
    }
}
