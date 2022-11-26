using System.Collections;
using UnityEngine;

internal class Animated : MonoBehaviour
{
    private Canvas canvas;
    private Movable movable;
    private Animator animator;
    Selectable selectable;
    private IWithHealth healthContainer;
    private ICanCombat combatScript;
    private SpriteRenderer[] sprites;
    private BoxCollider2D col;
    [SerializeField] private float deathFadeOutSpeed;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        if (!TryGetComponent<Animator>(out animator)) animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator Attached to this Script!");
            Destroy(this);
        }
        movable = GetComponent<Movable>();
        canvas = GetComponentInChildren<Canvas>();
        healthContainer = GetComponentInParent<IWithHealth>();
        selectable = GetComponent<Selectable>();
        combatScript = GetComponent<ICanCombat>();


        //Events
        SelectionManager.OnSelectionChanged += onSelection;
        if (healthContainer != null) healthContainer.onHealthChanged += dieOnHealthBelowZero;
        if (combatScript != null) combatScript.onAttack += swingWeapon;
        if (combatScript != null) combatScript.onAttack += lookAtEnemy;
    }

    private void lookAtEnemy(GameObject enemy)
    {
        if (enemy.transform.position.x < transform.position.x)
        {
            Movable.LookAt(gameObject, enemy.transform.position);
        }
    }


    private void OnDestroy()
    {
        SelectionManager.OnSelectionChanged -= onSelection;
        if (healthContainer != null) healthContainer.onHealthChanged -= dieOnHealthBelowZero;
        if (combatScript != null)
        {
            combatScript.onAttack -= swingWeapon;
            combatScript.onAttack -= lookAtEnemy;
        }
    }

    private void swingWeapon(GameObject enemy)
    {
        animator.SetTrigger("Attack");
    }

    private IEnumerator fadeOut()
    {
        float start = sprites[0].color.a;
        while (start > 0)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                Color color = sprite.color;
                color.a -= deathFadeOutSpeed * Time.deltaTime;
                sprite.color = color;
            }
            yield return null;
        }
    }

    private void dieOnHealthBelowZero(float amount)
    {
        if (healthContainer.getCurrentHealth() <= 0)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 10f);
            Destroy(col);
            StartCoroutine(fadeOut());
        }
    }

    private void onSelection()
    {
        if (selectable != null && selectable == SelectionManager.getInstance().Selectable)
        {
            animator.SetTrigger("Hit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas != null && movable != null) canvas.enabled = SelectionManager.getInstance().SelectedMovable == movable || CombatManager.getInstance().isFighting(GetComponent<Warrior>());
        if (movable != null) animator.SetBool("IsRunning", movable.IsRunning);
        //TODO implement attack when we setup the combat system
    }
}
