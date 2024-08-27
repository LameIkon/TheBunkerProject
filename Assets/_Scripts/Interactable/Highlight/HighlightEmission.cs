using System.Collections.Generic;
using UnityEngine;

public class HighlightEmission : MonoBehaviour
{
    [Header("Hidden Sprites")]
    [SerializeField] private bool _allowActivateHiddenObjects; // If sprites just arent active
    [SerializeField] private List<GameObject> _objectsToHideAndShow = new List<GameObject>(); 

    [Header("Change between Emission")]
    [SerializeField] private bool _allowChangeMaterial;
    [SerializeField] private Material _redEmission;
    [SerializeField] private Material _greenEmission;
    [SerializeField] private List<Transform> _objectsToChangeEmission = new List<Transform>();


    public void Highlight() // Change sprites to highlighted sprite
    {
        if (_allowActivateHiddenObjects)
        {
            ActivateHiddenObjects();
        }

        if (_allowChangeMaterial)
        {
            ChangeGreenMaterial();
        }
    }

    private void ActivateHiddenObjects()
    {
        if (_objectsToHideAndShow.Count > 0)
        {
            foreach (GameObject @object in _objectsToHideAndShow)
            {
                @object.SetActive(true);
            }
        }
    }

    private void DeactivateHiddenSprites()
    {
        if (_objectsToHideAndShow.Count > 0)
        {
            foreach (GameObject @object in _objectsToHideAndShow)
            {
                @object.SetActive(false);
            }
        }
    }

    private void ChangeRedMaterial()
    {
        if (_objectsToChangeEmission.Count > 0)
        {
            for (int i = 0; i < _objectsToChangeEmission.Count; i++)
            {
                SpriteRenderer spriteRenderer = _objectsToChangeEmission[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.material = _redEmission;
                }
            }
        }
    }

    private void ChangeGreenMaterial()
    {
        if (_objectsToChangeEmission.Count > 0)
        {
            for (int i = 0; i < _objectsToChangeEmission.Count; i++)
            {
                SpriteRenderer spriteRenderer = _objectsToChangeEmission[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.material = _greenEmission;
                }
            }
        }
    }

    public void ReturnToOriginal() // Return to original sprites before change
    {
        if (_allowActivateHiddenObjects)
        {
            DeactivateHiddenSprites();
        }

        if (_allowChangeMaterial)
        {
            ChangeRedMaterial();
        }
    }
}
