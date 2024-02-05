using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBehavior : MonoBehaviour
{   
    public Rigidbody2D rigidbody;
    public Animator animator;
    public Vector3 movement;
    private Vector2 position;
    public LayerMask knightLayerMask;
    public Transform castPoint;

    public float speed = 0f;
    private float speedStashed;
    private bool changed = false;

    public enum EnemyType
    {
        red,
        green,
        gold,
    }
    public EnemyType type;

    private GameManager gms;

    void Start()
    {   
        speed = 0;
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        speedStashed = speed;

        movement = Vector3.zero;

        NewDirection();

        StartCoroutine(ChangeDirectionInterval());

    }

    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", speed);

        if (gameObject.tag == "Red")
        {
            CanSeePlayer();

        }
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
            givePoints();
            Destroy(gameObject);
        }
    }   



    private void SetDirection(int direction)
    {
        if (transform.position.x % 1 >= 0.001f || transform.position.y % 1 >= 0.001f)
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
        Vector3 current = transform.position;

        if (movement.x == 0)
        {
            if (CheckAxis(current, Vector3.right))
            {
                SetDirection(0);
                return;
            }
            else if (CheckAxis(current, Vector3.left))
            {
                SetDirection(2);
                return;
            }
        }
        if (movement.y == 0)
        {
            
            if (CheckAxis(current, Vector3.down))
            {
                SetDirection(3);
                return;
            }
            else if (CheckAxis(current, Vector3.up))
            {
                SetDirection(1);
                return;
            }
        }
<<<<<<< Updated upstream
=======

        speed = 0;
>>>>>>> Stashed changes
    }

    private bool CheckAxis(Vector3 location, Vector3 direction)
    {   
        location += direction;

        if (Physics2D.OverlapBox(location, Vector2.one / 2f, 0f, knightLayerMask))
        {
            Debug.Log("Obstacle detected at: "+location);
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
            speed = 0;
            if(transform.position.x % 1 == 0 || transform.position.y % 1 == 0 )
            {
                NewDirection();
            }
            speed = speedStashed;
            yield return new WaitForSeconds(0.4f);
        }

        StartCoroutine(ChangeDirectionInterval());
    }

    private void givePoints()
    {
        switch (type)
        {
            case EnemyType.gold:
                GameObject.Find("Player").GetComponent<BombBehavior1>().AddScore(5);
                break;
            case EnemyType.green:
                GameObject.Find("Player").GetComponent<BombBehavior1>().AddScore(10);
                break;
            case EnemyType.red:
                GameObject.Find("Player").GetComponent<BombBehavior1>().AddScore(50);
                break;
        }
    }

    private void CanSeePlayer()
    {
        bool val = false;

        Vector2 rightView = castPoint.position + Vector3.right * 30;
    }
}
