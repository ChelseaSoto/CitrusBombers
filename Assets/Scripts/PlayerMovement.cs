using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public Animator animator;
    private Vector2 movement;

    public float speed = 5f;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y < 0 && Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            animator.SetInteger("Direction", 0);
        }
        else if (movement.y > 0 && Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            animator.SetInteger("Direction", 1);
        }
        else if (movement.x < 0 && Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetInteger("Direction", 2);
        }
        else if (movement.x > 0 && Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            animator.SetInteger("Direction", 3);
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
    }
}
