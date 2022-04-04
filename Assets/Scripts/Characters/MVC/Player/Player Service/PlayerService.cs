using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService :  GenericSingleton<PlayerService>
{
    [SerializeField] PlayerScriptableObject playerScriptableObject;

    private PlayerController playerController;

    [SerializeField] PlayerView playerView;

    [SerializeField] Transform positionToInstantiate; 


    protected override void Awake()
    { 
        base.Awake();
        InstantiatePlayer();
    }
 
 
 
    void InstantiatePlayer()
    {
        PlayerModel playerModel = new PlayerModel(playerScriptableObject);

        playerController = new PlayerController(playerModel,playerView,positionToInstantiate);
    }  

    public void SelectInitialWeapon(Transform fps)
    {
        WeaponService.instance.SelectInitialWeapon(fps); 
    }

    public void SelectWeapons(Transform fpsTransform , int weaponIndex)
    {
        WeaponService.Instance.SelectWeapon(fpsTransform,weaponIndex);
    }

    


}
