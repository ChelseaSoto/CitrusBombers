using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject knightPrefab;
    public Rigidbody2D rigidbody;
    public Vector2 movement;
    private Vector2 position;
    public LayerMask knightLayerMask;

    public float speed = 4f;
    private bool changed = false;

    void Start()
    {   
        NewDirection();

        StartCoroutine(ChangeDirectionInterval());
    }

    void FixedUpdate()
    {
        transform.Translate(position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "Solid")
        {
            movement.x = -movement.x;
            movement.y = -movement.y;
        }
    }

    private void SetDirection(int direction)
    {
        if (transform.position.x % 1 != 0f && transform.position.y % 1 != 0f)
        {
            Debug.Log("failed to meet both: x divisible " + rigidbody.position.x % 1 +". y divisible " + rigidbody.position.y);
            return;
        }
        
        Debug.Log("x divisible " + rigidbody.position.x % 1 +". y divisible " + rigidbody.position.y);
        changed = true;

        switch (direction)
        {
            case 0:
                movement.x = 1;
                movement.y = 0;
                Debug.Log("Moving Right");
                break;
            case 1:
                movement.x = 0;
                movement.y = 1;
                Debug.Log("Moving Up");
                break;
            case 2:
                movement.x = -1;
                movement.y = 0;
                Debug.Log("Moving Left");
                break;
            case 3:
                movement.x = 0;
                movement.y = -1;
                Debug.Log("Moving Down");
                break;
        }
    }

    private void NewDirection()
    {
        position = transform.position;
        if (movement.x == 0)
        {
            Debug.Log("Checking x axis");
            if (CheckAxis(position, Vector2.right))
            {
                Debug.Log("Returned true to open right");
                SetDirection(0);
            }
            else if (CheckAxis(position, Vector2.left))
            {
                Debug.Log("Returned true to open left");
                SetDirection(2);
            }
        }
        else if (movement.y == 0)
        {
            Debug.Log("Checking y axis");
            if (CheckAxis(position, Vector2.up))
            {
                Debug.Log("Returned true to open up");
                SetDirection(1);
            }
            else if (CheckAxis(position, Vector2.down))
            {
                Debug.Log("Returned true to open down");
                SetDirection(3);
            }
        }
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

    IEnumerator ChangeDirectionInterval()
    {
        changed = false;
        yield return new WaitForSeconds(10f);

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
}
