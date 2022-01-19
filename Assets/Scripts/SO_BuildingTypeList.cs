using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
public class SO_BuildingTypeList : ScriptableObject
{
    [FormerlySerializedAs("buildingTypeList")] public List<SO_BuildingType> list;
}
