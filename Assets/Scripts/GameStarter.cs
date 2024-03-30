using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    private void LinkHealthBarWithPlayer(PlayerInput player, GameObject area)
    {
        var canvasInstance = Instantiate(playerWorldCanvas, Vector3.zero, Quaternion.identity);
        
        // Assign the player's camera to the player's canvas so we can see the canvas
        var parentHealthBar = canvasInstance.GetComponentInChildren<Image>();

        //Give the player a reference to the image
        var playerHealth = player.GetComponentInChildren<PlayerHealth>();
        playerHealth.SetHealthBar(parentHealthBar.gameObject);
        
        //Set the camera to the right canvas
        canvasInstance.GetComponent<Canvas>().worldCamera = player.camera;
    }
}