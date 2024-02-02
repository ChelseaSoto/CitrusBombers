using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAdvancedBehavior : MonoBehaviour
{   
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Vector2 movement;
    private Vector2 position;
    public LayerMask knightLayerMask;

    public float speed = 4f;
    private float speedStashed;
    private bool changed = false;

    private GameManager gms;
    public GameObject player;

    void Start()
    {   
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        speedStashed = speed;

        NewDirection();

        StartCoroutine(ChangeDirectionInterval());
    }

    void Update()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", speed);

        //Time to chase
        if (CanSeePlayer())
        {
            StopCoroutine(ChangeDirectionInterval());
            ChasePlayer();
        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.tag == "Solid")
        {
            movement.x = -movement.x;
            movement.y = -movement.y;
        }

        if (other.tag == "Player")
        {
            movement.x = -movement.x;
            movement.y = -movement.y;
        }

        if (other.tag == "Explosion")
        {
            gms.enemyCount--;
            Destroy(gameObject);
        }
    }

    private void SetDirection(int direction)
    {
        position = rigidbody.position;

        speed = 0;
        movement = Vector2.zero;

        if (position.x % 1 >= 0.0001f && transform.position.y % 1 >= 0.0001f)
        {
            speed = speedStashed;
            return;
        }

        changed = true;

        switch (direction)
        {
            case 0:
                movement.x = 1;
                movement.y = 0;
                break;
            case 1:
                movement.x = 0;
                movement.y = 1;
                break;
            case 2:
                movement.x = -1;
                movement.y = 0;
                break;
            case 3:
                movement.x = 0;
                movement.y = -1;
                break;
        }
        
        speed = speedStashed;
    }

    private void NewDirection()
    {   
        position = rigidbody.position;

        if (movement.x == 0)
        {
            if (CheckAxis(position, Vector2.right))
            {
                SetDirection(0);
            }
            else if (CheckAxis(position, Vector2.left))
            {
                SetDirection(2);
            }
        }
        if (movement.y == 0)
        {
            
            if (CheckAxis(position, Vector2.down))
            {
                SetDirection(3);
            }
            else if (CheckAxis(position, Vector2.up))
            {
                SetDirection(1);
            }
        }

        //SetDirection(2);
    }

    private bool CheckAxis(Vector2 location, Vector2 direction)
    {   
        location += direction;

        if (Physics2D.OverlapBox(location, Vector2.one / 2f, 0f, knightLayerMask))
        {
            return false;
        }
        
        return true;
    }

    private IEnumerator ChangeDirectionInterval()
    {
        changed = false;

        yield return new WaitForSeconds(Random.Range(5f, 15f));

        while (!changed)
        {
            if(!changed)
            {
                NewDirection();
            }
            yield return new WaitForSeconds(0.4f);
        }

        StartCoroutine(ChangeDirectionInterval());
    }

    private bool CanSeePlayer()
    {
        return false;
    } 

    private void ChasePlayer()
    {
        if (Mathf.Round(rigidbody.position.x) == Mathf.Round(player.GetComponent<Rigidbody2D>().position.x) && Mathf.Round(rigidbody.position.y) > Mathf.Round(player.GetComponent<Rigidbody2D>().position.y))
        {
            //move down
            SetDirection(3);
        }
        if (Mathf.Round(rigidbody.position.x) == Mathf.Round(player.GetComponent<Rigidbody2D>().position.x) && Mathf.Round(rigidbody.position.y) < Mathf.Round(player.GetComponent<Rigidbody2D>().position.y))
        {
            //move up
            SetDirection(1);
        }
        if (Mathf.Round(rigidbody.position.y) == Mathf.Round(player.GetComponent<Rigidbody2D>().position.y) && Mathf.Round(rigidbody.position.x) > Mathf.Round(player.GetComponent<Rigidbody2D>().position.x))
        {
            //move left
            SetDirection(2);
        }
        if (Mathf.Round(rigidbody.position.y) == Mathf.Round(player.GetComponent<Rigidbody2D>().position.y) && Mathf.Round(rigidbody.position.x) < Mathf.Round(player.GetComponent<Rigidbody2D>().position.x))
        {
            //move right
            SetDirection(0);
        }
    }
}
