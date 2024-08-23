using UnityEngine;


public class Highlight : MonoBehaviour
{
    [Header("Highlight Components")]
    public HighlightEmission _HighlightEmission; // Highlight Interaction


    public bool _Interact;

    // Start is called before the first frame update
    void Start()
    {
        _HighlightEmission = GetComponentInChildren<HighlightEmission>(); // Used by the triggerscripts components in its children
    }

    public void SetInteract(bool interact) // Used for triggers. Tells if you can or cannot interact 
    {
        _Interact = interact;
    }

}
