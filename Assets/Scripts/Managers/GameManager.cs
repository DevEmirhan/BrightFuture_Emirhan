using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerController player;

    public void StartGame()
    {
        player.InitializePlayer();
    }
    public void ReloadGame()
    {

    }

}
