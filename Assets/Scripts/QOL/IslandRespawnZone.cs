using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandRespawnZone : MonoBehaviour
{
    [SerializeField] private Transform islandRespawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.position = islandRespawnPosition.position;
    }
}
