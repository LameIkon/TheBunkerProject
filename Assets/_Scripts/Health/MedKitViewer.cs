using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

sealed class MedKitViewer : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private MedKitCounter _medKitCounter;
    [SerializeField] private uint _numberOfMedKits;
    private bool _fullMedKitInventory;
    private bool _emptyMedKitInventory;

    void Start()
    {
        _medKitCounter.SetMedKitCountNew(_numberOfMedKits);
    }

    void Update()
    {
        FullOrEmptyChecker();
        

        /*
         *      The if-statements with an Input.GetKey() method are placeholder logic to
         *      demonstrate the functions that picks up and uses Med Kits to heal the player.
         *
         *      MUST BE REMOVED OR REPLACED AT A LATER TIME!!
         */

        #region REMOVE THESE IF-STATEMENTS ONCE A PERMANENT SOLUTION IS PUT IN PLACE.

        if (Input.GetKeyDown(KeyCode.O))
        {
            PickUpMedKit();
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            UseMedKit();
        }

        #endregion
    }

    private void UseMedKit()
    {
        if (_playerHealth._IsDead)
        {
            return;
        }

        if (_playerHealth._IsAtMaxHealth)
        {
            print("Your health is already at maximum.");
            return;
        }

        if (_emptyMedKitInventory)
        {
            print("You have no Med Kits to use.");
            return;
        }

        _numberOfMedKits -= 1;
        print("You have successfully gained full health.");
        _playerHealth.GainHealth();
        UpdateMedKitBar();
    }

    /*
     *      The PickUpMedKit method should be called when made contact with a Med Kit.
     *      Remember to not let the Med Kit despawn if the Med Kit inventory is full.
     *      It is called temporarily in Update() in MedKitViewer.cs as a placeholder.
     *
     *      REMOVE THIS COMMENT ONCE A PERMANENT SOLUTION IS PUT IN PLACE.
     */

    private void PickUpMedKit()
    {
        if (_playerHealth._IsDead)
        {
            return;
        }

        if (_fullMedKitInventory)
        {
            print("Your Med Kit inventory is already full.");
            return;
        }

        _numberOfMedKits += 1;
        print("A Med Kit has been picked up.");
        UpdateMedKitBar();
    }

    private void FullOrEmptyChecker()
    {
        switch (_numberOfMedKits)
        {
            case 0:
                _fullMedKitInventory = false;
                _emptyMedKitInventory = true;
                break;

            case 3:
                _fullMedKitInventory = true;
                _emptyMedKitInventory = false;
                break;

            default:
                _fullMedKitInventory = false;
                _emptyMedKitInventory = false;
                break;
        }
    }

    private void UpdateMedKitBar()
    {
        _medKitCounter.SetMedKitCountNew(_numberOfMedKits);
    }
}