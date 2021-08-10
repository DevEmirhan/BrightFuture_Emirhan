using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{

    [SerializeField] private PlayerController player;
    [SerializeField] private int LoadingSceneIndex = 0;

    private void Start()
    {
        UIManager.Instance.RefreshPanel(0);
    }
    public void StartGame()
    {
        player.InitializePlayer();
        CameraManager.Instance.ActivateCamera(1);
        UIManager.Instance.OpenPage(1);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(LoadingSceneIndex);
    }

}
