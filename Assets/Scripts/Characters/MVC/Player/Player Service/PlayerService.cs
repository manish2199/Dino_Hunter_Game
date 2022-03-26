using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : GenericSingleton<PlayerService>
{
    [SerializeField] PlayerScriptableObject playerScriptableObject;

    private PlayerController playerController;

    [SerializeField] PlayerView playerView;

    [SerializeField] Transform positionToInstantiate; 

    void Start()
    {
        InstantiatePlayer();
    }


    void InstantiatePlayer()
    {
        PlayerModel playerModel = new PlayerModel(playerScriptableObject);

        playerController = new PlayerController(playerModel,playerView,positionToInstantiate);
    }
}
