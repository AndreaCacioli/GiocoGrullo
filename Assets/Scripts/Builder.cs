using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Building[] canBuild;
    [SerializeField] private int buildingCharges;
    void Start()
    {
        buildingCharges = GameRules.getInstance().buildersBaseCharges();
        //TODO add a way to improve the charges of new builders
    }
    public void build(Building building)
    {
        if (ResourcesManager.getInstance().gold < building.getPrice())
        {
            Debug.Log("You lack the required resource: gold");
            return;
        }
        ResourcesManager.getInstance().gold -= building.getPrice();
        Instantiate(building.gameObject);
        buildingCharges--;
        if (buildingCharges <= 0) Destroy(gameObject);
    }

    public void initializeShop()
    {
        ShopManager.getInstance().shopList = canBuild;
    }
}
