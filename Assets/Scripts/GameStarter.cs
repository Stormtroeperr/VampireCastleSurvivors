using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private SceneAsset mainMenu;
    [SerializeField] private SceneAsset level;
    
    public void StartGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadSceneAsync(level.name, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(mainMenu.name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == level.name)
        {
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}