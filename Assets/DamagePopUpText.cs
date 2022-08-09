using TMPro;
using UnityEngine;

public class DamagePopUpText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI sampleText;
    [SerializeField] private float damageTextVisibilityTime = 1f;
    [SerializeField] private float radius = 3f;
    Canvas canvas;
    IWithHealth healthContainer;

    // Start is called before the first frame update
    void Start()
    {
        healthContainer = GetComponentInParent<IWithHealth>();
        if (healthContainer == null)
        {
            Debug.LogError("Impossible to find the Health Container (A class implementing the IWithHealth interface)");
            Destroy(this);
        }
        canvas = GetComponent<Canvas>();
        healthContainer.onHealthChanged += ShowDamageValue;
    }

    private void ShowDamageValue(float value)
    {
        TextMeshProUGUI instance = Instantiate(sampleText, canvas.transform);
        instance.SetText(value.ToString());
        float r = Random.Range(-radius, radius);
        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + r, rectTransform.anchoredPosition.y + Mathf.Abs(r));
        Destroy(instance.gameObject, damageTextVisibilityTime);
    }

}
