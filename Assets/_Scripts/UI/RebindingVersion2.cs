using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [System.Serializable]
    private class RebindingItem
    {
        public string description;
        public InputActionReference _ActionReference; // The input action
        public GameObject _StartRebindingButton; // The button to start rebinding
        public TextMeshProUGUI _BindingDisplayText; // The button text
        public GameObject _WaitingForInput; // Text indicating waiting for input
        public int _BindingIndex; // The index of the binding to change
    }

    [SerializeField] private PlayerController _playerController; // Reference to the player input
    [SerializeField] private List<RebindingItem> _rebindingItems; // List of rebinding items

    private const string _rebindsKey = "rebinds"; // Key for stored rebinds in PlayerPrefs
    private const string _defaultRebindsKey = "defaultRebinds"; // Key for default rebinds in PlayerPrefs

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    private RebindingItem _currentRebindingItem;

    private void Start()
    {
        SaveDefaultBindings(); // Save default bindings if not already saved
        LoadRebinds();       
    }

    public void StartRebinding(int index) // Called from button
    {
        _currentRebindingItem = _rebindingItems[index];
        _playerController.PlayerInput.SwitchCurrentActionMap("No Input");

        _currentRebindingItem._StartRebindingButton.SetActive(false);
        _currentRebindingItem._WaitingForInput.SetActive(true);

        _rebindingOperation = _currentRebindingItem._ActionReference.action.PerformInteractiveRebinding(_currentRebindingItem._BindingIndex)
            .WithControlsExcluding("<Keyboard>/escape")
            .WithControlsExcluding("<keyboard>/anyKey")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        UpdateBindingText(_currentRebindingItem);

        _rebindingOperation.Dispose();

        _currentRebindingItem._StartRebindingButton.SetActive(true);
        _currentRebindingItem._WaitingForInput.SetActive(false);

        _playerController.PlayerInput.SwitchCurrentActionMap("Player");

        SaveRebinds();
    }

    public void SaveRebinds() 
    {
        string rebinds = _playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(_rebindsKey, rebinds);
        PlayerPrefs.Save();
        Debug.Log("Saved rebinds");
    }

    private void LoadRebinds()
    {
        string rebinds = PlayerPrefs.GetString(_rebindsKey, string.Empty);

        if (string.IsNullOrEmpty(rebinds)) // If no stored rebinds
        {
            foreach (var rebindingItem in _rebindingItems)
            {
                UpdateBindingText(rebindingItem); // Display default settings
            }
            return; // Return if no rebinds found
        }

        // Else
        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds); // Load stored rebinds from JSON
        foreach (var rebindingItem in _rebindingItems)
        {
            UpdateBindingText(rebindingItem); // Display rebinds
        }
    }

    public void LoadDefault() // Button to load default settings
    {
        string defaultRebinds = PlayerPrefs.GetString(_defaultRebindsKey, string.Empty);

        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(defaultRebinds); // Load default bindings
        foreach (var rebindingItem in _rebindingItems)
        {
            UpdateBindingText(rebindingItem); // Display default settings
        }
        SaveRebinds(); // Save default settings
        Debug.Log("Loaded default rebinds");
    }

    private void SaveDefaultBindings()
    {
        if (!PlayerPrefs.HasKey(_defaultRebindsKey)) // Check if default bindings are already saved
        {
            string defaultRebinds = _playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(_defaultRebindsKey, defaultRebinds);
            PlayerPrefs.Save();
            Debug.Log("Saved default rebinds");
        }
    }

    private void UpdateBindingText(RebindingItem rebindingItem)
    {
        int bindingIndex = rebindingItem._BindingIndex;
        rebindingItem._BindingDisplayText.text = InputControlPath.ToHumanReadableString(
            rebindingItem._ActionReference.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}