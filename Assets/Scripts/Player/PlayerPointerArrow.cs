using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPointerArrow : MonoBehaviour
{
    [SerializeField] private Transform arrow;

    private Vector3 currentPoint;
    
    private List<Transform> oxygenPoints;

    private void OnEnable()
    {
        InteractablesSpawningManager.AddOxygenToPointerList += AddNewOxygenToList;
    }
    
    private void OnDisable()
    {
        InteractablesSpawningManager.AddOxygenToPointerList -= AddNewOxygenToList;
    }

    private void Start()
    {
        oxygenPoints = new List<Transform>();
    }

    private void FixedUpdate()
    {
        RotateArrow();
    }

    private void UpdateList()
    {
        oxygenPoints = oxygenPoints.Where(o => o != null).ToList();
    }

    private void AddNewOxygenToList(Transform newOxygenTransform)
    {
        oxygenPoints.Add(newOxygenTransform);
    }

    private void RotateArrow()
    {
        float currentDistance = 99999f;
        
        for(int i = 0; i < oxygenPoints.Count; i++)
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
        }
        
        arrow.LookAt(currentPoint);
        arrow.eulerAngles = new Vector3(0, arrow.eulerAngles.y, 0);
    }
}
