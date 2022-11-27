using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image imageRenderer;
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
