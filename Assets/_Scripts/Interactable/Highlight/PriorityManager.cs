using UnityEngine;

public class PriorityManager : MonoBehaviour
{
    private PriorityManager _PriorityManager; // used for other scripts to get it
    public static bool _PriorityInteractable;
    public static bool _CanInteract = true; // call from other scripts to say that this should be called
    public static bool _CanInteractDialogue = true; // call from other scripts to say that this should be called
    public static GameObject _CompareGameObject;

    private void OnEnable()
    {
        if (_PriorityManager == null)
        {
            _PriorityManager = GetComponent<PriorityManager>();
        }
        _PriorityInteractable = true;
        _CanInteract = true;
        _CanInteractDialogue = true;
    }


    public virtual bool TriggerEnter(GameObject gameObject) // On Trigger enter call this
    {
        if (_PriorityInteractable)
        {
            _PriorityInteractable = false;

            //AdditionalTriggerEnterImplementation(); should be added here
            return true;
        }
        return false;
    }

    protected virtual void AdditionalTriggerEnterImplementation()
    {
        // Here all new code will come as overriding
    }

    public virtual void TriggerExit(GameObject gameObject) // On trigger exit call this
    {
        if (!_PriorityInteractable)
        {
            AdditionalTriggerExitImplementation();
            _PriorityInteractable = true;
        }
        else
        {
            return;
        }
    }

    protected virtual void AdditionalTriggerExitImplementation()
    {
        // Here all new code will come as overriding
    }

    public virtual void TriggerUse(bool state) // On activate call this
    {
        if (state) // if bool is true use
        {

        }
        else // else set to false
        {

        }
    }

    protected virtual void AdditionalTriggerUseImplementation()
    {
        // Here all new code will come as overriding
    }

}
