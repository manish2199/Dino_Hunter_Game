using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewRaptorsScriptableObjectList")]
public class RaptorsList : ScriptableObject
{
   public WalkableDinosaurScriptableObject[] Raptors;
}
