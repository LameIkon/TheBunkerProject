using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerShadowCaster : MonoBehaviour
{
    public Transform player;            // Reference to the player object
    public GameObject[] shadowCasters;  // List of objects with Shadow Caster 2D

    private Vector3 playerPosition;

    void Start()
    {
        // Initial player position
        playerPosition = player.position;
    }

    void Update()
    {
        // Update player position
        playerPosition = player.position;

        // Update shadow caster origins to player's position
        foreach (GameObject caster in shadowCasters)
        {
            // Calculate direction from player to shadow caster
            Vector3 directionToCaster = caster.transform.position - playerPosition;

            // Set shadow caster position to mimic shadow casting from player
            caster.GetComponent<ShadowCaster2D>().transform.position = playerPosition + directionToCaster;
        }
    }
}