using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private PackageManager Packages;
    [SerializeField] private GameObject ExclamationPoint;
    private bool thisHouse = false;
    private float timer = 0f;
    private void Update()
    {
        if (thisHouse)
            timer += Time.deltaTime;
        
        if (timer > 10f)
            thisHouse = false;
        
        ExclamationPoint.SetActive(thisHouse);
    }

    public void Selected()
    {
        thisHouse = !thisHouse;
        timer = 0f;
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
