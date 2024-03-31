using System;
using System.Collections;
using Interfaces;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;
using PlayerInput = UnityEngine.InputSystem.PlayerInput;

public class MainMenu : MonoBehaviour, IPlayerJoinListener
{
    [SerializeField] private TextMeshProUGUI playerA, playerB;
    [SerializeField] private Button startGame;

    [SerializeField] private Transform Player1Position, Player2Position;

    private PlayerJoinHandler _gameStarter;

    private void Start()
    {
        EventSystem.current.firstSelectedGameObject = startGame.gameObject;

        var inputManager = PlayerInputManager.instance;
        _gameStarter = inputManager.GetComponent<PlayerJoinHandler>();

        _gameStarter.AddPlayerJoinListener(this);

        startGame.gameObject.SetActive(false);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        var cc = player.GetComponent<CharacterController>();
        cc.enabled = false;
        
        
        player.camera.enabled = false;
        
        
        var playerTransform = player.transform;
        
        switch (player.playerIndex)
        {
            case 0:
                playerA.text = "Player A Joined!";
                playerA.color = Color.green;
                playerTransform.position = Player1Position.position;
                playerTransform.rotation = Player1Position.rotation;
                break;
            case 1:
                playerB.text = "Player B Joined!";
                playerB.color = Color.green;
                playerTransform.position = Player2Position.position;
                playerTransform.rotation = Player2Position.rotation;
                break;
        }

        GameObject button = startGame.gameObject;
        button.SetActive(true);
        EventSystem.current.SetSelectedGameObject(button);
    }
}