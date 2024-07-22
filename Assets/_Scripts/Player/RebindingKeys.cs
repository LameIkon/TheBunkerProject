using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TextMeshProUGUI _bindingDisplayText;
    [SerializeField] private GameObject _startRebinding;
    [SerializeField] private GameObject _waitingForInput;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    public void StartRebinding()
    {
        _playerController.PlayerInput.SwitchCurrentActionMap("No Input");

        _startRebinding.SetActive(false);
        _waitingForInput.SetActive(true);   

        _rebindingOperation = _interactAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }
    
    private void RebindComplete()
    {
        int bindingIndex = _interactAction.action.GetBindingIndexForControl(_interactAction.action.controls[0]);
        _bindingDisplayText.text = InputControlPath.ToHumanReadableString(_interactAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        _rebindingOperation.Dispose();

        _startRebinding.SetActive(true);
        _waitingForInput.SetActive(false);

        _playerController.PlayerInput.SwitchCurrentActionMap("Player");
    }
}
