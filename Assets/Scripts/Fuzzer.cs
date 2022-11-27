using System.Collections;
using UnityEngine;

public class Fuzzer : MonoBehaviour
{
    private ResourcesManager resources;
    private ShopManager buyables;
    [SerializeField] private Sprite mockImage;
    void Start()
    {
        resources = ResourcesManager.getInstance();
        buyables = ShopManager.getInstance();
        StartCoroutine("coroutine");
    }

    private IEnumerator coroutine()
    {
        while (true)
        {
            resources.gold = Random.Range(1, 1000000);
            int a = Random.Range(0, 2);
            if (a == 1) buyables.shopList = null;
            else buyables.shopList = randomBuyableList(10);
            yield return new WaitForSeconds(1);
        }
    }

    private IBuyable[] randomBuyableList(int maxNumberOfItems)
    {
        int len = Random.Range(1, maxNumberOfItems + 1);
        mockBuyable[] ret = new mockBuyable[len];
        for (int i = 0; i < len; i++)
        {
            ret[i] = new mockBuyable(mockImage);
            ret[i].mockPrice = Random.Range(1, 10000);
        }
        return ret;
    }

    private class mockBuyable : IBuyable
    {
        Sprite mImage;
        public double mockPrice = 100;

        public mockBuyable(Sprite image)
        {
            mImage = image;
        }

        public double getPrice()
        {
            return mockPrice;
        }

        public Sprite getImage()
        {
            return mImage;
        }
    }
}
