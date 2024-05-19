using System;
using UnityEngine;

public class PlayerDogCarrying : MonoBehaviour
{
    [SerializeField] private GameObject dogCartObject;
    
    public static bool hasDog = false;

    private PlayerMovespeedInteraction playerMoveSpeedInteraction;

    private void Start()
    {
        playerMoveSpeedInteraction = GetComponent<PlayerMovespeedInteraction>();
        dogCartObject.SetActive(false);
    }

    private void OnEnable()
    {
        DogCollectable.DogWasTaken += GetDog;
        DogHouseInteraction.DogWasReleased += ReleaseDog;
    }

    private void Update()
    {
        Debug.Log(hasDog);
    }

    private void OnDisable()
    {
        DogCollectable.DogWasTaken -= GetDog;
        DogHouseInteraction.DogWasReleased -= ReleaseDog;
    }
    
    private void GetDog(float dogSlowdown)
    {
        hasDog = true;
        dogCartObject.SetActive(true);
        
        playerMoveSpeedInteraction.ChangeSpeed(dogSlowdown);
    }

    private void ReleaseDog()
    {
        hasDog = false;
        dogCartObject.SetActive(false);
        
        playerMoveSpeedInteraction.ChangeSpeedToDefault();
    }
}
