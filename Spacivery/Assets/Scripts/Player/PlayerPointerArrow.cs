using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPointerArrow : MonoBehaviour
{
    [SerializeField] private Transform arrow;

    private int _lastDistanceIndex = 0, maxIndex;

    private Vector3 currentPoint;
    
    private List<Transform> oxygenPoints;
    
    private void Start()
    {
        oxygenPoints = new List<Transform>();
        
        var oxygenObj = FindObjectsOfType(typeof(OxygenItem)) as OxygenItem[];
        foreach (var oxygen in oxygenObj)
        {
            oxygenPoints.Add(oxygen.gameObject.transform);
        }
        
        maxIndex = _lastDistanceIndex + oxygenPoints.Count * 2;
    }

    private void LateUpdate()
    {
        RotateArrow();
    }

    private void UpdateList()
    {
        oxygenPoints = oxygenPoints.Where(o => o != null).ToList();
    }

    private void RotateArrow()
    {
        float currentDistance = 99999f;
        
        for(int i = _lastDistanceIndex; i < oxygenPoints.Count && i < maxIndex; i++)
        {
            if (oxygenPoints[i] == null)
            {
                UpdateList();
                continue;
            }
            
            var checkedDistance = (transform.position - oxygenPoints[i].position).sqrMagnitude;
            if (checkedDistance < currentDistance)
            {
                currentPoint = oxygenPoints[i].position;
                currentDistance = checkedDistance;
                
            }
            _lastDistanceIndex = i;
        }
    
        if (_lastDistanceIndex >= oxygenPoints.Count - 1) 
            _lastDistanceIndex = 0;
        
        arrow.LookAt(currentPoint);
        arrow.eulerAngles = new Vector3(0, arrow.eulerAngles.y, 0);
    }
}
