using System.Collections.Generic;
using UnityEngine;

public class PlayerPointerArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    private int _lastDistanceIndex = 0, maxIndex;

    private Vector3 currentPoint;
    
    private List<Vector3> oxygenPoints;
    
    private void Start()
    {
        oxygenPoints = new List<Vector3>();
        
        var oxygenObj = FindObjectsOfType(typeof(OxygenItem)) as OxygenItem[];
        foreach (var oxygen in oxygenObj)
        {
            oxygenPoints.Add(oxygen.gameObject.transform.position);
        }
        
        maxIndex = _lastDistanceIndex + oxygenPoints.Count * 2;
    }

    private void LateUpdate()
    {
        RotateArrow();
    }

    private void RotateArrow()
    {
        float currentDistance = 99999f;
        
        for(int i = _lastDistanceIndex; i < oxygenPoints.Count && i < maxIndex; i++)
        {
            var checkedDistance = (transform.position - oxygenPoints[i]).sqrMagnitude;
            if (checkedDistance < currentDistance)
            {
                currentPoint = oxygenPoints[i];
                currentDistance = checkedDistance;
                
            }
            _lastDistanceIndex = i;
        }
    
        if (_lastDistanceIndex >= oxygenPoints.Count - 1) 
            _lastDistanceIndex = 0;
        
        arrow.transform.LookAt(currentPoint);
    }
}
