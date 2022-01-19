using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "ScriptableObjects/BuildingType")]
public class SO_BuildingType :ScriptableObject
{
    public string nameString;
    public Transform prefab;
}
