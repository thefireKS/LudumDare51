using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject[] houseWaypoints;
    public Transform currentPoint;

    public GameObject arrow;

    private void LateUpdate()
    {
        RotateArrow();
    }

    public void SetNewPoint(Transform housePos)
    {
        int randomPoint = Random.Range(0, houseWaypoints.Length);
        currentPoint = housePos;
    }

    private void RotateArrow()
    {
        arrow.transform.LookAt(currentPoint);
    }
}
