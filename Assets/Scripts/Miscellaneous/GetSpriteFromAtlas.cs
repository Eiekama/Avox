using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
public class GetSpriteFromAtlas : MonoBehaviour
{
    [SerializeField] SpriteAtlas _atlas;
    [SerializeField] string spriteName;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = _atlas.GetSprite(spriteName);
    }
}
