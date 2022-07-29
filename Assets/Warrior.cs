using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    float health;
    Movable movable;
    Animator animator;

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
        animator.SetBool("isRunning", movable.isRunning);
        if (health <= 0) animator.SetTrigger("Die");
        //TODO implement attack when we setup the combat system
    }
}
