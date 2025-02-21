using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableInteractablesList", menuName = "ScriptableObjects/SpawnableInteractables")]
public class SpawnableInteractables : ScriptableObject
{
    [Header("Spawn distance")] 
    [Min(0.1f)]public float minSpawnDistance;
    public float maxSpawnDistance;

    [Space(5)] [Header("Oxygen")] 
    public int spawnOxygenInteractablesOnStartAmount;
    public OxygenItem oxygenInteractable;
    
    [Space(5)] [Header("Star")] 
    public GameObject starInteractable;
    public int scoreToSpawnStar;
    public float timeToSpawnStar;
    public int maxAmountOfStars;
    
    [Space(5)] [Header("Speed boost")]     
    public GameObject speedBoostInteractable;
    public int scoreToSpawnSpeedBoost;
    public float timeToSpawnSpeedBoost;
    public int maxAmountOfSpeedBoosts;
    
    [Space(5)] [Header("Slow boost")]     
    public GameObject slowBoostInteractable;
    public int scoreToSpawnSlowBoost;
    public float timeToSpawnSlowBoost;
    public int maxAmountOfSlowBoosts;
    
    [Space(5)] [Header("Swamp")]     
    public GameObject swampInteractable;
    public int scoreToSpawnSwamp;
    public float timeToSpawnSwamp;
    public int maxAmountOfSwamps;

    [Space(5)] [Header("Dog and Doghouse")] 
    public GameObject dogInteractable;
    public GameObject dogHouseInteractable;
    public int scoreToSpawnDogAndDogHouse;
    public int maxAmountOfDogsAndDogHouses;
}
