using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=============================================================================Base Class==================================================================//
public abstract class Dinosaurs
{
   // all dinosaurs have health
   // al dinosaur can do damage

   public int Health;

   public int Damage;


   // AudioClips 
   public DinosaurAudioClips[] DinosaurAudios;


}   

//=============================================================================Base Class Types==================================================================//


public class WalkingDinosaurs : Dinosaurs
{ 
    public float Speed;

    public float AngularSpeed;
   
    public float Acceleration;

    public float FieldOfViewAnle;
 
    public float TargetStopppingDistance;

    public float StoppingDistanceFromWayPoint;

    public float ChasingRange;

    public float AttackingRange;

    public int TimeToRespawnAfterDeath;

   [SerializeReference] public WalkableDinosaurView WalkableDinosaurView; 
}



public class Raptor : WalkingDinosaurs
{
   public RaptorsType raptorsType; 

   public SpecialAbility specialAbilities;
  
}

[Serializable]
public class SpecialAbility
{
   public SpecialAbilityType specialAbilityType; 

   public ParticleSystem Effect;
}

public class TRex : WalkingDinosaurs
{
   public TRexType TRexType;   
}

//  BlueRaptor // this raptor is medium fast low damage medium health  not have special ability
//  RedRaptor  // this raptor is medium fast //  higher than medium damage // medium health  // flame thrower special ability
//  OviRaptor  // this raptor is fastest in all raptors // medium damage // medium health //  not have special ability


// RedTrex  // this raptor is slower // high damage( kill in one bite ) // high health //  (can bite while runn)


//=============================================================================Enums==================================================================//


public enum AudioClipType
{
    None,
    IdleAudio,
    SniffAudio,
    EatingAudio,
    WalkingAudio,
    RoaringAudio,
    AttackingAudio,
    SearchSound, 
    FlameThrower,
    WalkingRoar
}
 
[Serializable]
public class DinosaurAudioClips
{
   public AudioClipType AudioClipType;

   public AudioClip[] AudioClip;
}

public enum SpecialAbilityType
{
    None,
    FlameThrower
}



public enum WalkingDinosaurType 
{
    None,
    Raptors,
    TRex
}


public enum  RaptorsType
{
    None,
    BlueRaptor,
    RedRaptor,
    OviRaptor,
}

public enum TRexType 
{
    None,
    RedTrex,
}


//============================================================================================================================================================
//============================================================================================================================================================


public abstract class FlyingDinosaurs : Dinosaurs
{
    
}








