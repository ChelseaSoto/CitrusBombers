using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player")]
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Vector2 movement;
    public float speed = 5f;
    public static int lives = 3;

    [Header("UI")]
    public TextMeshProUGUI livesTxt;

    private bool isHit;
    
    private void OnEnable()
    {
        livesTxt.text = string.Format("" + lives);
    }

    private void Update()
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

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Damage Dealer"))
        {

            if(other.tag == "Explosion")
            {
                Debug.Log("solve overlap");
                OverlapAvoid();
            }
            else
            {
                lives--;
            }

            if (lives <= 0)
            {
                lives = 0;
            }

            livesTxt.text = string.Format("" + lives);
            
            if (lives <= 0)
            {
                StartCoroutine(DeathSequence());
            }

            FindObjectOfType<AudioManager>().Play("PlayerDamage");

        }
    }

    public IEnumerator DeathSequence()
    {
        GetComponent<BombBehavior1>().enabled = false;
        enabled = false;
        BombBehavior1.score = 0;
        
        lives = 0; 
        animator.SetFloat("Horizontal", rigidbody.position.x);
        animator.SetFloat("Vertical", rigidbody.position.y);
        animator.SetFloat("Speed", 0);
        animator.SetInteger("Lives", lives);

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Dead", true);
        lives = 3;

        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().CheckLoseState();
    }

    public void OverlapAvoid()
    {
        if(!isHit)
        {
            isHit = true;
            lives--;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1.1f);
        isHit = false;
    }
}
