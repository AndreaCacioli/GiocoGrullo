using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIOnSelection : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI StepsText;
    [SerializeField] TextMeshProUGUI HealthText;
    [SerializeField] TextMeshProUGUI DefenseText;
    [SerializeField] GameObject stepsSectionContainer;
    [SerializeField] GameObject healthSectionContainer;
    [SerializeField] GameObject defenseSectionContainer;
    [SerializeField] Button BuildButton;

    void Start()
    {
        SelectionManager.OnSelectionChanged += handleGUI;
    }

    private void handleGUI()
    {
        Selectable newSelectable = SelectionManager.getInstance().Selectable;
        panel.SetActive(newSelectable != null);
        if (newSelectable == null) return;

        NameText.SetText(newSelectable.name);
        updateStepsSection(newSelectable);
        updateHealthSection(newSelectable);
        updateDefenseSection(newSelectable);
        toggleBuildButton(newSelectable);
    }

    private void toggleBuildButton(Selectable newSelectable)
    {
        Builder builderComponent = newSelectable.GetComponent<Builder>();
        BuildButton.interactable = builderComponent != null;
        if (builderComponent == null) return;
        //TODO find out why when the shop is shown, it shows no item until you scroll
        BuildButton.onClick.AddListener(builderComponent.initializeShop);
    }

    private void updateDefenseSection(Selectable newSelectable)
    {
        IWithDefense defenseContainer = newSelectable.GetComponent<IWithDefense>();
        defenseSectionContainer.SetActive(defenseContainer != null);
        if (defenseContainer != null)
        {
            DefenseText.SetText(defenseContainer.getDefenseValue().ToString());
        }
    }

    private void updateHealthSection(Selectable newSelectable)
    {
        IWithHealth healthContainer = newSelectable.GetComponent<IWithHealth>();
        healthSectionContainer.SetActive(healthContainer != null);
        if (healthContainer != null)
        {
            HealthText.SetText(healthContainer.getCurrentHealth().ToString());
        }
    }

    private void updateStepsSection(Selectable newSelectable)
    {
        Movable attachedMovable = newSelectable.GetComponent<Movable>();
        stepsSectionContainer.SetActive(attachedMovable != null);
        if (attachedMovable != null)
        {
            StepsText.SetText(newSelectable.GetComponent<Movable>().GetMovementPoints().ToString());
        }
    }
}
