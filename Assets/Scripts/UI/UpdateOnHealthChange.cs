using UnityEngine;
using UnityEngine.UI;

public class UpdateOnHealthChange : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float smoothing = .5f;
    IWithHealth healthContainer;
    Slider slider;
    Image sliderImage;

    private float lastPoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        healthContainer = GetComponentInParent<IWithHealth>();
        if (healthContainer == null)
        {
            Debug.LogError("Impossible to find the Health Container (A class implementing the IWithHealth interface)");
            Destroy(this);
        }
        slider = GetComponentInParent<Slider>();
        sliderImage = GetComponent<Image>();

    }



    // Update is called once per frame
    void Update()
    {
        if (healthContainer != null && slider != null)
        {
            if (lastPoints - healthContainer.getCurrentHealth() < 0.1f)
            {
                lastPoints = healthContainer.getCurrentHealth();
                return;
            }
            lastPoints = Mathf.Lerp(lastPoints, healthContainer.getCurrentHealth(), smoothing * Time.deltaTime);

            //TODO change the warrior Starting health to something parametric (do not assume that this is attached to a warrior)
            float percentage = lastPoints / GameRules.getInstance().WarriorStartingHealth();
            slider.SetValueWithoutNotify(percentage);
            if (percentage <= GameRules.getInstance().GetYellowValue()) sliderImage.color = Color.red;
            if (percentage > GameRules.getInstance().GetYellowValue()) sliderImage.color = Color.yellow;
            if (percentage > GameRules.getInstance().GetGreenValue()) sliderImage.color = Color.green;

        }

    }
}
