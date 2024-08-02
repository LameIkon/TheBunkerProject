using System.Collections.Generic;
using UnityEngine;

public class HighlightEmission : MonoBehaviour
{
    [SerializeField] private List<Transform> _objectsToHighlight = new List<Transform>();


    [SerializeField] private bool _haveUniq1;
    [SerializeField] private bool _haveUniq2;

    [Header("Original")]
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _uniq1;
    [SerializeField] private Sprite _uniq2;

    [Header("highlighted")]
    [SerializeField] private Sprite _defaultHighlight;
    [SerializeField] private Sprite _uniq1Highlight;
    [SerializeField] private Sprite _uniq2Highlight;


    private void Awake()
    {
        foreach (Transform @object in transform)
        {
            _objectsToHighlight.Add(@object);
        }
    }

    private void Start()
    {
        Highlight();
    }

    public void Highlight() // Change sprites to highlighted sprite
    {
        if (_objectsToHighlight.Count > 0)
        {
            // Change the sprite of the first object to the unique 1 sprite
            SpriteRenderer firstUniqueSprite = _objectsToHighlight[0].GetComponent<SpriteRenderer>();
            if (firstUniqueSprite != null && _haveUniq1)
            {
                firstUniqueSprite.sprite = _uniq1Highlight;
            }
            else if (firstUniqueSprite != null && !_haveUniq1)
            {
                firstUniqueSprite.sprite = _defaultHighlight;
            }

            // Change the sprite of the last object to the unique 2 sprite
            SpriteRenderer lastUniqueSprite = _objectsToHighlight[_objectsToHighlight.Count - 1].GetComponent<SpriteRenderer>();
            if (lastUniqueSprite != null && _haveUniq2)
            {
                lastUniqueSprite.sprite = _uniq2Highlight;
            }
            else if (firstUniqueSprite != null && !_haveUniq2)
            {
                lastUniqueSprite.sprite = _uniq2Highlight;
            }

            // Change the sprites of the middle objects to the default sprite
            for (int i = 1; i < _objectsToHighlight.Count - 1; i++)
            {
                SpriteRenderer spriteRenderer = _objectsToHighlight[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = _defaultHighlight;
                }
            }
        }
    }

    public void ReturnToOriginal() // Return to default sprites before change
    {
        if (_objectsToHighlight.Count > 0)
        {
            // Change the sprite of the first object to the unique 1 sprite
            SpriteRenderer firstUniqueSprite = _objectsToHighlight[0].GetComponent<SpriteRenderer>();
            if (firstUniqueSprite != null && _haveUniq1)
            {
                firstUniqueSprite.sprite = _uniq1;
            }
            else if (firstUniqueSprite != null && !_haveUniq1)
            {
                firstUniqueSprite.sprite = _default;
            }

            // Change the sprite of the last object to the unique 2 sprite
            SpriteRenderer lastUniqueSprite = _objectsToHighlight[_objectsToHighlight.Count - 1].GetComponent<SpriteRenderer>();
            if (lastUniqueSprite != null && _haveUniq2)
            {
                lastUniqueSprite.sprite = _uniq2;
            }
            else if (firstUniqueSprite != null && !_haveUniq2)
            {
                lastUniqueSprite.sprite = _uniq2;
            }

            // Change the sprites of the middle objects to the default sprite
            for (int i = 1; i < _objectsToHighlight.Count - 1; i++)
            {
                SpriteRenderer spriteRenderer = _objectsToHighlight[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = _default;
                }
            }
        }
    }
}
