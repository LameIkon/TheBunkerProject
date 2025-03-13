using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string PlayerID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);  // Ensure the manager persists across scenes

        GeneratePlayerID(); // Assign a unique ID to the player
    }


    private void GeneratePlayerID()
    {
        PlayerID = gameObject.GetInstanceID().ToString(); // Assigns a unique ID to the player

    }




}
