using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private PackageManager Packages;
    [SerializeField] private GameObject ExclamationPoint;
    private bool thisHouse = false;

    private void Update()
    {
        if(thisHouse)
            ExclamationPoint.SetActive(true);
        else
            ExclamationPoint.SetActive(false);    
    }

    public void Selected()
    {
        thisHouse = true;
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (collided.CompareTag("Player") && thisHouse)
        {
            Packages.DeliverySucceed();
            thisHouse = false;
        }
    }
}
