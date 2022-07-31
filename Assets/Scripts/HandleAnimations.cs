using UnityEngine;

internal class HandleAnimations : MonoBehaviour, IWithHealth
{
    //TODO maybe separate the health container to another class and make this class the one that updates the graphics
    [SerializeField][Min(0)] float health;
    //TODO move health to a different script
    Canvas canvas;
    Movable movable;
    Animator animator;


    public float getCurrentHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Animator>(out animator)) animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("No Animator Attached to this Script!");
        movable = GetComponent<Movable>();
        health = GameRules.WarriorStartingHealth();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas != null) canvas.gameObject.SetActive(SelectionManager.getInstance().SelectedMovable == movable);
        if (animator != null) animator.SetBool("IsRunning", movable.IsRunning);
        if (animator != null) animator.SetBool("Action", movable.IsRunning);
        if (health <= 0) animator.SetTrigger("Die");
        //TODO implement attack when we setup the combat system
    }
}
