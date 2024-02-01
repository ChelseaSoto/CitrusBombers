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
                player.GetComponent<BombBehavior1>().explosionRadius++;
                break;
        }

        StartCoroutine(PowerDown(player));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPowerupPickup(other.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator PowerDown(GameObject player)
    {
        switch (type)
        {
            case PowerupType.ExtraOrange:
                yield return new WaitForSeconds(15f);
                player.GetComponent<BombBehavior1>().LoseBomb();
                break;
            
            case PowerupType.MoreJuice:
                yield return new WaitForSeconds(10f);
                player.GetComponent<BombBehavior1>().explosionRadius--;
                break;
        }
    }
}
