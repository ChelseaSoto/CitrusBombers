using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public SpriteRenderer center;
    public SpriteRenderer middle;
    public SpriteRenderer end;

    public void SetActive(SpriteRenderer renderer)
    {
        center.enabled = renderer == center;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
