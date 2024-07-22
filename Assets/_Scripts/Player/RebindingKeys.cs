using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactAction; // The input key 
    [SerializeField] private PlayerController _playerController; // Get reference to the player input
    [SerializeField] private GameObject _startRebinding; // The button to be selected for rebinding
    [SerializeField] private TextMeshProUGUI _bindingDisplayText; // The button text
    [SerializeField] private GameObject _waitingForInput; // Text saying waiting for input

    private const string _rebindsKey = "rebinds"; // Rebinds stored as string to json
    private const string _defaultRebindsKey = "defaultRebinds"; // Default rebinds stored as string to json

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private void Start()
    {
        SaveDefaultBindings(); // Called the first time to remember default settings. 
        LoadRebinds();       
    }

    public void StartRebinding() // Called from button
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
        UpdateBindingText();

        _rebindingOperation.Dispose();

        _startRebinding.SetActive(true);
        _waitingForInput.SetActive(false);

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

        if (string.IsNullOrEmpty(rebinds)) // If dont have stored rebinds
        {
            Debug.Log("no rebinds");
            return; // Return nothing
        }

        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds); // Get the stored rebinds from json
        UpdateBindingText(); // Display the settings
        Debug.Log("loaded rebinds");
    }

    public void LoadDefault() // Button. Set to default by deleteing preferences
    {
        string defaultRebinds = PlayerPrefs.GetString(_defaultRebindsKey, string.Empty);

        _playerController.PlayerInput.actions.LoadBindingOverridesFromJson(defaultRebinds); // Load default bindings
        UpdateBindingText(); // Display the settings
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

    private void UpdateBindingText()
    {
        int bindingIndex = _interactAction.action.GetBindingIndexForControl(_interactAction.action.controls[0]);
        _bindingDisplayText.text = InputControlPath.ToHumanReadableString(_interactAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
