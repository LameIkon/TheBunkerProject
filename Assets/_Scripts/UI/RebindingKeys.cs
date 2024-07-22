using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [System.Serializable]
    private class RebindingItem
    {
        public InputActionReference _ActionReference; // The input key
        public GameObject _StartRebindingButton; // The button to be selected for rebinding
        public TextMeshProUGUI _BindingDisplayText; // The button text
        public GameObject _WaitingForInput; // Text saying waiting for input
    }

    [SerializeField] private PlayerController _playerController; // Get reference to the player input
    //[SerializeField] private GameObject _waitingForInput; // Text saying waiting for input
    [SerializeField] private List<RebindingItem> _rebindingItems; // List of rebinding items


    private const string _rebindsKey = "rebinds"; // Rebinds stored as string to json
    private const string _defaultRebindsKey = "defaultRebinds"; // Default rebinds stored as string to json

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    private RebindingItem _currentRebindingItem;

    private void Start()
    {
        SaveDefaultBindings(); // Called the first time to remember default settings. 
        LoadRebinds();       
    }

    public void StartRebinding(int index) // Called from button
    {
        _currentRebindingItem = _rebindingItems[index];
        _playerController.PlayerInput.SwitchCurrentActionMap("No Input");

        _currentRebindingItem._StartRebindingButton.SetActive(false);
        _currentRebindingItem._WaitingForInput.SetActive(true);



        _rebindingOperation = _currentRebindingItem._ActionReference.action.PerformInteractiveRebinding()
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
        Debug.Log("saved");
    }

    private void LoadRebinds()
    {
        string rebinds = PlayerPrefs.GetString(_rebindsKey, string.Empty);

        if (string.IsNullOrEmpty(rebinds)) // If i have stored rebinds
        {
            Debug.Log("no changing");
            foreach (var rebindingKey in _rebindingItems)
            {
                UpdateBindingText(rebindingKey); // Display the settings
            }
            return; // Return nothing
        }

        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds); // Get the stored rebinds from json
        foreach (var rebindingKey in _rebindingItems)
        {
            UpdateBindingText(rebindingKey); // Display the settings
        }
        Debug.Log("loaded rebinds");
    }

    public void LoadDefault() // Button. Set to default by deleteing preferences
    {
        string defaultRebinds = PlayerPrefs.GetString(_defaultRebindsKey, string.Empty);

        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(defaultRebinds); // Load default bindings
        foreach (var rebindingKey in _rebindingItems)
        {
            UpdateBindingText(rebindingKey); // Display the settings
        }
        SaveRebinds(); // Save default settings
        Debug.Log("loaded default rebinds");
    }

    private void SaveDefaultBindings()
    {
        if (!PlayerPrefs.HasKey(_defaultRebindsKey)) // Check if default bindings are already saved
        {
            string defaultRebinds = _playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(_defaultRebindsKey, defaultRebinds);
            PlayerPrefs.Save();
            Debug.Log("saved default rebinds");
        }
    }

    private void UpdateBindingText(RebindingItem rebindingItem)
    {
        int bindingIndex = rebindingItem._ActionReference.action.GetBindingIndexForControl(rebindingItem._ActionReference.action.controls[0]);
        rebindingItem._BindingDisplayText.text = InputControlPath.ToHumanReadableString(rebindingItem._ActionReference.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
