using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    /// ////////////////
    //TODO Remove This!!!
    [SerializeField] public Shop shop;
    public void scrollLeft()
    {
        shop.scrollLeft();
    }
    /// ////////////////////////////////////////////
    private Sprite _image;
    public Sprite image
    {
        get { return _image; }
        set
        {
            _image = value;
            GetComponentInChildren<Image>().overrideSprite = value;
        }
    }

    private double _price;
    public double price
    {
        get { return _price; }
        set
        {
            _price = value;
            GetComponentInChildren<TextMeshProUGUI>().SetText(price.ToString());
        }
    }
}
