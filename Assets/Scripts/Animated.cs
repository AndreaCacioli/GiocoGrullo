using UnityEngine;

internal class Animated : MonoBehaviour
{
    private Canvas canvas;
    private Movable movable;
    private Animator animator;
    Selectable selectable;
    private IWithHealth healthContainer;

    //Used to only show the selection animation once
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas != null && movable != null) canvas.gameObject.SetActive(SelectionManager.getInstance().SelectedMovable == movable);
        if (movable != null) animator.SetBool("IsRunning", movable.IsRunning);
        if (movable != null) animator.SetBool("Action", movable.IsRunning);
        if (healthContainer != null && healthContainer.getCurrentHealth() <= 0) animator.SetTrigger("Die");
        //TODO implement attack when we setup the combat system


        if (!done && selectable != null && selectable == SelectionManager.getInstance().Selectable)
        {
            animator.SetTrigger("Hit");
            done = true;
        }
        if (done)
        {
            if (selectable != null && selectable != SelectionManager.getInstance().Selectable) done = false;
        }

    }
}
