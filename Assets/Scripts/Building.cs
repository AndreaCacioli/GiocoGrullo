using UnityEngine;

public class Building : MonoBehaviour, IBuyable
{
    [SerializeField] private double price;
    [SerializeField] private Sprite shopImage;

    public string getName()
    {
        return gameObject.name;
    }
    public Sprite getImage()
    {
        return shopImage;
    }

    public double getPrice()
    {
        return price;
    }
}
