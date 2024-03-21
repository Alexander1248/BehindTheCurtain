using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Spell")]
public class Spell : ScriptableObject
{
    public GameObject prefab;
    public int manaCost;
    
    

}