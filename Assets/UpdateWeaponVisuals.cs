using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common;
using UnityEngine;

public class UpdateWeaponVisuals : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Character character;
    IWithOffensiveTools OToolsContainer;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        spriteRenderer = GetComponentInChildren<MeleeWeapon>().GetComponent<SpriteRenderer>();
        OToolsContainer = GetComponent<IWithOffensiveTools>();
        OToolsContainer.onOffensiveToolChanged += changeSprite;

    }

    private void changeSprite(IOffenseTool weapon)
    {
        Sprite sprite = ((Weapon)weapon).sprite;
        var list = SpriteCollection.Instances["Megapack"].MeleeWeapon1H;
        foreach (var v in list)
        {
            if (v.Sprite.name == sprite.name)
            {
                character.Equip(v, HeroEditor.Common.Enums.EquipmentPart.MeleeWeapon1H);
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
