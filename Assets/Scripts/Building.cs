using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuyable
{
    [SerializeField] private double price;
    [SerializeField] private Sprite shopImage;

    public Sprite getImage()
    {
        return shopImage;
    }

    public double getPrice()
    {
        return price;
    }
}
