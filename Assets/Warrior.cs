using UnityEngine;

public class Warrior : MonoBehaviour, IWithHealth
{
    //TODO maybe separate the health container to another class and make this class the one that updates the graphics
    [SerializeField][Min(0)] float health;
    [SerializeField] Canvas canvas;
    Movable movable;
    Animator animator;


    public float getCurrentHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movable = GetComponent<Movable>();
        health = GameRules.WarriorStartingHealth();
    }

    // Update is called once per frame
    void Update()
    {
        canvas.gameObject.SetActive(SelectionManager.getInstance().SelectedMovable == movable);
        animator.SetBool("isRunning", movable.isRunning);
        if (health <= 0) animator.SetTrigger("Die");
        //TODO implement attack when we setup the combat system
    }
}
