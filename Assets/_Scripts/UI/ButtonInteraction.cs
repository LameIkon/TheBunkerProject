using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonInteraction : MonoBehaviour
{
    public Button[] buttons; // Needs correct lineup
    private int currentIndex = -1; // Needs to show buttons as deselected 
    private bool startedSelection; // Used to say if a button is selected or not


    private void Awake()
    {
        currentIndex = -1;
        startedSelection = false;
    }

    private void OnEnable()
    {
        currentIndex = -1;
        startedSelection = false;
    }

    private void OnDisable()
    {
        if (EventSystem.current != null) // Ensure no null reference when you exit the scene 
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Checks if any key is pressed at the start. Used to start selection at 0
        if (!startedSelection)
        {
            FirstInteraction();
        }
        else if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectTheDeselectedButton();
        }
        else
        {
            InteractionWithButtons();
        }

        // Whenever you move your mouse too much it will deselect buttons
        DeselectOnMouseMove();
    }

    void FirstInteraction()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)
           || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            startedSelection = true;
            currentIndex = 0;
            SelectButton(0);  // Select the first button
        }
    }

    void InteractionWithButtons()
    {
        // Key input for button selection
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SelectButton(-1);  // Move selection up
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SelectButton(1);   // Move selection down
        }

        // Key input for button activation
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ActivateSelectedButton();
        }
    }

    void SelectTheDeselectedButton()
    {
        // Key input for button selection
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // Select the Same button again.
            buttons[currentIndex].Select();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            // Select the Same button again.
            buttons[currentIndex].Select();
        }

    }

    void SelectButton(int direction)
    {
        // Move the selection index in the given direction
        currentIndex = (currentIndex + direction + buttons.Length) % buttons.Length;

        // Select the new button.
        buttons[currentIndex].Select();

    }

    void ActivateSelectedButton()
    {
        // click the selected button
        if (gameObject.activeSelf == true)
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    float _totalMouseMovement = 0f;
    float _maxMouseMovementTreshold = 5f;
    void DeselectOnMouseMove()
    {
        _totalMouseMovement += Input.GetAxis("Mouse X");
        _totalMouseMovement += Input.GetAxis("Mouse Y");

        if (Mathf.Abs(_totalMouseMovement) >= _maxMouseMovementTreshold)
        {
            if (gameObject.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            _totalMouseMovement = 0f;
        }
    }
}
