using System;
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
    public void build(IBuyable buyable)
    {
        if (!(buyable is Building)) throw new Exception("Builders can only build buildings");
        Building building = (Building)buyable;
        GraphNode myTile = DataStructureManager.getInstance().getNode(transform.position);
        Vector3 myTileCoordinates = DataStructureManager.getInstance().getWorldCoordinates(myTile);
        Instantiate(building.gameObject, myTileCoordinates, Quaternion.identity);
        buildingCharges--;
        ShopManager.getInstance().clearShopList();
        ShopManager.getInstance().onItemBought -= build;
        if (buildingCharges <= 0) Destroy(gameObject);
    }

    public void initializeShop()
    {
        ShopManager.getInstance().shopList = canBuild;
        ShopManager.getInstance().onItemBought += build;
    }
}
