using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;

    void Start()
    {
        player = player.GetComponent<Transform>();
    }

    void Update()
    {   
        if (player.position.x >= -8f && player.position.x <= 8f)
        {
            transform.position = new Vector3(player.position.x, 0, -10);
        }
        else if (player.position.x <= -8f)
        {
            transform.position = new Vector3(-8, 0, -10);
        }

        else if (player.position.x >= 8f)
        {
            transform.position = new Vector3(8, 0, -10);
        }
    }
}
