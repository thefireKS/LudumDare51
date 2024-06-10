using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableInteractablesList", menuName = "ScriptableObjects/SpawnableInteractables")]
public class SpawnableInteractables : ScriptableObject
{
    [Header("Oxygen")] [SerializeField]
    public GameObject oxygenInteractable;
    
    [Space(5)] [Header("Star")] [SerializeField]
    public GameObject starInteractable;
    [SerializeField] public int scoreToSpawnStar;
    [SerializeField] public float timeToSpawnStar;
    [SerializeField] public int maxAmountOfStars;
    
    [Space(5)] [Header("Speed boost")] [SerializeField]
    public GameObject speedBoostInteractable;
    [SerializeField] public int scoreToSpawnSpeedBoost;
    [SerializeField] public float timeToSpawnSpeedBoost;
    [SerializeField] public int maxAmountOfSpeedBoosts;
    
    [Space(5)] [Header("Slow boost")] [SerializeField]
    public GameObject slowBoostInteractable;
    [SerializeField] public int scoreToSpawnSlowBoost;
    [SerializeField] public float timeToSpawnSlowBoost;
    [SerializeField] public int maxAmountOfSlowBoosts;
    
    [Space(5)] [Header("Swamp")] [SerializeField]
    public GameObject swampInteractable;
    [SerializeField] public int scoreToSpawnwamp;
    [SerializeField] public float timeToSpawnwamp;
    [SerializeField] public int maxAmountOfSwamps;

    [Space(5)] [Header("Dog and Doghouse")] 
    [SerializeField] public GameObject dogInteractable;
    [SerializeField] public GameObject dogHouseInteractable;
    [SerializeField] public int scoreToSpawnDogAndDogHouse;
    [SerializeField] public int maxAmountOfDogsAndDogHouses;
}
