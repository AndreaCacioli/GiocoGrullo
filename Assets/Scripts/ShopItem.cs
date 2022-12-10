using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image imageRenderer;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    public void init(IBuyable buyable)
    {
        image = buyable.getImage();
        name = buyable.getName();
        price = buyable.getPrice();
    }

    private Sprite _image;
    public Sprite image
    {
        get { return _image; }
        set
        {
            _image = value;
            if (imageRenderer) imageRenderer.sprite = value;
            imageRenderer.SetNativeSize();
        }
    }

    private string _name;
    public new string name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
            if (nameText) nameText.SetText(value);
        }
    }

    private double _price;
    public double price
    {
        get { return _price; }
        set
        {
            _price = value;
            if (priceText) priceText.SetText(value.ToString());
        }
    }
}
