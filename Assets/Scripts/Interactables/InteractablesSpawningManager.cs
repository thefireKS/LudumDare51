using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractablesSpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnAreaHolder;
    [SerializeField] private SpawnableInteractables spawnableInteractables;
    [SerializeField] private OxygenAndPointsManager oxygenAndPointsManager;
    [SerializeField] private InteractablesSpawningPenaltyManager interactablesSpawningPenaltyManager;

    private bool ableToSpawnStars, ableToSpawnSpeedBoosts, ableToSpawnSlowBoosts, ableToSpawnSwamps, ableToSpawnDogs;

    private static List<Transform> starsTransforms, speedBoostsTransforms, slowBoostsTransforms, swampsTransforms;

    private bool ableToSpawnAll =>
        ableToSpawnStars & ableToSpawnSpeedBoosts & ableToSpawnSlowBoosts & ableToSpawnSwamps & ableToSpawnDogs;

    private float minDistance, maxDistance;

    private Transform player;
    private List<BoxCollider> spawnAreas = new List<BoxCollider>();

    public static Action<Transform> AddOxygenToPointerList;

    private void OnEnable()
    {
        OxygenItem.OnOxygenCollected += SpawnNewOxygen;
        OxygenAndPointsManager.ScoreChanged += CheckForSpawnConditions;
        //DogHouseInteraction.DogWasReleased += SpawnNewDogs;
        RescueTheDogEvent.spawnEventDog += SpawnNewDogs;

        starsTransforms = new List<Transform>();
        speedBoostsTransforms = new List<Transform>();
        slowBoostsTransforms = new List<Transform>();
        swampsTransforms = new List<Transform>();
    }

    private void OnDisable()
    {
        OxygenItem.OnOxygenCollected -= SpawnNewOxygen;
        OxygenAndPointsManager.ScoreChanged -= CheckForSpawnConditions;
        //DogHouseInteraction.DogWasReleased -= SpawnNewDogs;
        RescueTheDogEvent.spawnEventDog -= SpawnNewDogs;
    }

    private void Start()
    {
        minDistance = spawnableInteractables.minSpawnDistance;
        maxDistance = spawnableInteractables.maxSpawnDistance;
        
        player = GameObject.FindWithTag("Player").transform;
        
        foreach (var boxCollider in spawnAreaHolder.GetComponents<BoxCollider>())
        {
            spawnAreas.Add(boxCollider);
        }

        for (var i = 0; i < spawnableInteractables.spawnOxygenInteractablesOnStartAmount; i++)
        {
            SpawnNewOxygen();
        }
    }

    private void CheckForSpawnConditions(int currentPlayerScore)
    {
        if(ableToSpawnAll) return;

        if (spawnableInteractables.scoreToSpawnStar > 0 && !ableToSpawnStars && currentPlayerScore > spawnableInteractables.scoreToSpawnStar)
        {
            ableToSpawnStars = true;
            StartCoroutine(SpawnNewItem(spawnableInteractables.starInteractable, spawnableInteractables.timeToSpawnStar,
                spawnableInteractables.scoreToSpawnStar, starsTransforms));
        }
        if (spawnableInteractables.scoreToSpawnSpeedBoost > 0 && !ableToSpawnSpeedBoosts && currentPlayerScore > spawnableInteractables.scoreToSpawnSpeedBoost)
        {
            ableToSpawnSpeedBoosts = true;
            StartCoroutine(SpawnNewItem(spawnableInteractables.speedBoostInteractable, spawnableInteractables.timeToSpawnSpeedBoost,
                 spawnableInteractables.maxAmountOfSpeedBoosts, speedBoostsTransforms));
        }
        if (spawnableInteractables.scoreToSpawnSlowBoost > 0 && !ableToSpawnSlowBoosts && currentPlayerScore > spawnableInteractables.scoreToSpawnSlowBoost)
        {
            ableToSpawnSlowBoosts = true;
            StartCoroutine(SpawnNewItem(spawnableInteractables.slowBoostInteractable, spawnableInteractables.timeToSpawnSlowBoost,
                 spawnableInteractables.maxAmountOfSlowBoosts, slowBoostsTransforms));
        }
        if (spawnableInteractables.scoreToSpawnSwamp > 0 && !ableToSpawnSwamps && currentPlayerScore > spawnableInteractables.scoreToSpawnSwamp)
        {
            ableToSpawnSwamps = true;
            StartCoroutine(SpawnNewItem(spawnableInteractables.swampInteractable, spawnableInteractables.timeToSpawnSwamp, 
                spawnableInteractables.maxAmountOfSwamps, swampsTransforms));
        }
        if (spawnableInteractables.scoreToSpawnDogAndDogHouse > 0 && !ableToSpawnDogs && currentPlayerScore > spawnableInteractables.scoreToSpawnDogAndDogHouse)
        {
            ableToSpawnDogs = true;
            SpawnNewDogs();
        }
    }

    private void SpawnNewOxygen()
    {
         var oxg = Instantiate(spawnableInteractables.oxygenInteractable, GetRandomSpawnAreaPosition(), quaternion.identity);
         interactablesSpawningPenaltyManager.AddOxygenPenalty(oxg);
         AddOxygenToPointerList?.Invoke(oxg.transform);
    }

    private void SpawnNewDogs()
    {
        Instantiate(spawnableInteractables.dogInteractable, GetRandomSpawnAreaPosition(), quaternion.identity);
        Instantiate(spawnableInteractables.dogHouseInteractable, GetRandomSpawnAreaPosition(), quaternion.identity);
    }

    private IEnumerator SpawnNewItem(GameObject gameObjectToInstantiate, float timeToSpawn, int maxAmountCounter, List<Transform> currentGameObjectsTransforms)
    {
        currentGameObjectsTransforms = currentGameObjectsTransforms.Where(t => t != null).ToList();
        
        var obj = Instantiate(gameObjectToInstantiate, GetRandomSpawnAreaPosition(), quaternion.identity).transform;
        currentGameObjectsTransforms.Add(obj);
        
        Debug.Log(currentGameObjectsTransforms.Count);
        if (currentGameObjectsTransforms.Count > maxAmountCounter)
            RemoveMostDistancedObject(ref currentGameObjectsTransforms);

        yield return new WaitForSeconds(timeToSpawn);
        StartCoroutine(SpawnNewItem( gameObjectToInstantiate,  timeToSpawn,  maxAmountCounter, currentGameObjectsTransforms));
    }

    private Vector3 GetRandomSpawnAreaPosition()
    {
        var offset = spawnAreaHolder.transform.position;
        var possibleAreas = new List<BoxCollider>();
        
        foreach (var area in spawnAreas)
        {
            var dist = (area.center + offset - player.position).sqrMagnitude;
            if(dist < maxDistance + interactablesSpawningPenaltyManager._currentDistancePenalty && dist > minDistance + interactablesSpawningPenaltyManager._currentDistancePenalty)
                possibleAreas.Add(area);
        }

        var selectedArea = possibleAreas[Random.Range(0, possibleAreas.Count)];
        
        float randomPosX = Random.Range(selectedArea.center.x - selectedArea.size.x / 2,
            selectedArea.center.x + selectedArea.size.x / 2);
        float randomPosZ = Random.Range(selectedArea.center.z - selectedArea.size.z / 2,
            selectedArea.center.z + selectedArea.size.z / 2);
        Vector3 areaPosition =
            new Vector3(offset.x + randomPosX, offset.y + selectedArea.center.y, offset.z + randomPosZ);

        return areaPosition;
    }

    private void RemoveMostDistancedObject(ref List<Transform> list)
    {
        float maxDist = 0f;
        Transform objectToRemove = null;
        foreach (var t in list)
        {
            var dist = (player.position - t.position).sqrMagnitude;
            if (dist > maxDist)
            {
                maxDist = dist;
                objectToRemove = t;
            }
        }

        Destroy(objectToRemove.gameObject);
        list.Remove(objectToRemove);
    }
}
