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
        speedStashed = speed;
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        speed = 0;
        movement = Vector3.zero;

        Debug.Log("Stashed speed: "+speedStashed+" Current speed is: "+speed);
        
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

    private void NewDirection()
    {
        Debug.Log("Current location: "+transform.position.x+", "+transform.position.y);

        if (Mathf.Abs(transform.position.x) % 1 <= 0.1f && Mathf.Abs(transform.position.y) % 1 <= 0.1f)
        {
            Debug.Log("remainder x:" + Mathf.Abs(transform.position.x) % 1 + " Remainder y: " + Mathf.Abs(transform.position.y));
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            Debug.Log("Current location: " + transform.position.x + ", " + transform.position.y);
            Vector3 current = transform.position;
            Debug.Log("Current location: " + current.x + ", " + current.y);

            if (movement.x == 0)
            {
                if (CheckAxis(current, Vector3.right))
                {
                    StartCoroutine(SetDirection(0));
                    Debug.Log("Going right");
                    return;
                }
                else if (CheckAxis(current, Vector3.left))
                {
                    StartCoroutine(SetDirection(2));
                    Debug.Log("Going left");
                    return;
                }
            }
            if (movement.y == 0)
            {

                if (CheckAxis(current, Vector3.down))
                {
                    StartCoroutine(SetDirection(3));
                    Debug.Log("Going down");
                    return;
                }
                else if (CheckAxis(current, Vector3.up))
                {
                    StartCoroutine(SetDirection(1));
                    Debug.Log("Going up");
                    return;
                }
            }
        }

        changed = false;
        return;

        
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

    private IEnumerator SetDirection(int direction)
    {   
        changed = true;

        Debug.Log("Assigning direction");
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
        
        yield return new WaitForSeconds(0.3f);
        Debug.Log("Moving Now");
        speed = speedStashed;
    }

    private IEnumerator ChangeDirectionInterval()
    {
        changed = false;

        yield return new WaitForSeconds(Random.Range(5f, 15f));

        while (!changed)
        {
            if(transform.position.x % 1 == 0 || transform.position.y % 1 == 0)
            {
                speed = 0;
                Debug.Log("Stopped moving. checking to round.");
                NewDirection();
            }
            speed = speedStashed;
            yield return new WaitForSeconds(0.05f);
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