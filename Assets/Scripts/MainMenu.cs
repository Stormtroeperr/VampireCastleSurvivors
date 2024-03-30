using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPlayerJoinListener
{
    [SerializeField] private TextMeshProUGUI playerA, playerB;
    [SerializeField] private Button startGame;

    private bool _playerAReady, _playerBReady;
    private PlayerJoinHandler _gameStarter;

    private void Start()
    {
        EventSystem.current.firstSelectedGameObject = startGame.gameObject;

        var inputManager = PlayerInputManager.instance;
        _gameStarter = inputManager.GetComponent<PlayerJoinHandler>();

        _gameStarter.AddPlayerJoinListener(this);

        startGame.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _gameStarter.RemovePlayerJoinListener(this);
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        if (player.playerIndex == 0)
        {
            playerA.text = "Player A Joined!";
            playerA.color = Color.green;
            _playerAReady = true;
        }
        else if (player.playerIndex == 1)
        {
            playerB.text = "Player B Joined!";
            playerB.color = Color.green;
            _playerBReady = true;
        }

        if (!_playerAReady || !_playerBReady) return;

        GameObject button = startGame.gameObject;
        button.SetActive(true);
        EventSystem.current.SetSelectedGameObject(button);
    }
}