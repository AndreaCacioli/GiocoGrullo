using TMPro;
using UnityEngine;

public class UpdateTopBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    private ResourcesManager resources;


    void Start()
    {
        resources = ResourcesManager.getInstance();
        resources.OnGoldValueChanged += onGoldValueChanged;
    }

    void onGoldValueChanged(double newValue)
    {
        goldText.SetText(newValue.ToString());
    }
}
