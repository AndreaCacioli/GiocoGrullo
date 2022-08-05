using TMPro;
using UnityEngine;

public class UpdateUIOnSelection : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI StepsText;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] TextMeshProUGUI DefenseText;
    [SerializeField] GameObject stepsSectionContainer;
    [SerializeField] GameObject healthSectionContainer;
    [SerializeField] GameObject defenseSectionContainer;

    // Start is called before the first frame update
    void Start()
    {
        SelectionManager.OnSelectionChanged += handleGUI;
    }

    private void handleGUI()
    {
        Selectable newSelectable = SelectionManager.getInstance().Selectable;
        canvas.enabled = newSelectable != null;

        if (newSelectable == null) return;

        Movable attachedMovable = newSelectable.GetComponent<Movable>();
        IWithHealth healthContainer = newSelectable.GetComponent<IWithHealth>();
        IWithDefense defenseContainer = newSelectable.GetComponent<IWithDefense>();

        NameText.SetText(newSelectable.name);

        stepsSectionContainer.SetActive(attachedMovable != null);
        if (attachedMovable != null)
        {
            StepsText.SetText(attachedMovable.GetMovementPoints().ToString());
        }

        healthSectionContainer.SetActive(healthContainer != null);
        if (healthContainer != null)
        {
            HealthText.SetText(healthContainer.getCurrentHealth().ToString());
        }

        defenseSectionContainer.SetActive(defenseContainer != null);
        if (defenseContainer != null)
        {
            DefenseText.SetText(defenseContainer.getDefenseValue().ToString());
        }

    }
}
