using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _playerHUD;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _exitText;

    public enum CurrentState
    {
        _IsPlaying,
        _IsPaused,
        _IsInSettings,
        _IsTryingToExit
    }

    public CurrentState currentState;
    private bool _isPaused;

    private void Awake()
    {
        // Ensure it is set correct
        currentState = CurrentState._IsPlaying; 
        _isPaused = false;
    }

    private void PerformAction(CurrentState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case CurrentState._IsPlaying:
                HandlePlaying();
                break;
            case CurrentState._IsPaused:
                HandlePausing();
                break;
            case CurrentState._IsInSettings:
                HandleSettings();
                break;
            case CurrentState._IsTryingToExit:
                HandleExit();
                break;
        }
    }

    #region Handle Logic
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentState == CurrentState._IsPaused)
            {
                if (_isPaused)
                {
                    PerformAction(CurrentState._IsPlaying);
                }
                else
                {
                    PerformAction(CurrentState._IsPaused);
                }
            }
            else
            {
                PerformAction(CurrentState._IsPaused);
            }
        }
    }

    private void HandlePlaying()
    {
        _pauseMenu.SetActive(false); // Hide pause screen
        _playerHUD.SetActive(true); // Show playerHUD
        Time.timeScale = 1.0f; // Run time again
        _isPaused = false; // Ensure input system knows when is paused or not
    }

    private void HandlePausing() // Other stuff like preventing shooting needs to be stopped also since that method can still be called
    {
        _pauseMenu.SetActive(true); // Show pause
        _pauseScreen.SetActive(true); // Show options
        _playerHUD.SetActive(false); // Hide playerHUD
        _exitText.SetActive(false); // Hide Exit warning
        _settings.SetActive(false); // Hide Settings
        Time.timeScale = 0f; // Stop time
        _isPaused = true; // Ensure input system knows when is paused or not
    }

    private void HandleSettings()
    {
        _settings.SetActive(true); // Show Settings
        _pauseScreen.SetActive(false); // Hide other options
    }

    private void HandleExit()
    {
        _exitText.SetActive(true); // Show exit warning
        _pauseScreen.SetActive(false); // Hide other options
    }
    #endregion
    #region Buttons
    public void ContinueButton() // Continue
    {
        PerformAction(CurrentState._IsPlaying);
    }

    public void NoExitButton() // Say no to exit
    {
        PerformAction(CurrentState._IsPaused);
    }

    public void TryExitButton() // Exit to main Menu
    {
        // Going to main Menu
        PerformAction(CurrentState._IsTryingToExit);
    }

    public void ExitButton()
    {
        Debug.Log("you exited to main menu");
    }


    public void SettingsButton() // Go to settings
    {
        PerformAction(CurrentState._IsInSettings);
    }
    #endregion
}
