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
<<<<<<< Updated upstream
        switch (type)
        {
            case PowerupType.ExtraOrange:
                player.GetComponent<BombBehavior>().AddBomb();
                break;
            case PowerupType.MoreJuice:
                player.GetComponent<BombBehavior>().explosionRadius++;
                break;
        }
=======
        float time = 0f;
        switch (type)
        {
            case PowerupType.ExtraOrange:
                player.GetComponent<BombBehavior1>().AddBomb();
                time = 15f;
                break;
            case PowerupType.MoreJuice:
                player.GetComponent<BombBehavior1>().explosionRadius++;
                time = 10f;
                break;
        }

        StartCoroutine(PowerDown(player, time));
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPowerupPickup(other.gameObject);
<<<<<<< Updated upstream
        }
    }
=======
            Destroy(gameObject);
        }
    }

    private IEnumerator PowerDown(GameObject player, float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (type)
        {
            case PowerupType.ExtraOrange:
                player.GetComponent<BombBehavior1>().LoseBomb();
                break;

            case PowerupType.MoreJuice:
                player.GetComponent<BombBehavior1>().explosionRadius--;
                break;
        }
    }

    
>>>>>>> Stashed changes
}
