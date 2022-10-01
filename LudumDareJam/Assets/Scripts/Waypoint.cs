using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject[] houseWaypoints;
    public Transform currentPoint;

    public GameObject arrow;


    private IEnumerator Start()
    {
        GetAllWaypoints();
        yield return new WaitForSeconds(.5f);
        SetNewPoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetNewPoint();
        }
    }

    private void LateUpdate()
    {
        RotateArrow();
    }

    public void SetNewPoint()
    {
        int randomPoint = Random.Range(0, houseWaypoints.Length);
        currentPoint = houseWaypoints[randomPoint].transform;
    }

    public void RotateArrow()
    {
        arrow.transform.LookAt(currentPoint);
    }

    public void GetAllWaypoints()
    {
        houseWaypoints = GameObject.FindGameObjectsWithTag("Post Box");
    }

}
