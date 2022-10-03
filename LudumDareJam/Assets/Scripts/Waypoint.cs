using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform currentPoint;

    public GameObject arrow;

    private void LateUpdate()
    {
        RotateArrow();
    }

    public void SetNewPoint(Transform housePos)
    {
        currentPoint = housePos;
    }

    private void RotateArrow()
    {
        arrow.transform.LookAt(currentPoint);
    }
}
