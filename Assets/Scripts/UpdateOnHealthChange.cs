using UnityEngine;
using UnityEngine.UI;

public class UpdateOnHealthChange : MonoBehaviour
{
    IWithHealth healthContainer;
    Slider slider;
    [SerializeField] float percentageYellow = .39f;
    [SerializeField] float percentageGreen = .69f;
    [SerializeField] Image sliderImage;
    // Start is called before the first frame update
    void Start()
    {
        healthContainer = GetComponentInParent<IWithHealth>();
        if (healthContainer == null)
        {
            throw new System.Exception("Impossible to find the Health Container (A class implementing the IWithHealth interface)");
        }
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthContainer != null && slider != null)
        {
            //TODO change the warrior Starting health to something parametric (do not assume that this is attached to a warrior)
            float percentage = healthContainer.getCurrentHealth() / GameRules.WarriorStartingHealth();
            slider.SetValueWithoutNotify(percentage);
            if (percentage <= percentageYellow) sliderImage.color = Color.red;
            if (percentage > percentageYellow) sliderImage.color = Color.yellow;
            if (percentage > percentageGreen) sliderImage.color = Color.green;
        }

    }
}
