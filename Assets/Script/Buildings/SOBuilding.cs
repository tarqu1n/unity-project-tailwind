using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Custom/Building")]
public class SOBuilding : ScriptableObject
{
    [Header("Copy")]
    public new string name;
    public string description;

    [Header("Assets")]
    public Sprite thumbnail;
    public GameObject prefab;
    
    [Header("Costs")]
    public int costWood = 0;
}
