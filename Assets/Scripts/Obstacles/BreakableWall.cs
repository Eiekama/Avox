using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, IDamageable
{
    public enum HitDirections { All = 0x0, LeftOnly = 0x1, RightOnly = 0x3 }
    static HashSet<int> brokenWalls = new HashSet<int>();

    public int id;
    public HitDirections hitDirections;

    public List<Sprite> sprites;
    SpriteRenderer _spriteRenderer;
    int _state;

    public GameObject gateObject;    

    void Start()
    {
        if (brokenWalls.Contains(id))
        {
            Destroy(gameObject);

            if (gateObject != null)
            {
                // hard-coded: can abstract if needed
                gateObject.transform.position = new Vector3(12, -10f);
            }
        }

        _state = 0;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Damage(Transform source, int dmgTaken)
    {
        if (hitDirections == HitDirections.LeftOnly && source.position.x > transform.position.x
            || hitDirections == HitDirections.RightOnly && source.position.x < transform.position.x)
            return;

        _state += dmgTaken;

        if (_state >= sprites.Count)
        {
            Destroy(gameObject);
            brokenWalls.Add(id);
        } else
        {
            _spriteRenderer.sprite = sprites[_state];
        }
    }
}
