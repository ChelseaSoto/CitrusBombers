using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
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
                player.GetComponent<BombBehavior>().AddBomb();
                break;
            case PowerupType.MoreJuice:
                player.GetComponent<BombBehavior>().explosionRadius++;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPowerupPickup(other.gameObject);
        }
    }
}
