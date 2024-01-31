using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KnightBehavior : MonoBehaviour
{
    public Animator animator;
    private EnemyBehavior ebs;

    void Start()
    {
        ebs = GameObject.Find("knight").GetComponent<EnemyBehavior>();

    }

    void Update()
    {   
        animator.SetFloat("Horizontal", ebs.movement.x);
        animator.SetFloat("Vertical", ebs.movement.y);
        animator.SetFloat("Speed", ebs.speed);
    }
}

    