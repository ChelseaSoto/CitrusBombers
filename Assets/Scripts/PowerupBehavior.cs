using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{

    public enum PowerupType
    {
        ExtraOrange,
        MoreJuice,
    }

    public PowerupType type;

    private void OnPowerupPickup(GameObject player)
    {
        switch (type)
        {
            case PowerupType.ExtraOrange:
                player.GetComponent<BombBehavior1>().AddBomb();
                break;
            case PowerupType.MoreJuice:
                player.GetComponent<BombBehavior1>().AddRange();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PowerupNotification.firstPickup++;
            OnPowerupPickup(other.gameObject);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Pickup");
        }
    }
}
