using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dinosaurs
{
   // all dinosaurs have health
   // al dinosaur can do damage

   public int health;

   public int Damage;


}    


public abstract class WalkingDinosaurs : Dinosaurs
{ 
    public float Speed;

    public float AngularSpeed;
   
    public float Acceleration;

    public float TargetStopppingDistance;

}




public class Raptor : WalkingDinosaurs
{
   public RaptorsType raptorsType; 

   public SpecialAbilityType specialAbilityType;
}

public class TRex : WalkingDinosaurs
{
   public TRexType TRexType;   
}

//  BlueRaptor // this raptor is medium fast low damage medium health  not have special ability
//  RedRaptor  // this raptor is medium fast //  higher than medium damage // medium health  // flame thrower special ability
//  OviRaptor  // this raptor is fastest in all raptors // medium damage // medium health //  not have special ability


// RedTrex  // this raptor is slower // high damage( kill in one bite ) // high health //  (can bite while runn)


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








