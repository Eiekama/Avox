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

    public virtual void Damage(Collider2D source, int dmgTaken)
    {
        if (hitDirections == HitDirections.LeftOnly && source.transform.position.x > transform.position.x
            || hitDirections == HitDirections.RightOnly && source.transform.position.x < transform.position.x)
            return;

        _state += dmgTaken;

        if (_state >= sprites.Count)
        {
            Destroy(gameObject);
            brokenWalls.Add(id);
        } else
        {
            _spriteRenderer.sprite = sprites[_state];
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        for (int i = 0; i < 6; i++)
        {
            (Vector3, Vector3) positions = (_spriteRenderer.transform.localPosition, new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f)));
            if (i == 5)
                positions.Item2 = Vector3.zero;

            for (float t = 0; t < 0.04f; t += Time.deltaTime)
            {
                _spriteRenderer.transform.localPosition = Vector3.Lerp(positions.Item1, positions.Item2, t * (1 / 0.04f));
                yield return new WaitForEndOfFrame();
            }

            _spriteRenderer.transform.localPosition = positions.Item2;
        }
    }
}
