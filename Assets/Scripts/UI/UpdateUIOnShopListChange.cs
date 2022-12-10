using UnityEngine;

public class UpdateUIOnShopListChange : MonoBehaviour
{
    [SerializeField] private Shop shop;
    private ShopManager instance;
    void Start()
    {
        instance = ShopManager.getInstance();
        instance.OnShopListChanged += toggleShop;

    }

    private void toggleShop(IBuyable[] newList)
    {
        shop.gameObject.SetActive(newList != null); 
    }
}
