using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractablesSpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnAreaHolder;
    [SerializeField] private OxygenAndPointsManager oxygenAndPointsManager;
    [SerializeField] private SpawnableInteractables spawnableInteractables;

    private int currentStarsAmount = 0,
        currentSpeedBoostsAmount = 0,
        currentSlowBoostsAmount = 0,
        currentSwampsAmount = 0,
        currentDogAndDogHouseAmount = 0;

    private bool ableToSpawnStars = false, 
        ableToSpawnSpeedBoosts = false, 
        ableToSpawnSlowBoosts = false, 
        ableToSpawnSwamps = false, 
        ableToSpawnDogs = false;
    
    private Transform player;
    private List<BoxCollider> spawnAreas = new List<BoxCollider>();

    public static Action<Transform> AddOxygenToPointerList;

    private void OnEnable()
    {
        OxygenItem.OnOxygenCollected += SpawnNewOxygen;
        //OxygenAndPointsManager.ScoreChanged += ;
    }

    private void OnDisable()
    {
        OxygenItem.OnOxygenCollected -= SpawnNewOxygen;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
        foreach (var boxCollider in spawnAreaHolder.GetComponents<BoxCollider>())
        {
            spawnAreas.Add(boxCollider);
        }
    }

    private void Update()
    {
        
    }

    private void CheckForSpawnConditions(int currentPlayerScore)
    {
        
    }

    private void SpawnNewOxygen()
    {
         var oxg = Instantiate(spawnableInteractables.oxygenInteractable, GetRandomSpawnAreaPosition(), quaternion.identity).transform;
         AddOxygenToPointerList?.Invoke(oxg.transform);
    }

    private Vector3 GetRandomSpawnAreaPosition()
    {
        var selectedArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
        var offset = spawnAreaHolder.transform.position;
        
        float randomPosX = Random.Range(selectedArea.center.x - selectedArea.size.x / 2,
            selectedArea.center.x + selectedArea.size.x / 2);
        float randomPosZ = Random.Range(selectedArea.center.z - selectedArea.size.z / 2,
            selectedArea.center.z + selectedArea.size.z / 2);
        Vector3 areaPosition = new Vector3(offset.x + randomPosX, offset.y + selectedArea.center.y,offset.z + randomPosZ);
        
        return areaPosition;
    }
}
