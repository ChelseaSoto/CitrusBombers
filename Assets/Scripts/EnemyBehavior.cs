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
    private int chaseDirection;

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

    private void Start()
    {
        speedStashed = speed;
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        speed = 0;
        movement = Vector3.zero;

        
        NewDirection();
        StartCoroutine(ChangeDirectionInterval());
    }

    private void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
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

        if (Mathf.Abs(transform.position.x) % 1 <= 0.1f && Mathf.Abs(transform.position.y) % 1 <= 0.1f)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            Vector3 current = transform.position;

            if (movement.x == 0)
            {
                if (CheckAxis(current, Vector3.right))
                {
                    StartCoroutine(SetDirection(0));
                    return;
                }
                else if (CheckAxis(current, Vector3.left))
                {
                    StartCoroutine(SetDirection(2));
                    return;
                }
            }
            if (movement.y == 0)
            {

                if (CheckAxis(current, Vector3.down))
                {
                    StartCoroutine(SetDirection(3));
                    return;
                }
                else if (CheckAxis(current, Vector3.up))
                {
                    StartCoroutine(SetDirection(1));
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
            return false;
        }
        
        return true;
    }

    private IEnumerator SetDirection(int direction)
    {   
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
        
        yield return new WaitForSeconds(0.3f);
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
}