using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/NewTRexScriptableObjectList")]
public class TRexList : ScriptableObject
{
   public WalkableDinosaurScriptableObject[] TRexs;
}
