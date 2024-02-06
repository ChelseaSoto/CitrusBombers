using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BombBehavior1: MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public float fuseTime = 3f;
    public int bombCount = 1;
    private int bombsRemaining;
    private int bombsCurrent;

    [Header("Explosion")]
    public ExplosionBehavior explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public PowerupSpawnBehavior spawnerPrefab;

    [Header("UI")]
    public static int score;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI rangeTxt;
    public TextMeshProUGUI countTxt;


    private void OnEnable()
    {
        bombsRemaining = bombCount;
        bombsCurrent = bombsRemaining;
    } 

    private void Start()
    {
        scoreTxt.text = string.Format("" + score);
        rangeTxt.text = string.Format("" + explosionRadius);
        countTxt.text = string.Format("" + bombCount);
    }

    private void Update()
    {
        if (bombsCurrent > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaceBomb());
            FindObjectOfType<AudioManager>().Play("BombPlace");
        }

        if (bombsCurrent > bombsRemaining)
        {
            bombsCurrent = bombsRemaining;
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsCurrent--;

        yield return new WaitForSeconds(fuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        ExplosionBehavior explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActive(explosion.center);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombsCurrent++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            clearDestructible(position, direction);
            return;
        }
        
        ExplosionBehavior explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActive(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, direction, length - 1);

        FindObjectOfType<AudioManager>().Play("BombExplode");
    }

    private void clearDestructible(Vector2 position, Vector2 direction)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null)
        {
            AddScore(2);
            ExplosionBehavior explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActive(explosion.end);
            explosion.SetDirection(direction);
            explosion.DestroyAfter(explosionDuration);
            Destroy(explosion.gameObject, explosionDuration);
            Instantiate(spawnerPrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        bombCount++;
        bombsRemaining = bombCount;
        bombsCurrent++;
        countTxt.text = string.Format("" + bombCount);
        StartCoroutine(PowerDown(0));
    }

    public void LoseBomb()
    {
        bombCount--;
        bombsRemaining = bombCount;
        bombsCurrent--;
        countTxt.text = string.Format("" + bombCount);
    }

    public void AddRange()
    {
        explosionRadius++;
        rangeTxt.text = string.Format("" + explosionRadius);
        StartCoroutine(PowerDown(1));
    }

    public void LoseRange()
    {
        explosionRadius--;
        rangeTxt.text = string.Format("" + explosionRadius);
    }

    public IEnumerator PowerDown(int type)
    {   
        switch (type)
        {
            case 0:
                yield return new WaitForSeconds(10f);
                LoseBomb();
                break;

            case 1:
                yield return new WaitForSeconds(8f);
                LoseRange();
                break;
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreTxt.text = string.Format("" + score);
    }
}
