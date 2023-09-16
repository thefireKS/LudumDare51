using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]
public class Dog : MonoBehaviour
{
    private NavMeshAgent agent;
    
    [SerializeField] private Animator anim;

    private static List<Dog> instance = new List<Dog>();
    
    [SerializeField] private Transform mainTarget;
    [SerializeField] private Transform playerTarget;
    private Transform currentTarget;

    [SerializeField] private float agroRadius = 10f;
    [SerializeField] protected float biteRadius = 2f;

    private float biteCooldown = 1f;
    protected bool canBite = false;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = biteRadius;
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        instance.Add(GetComponent<Dog>());
    }

    private void Update()
    {
        Debug.Log(instance);
        if (!canBite)
        {
            if (mainTarget == null)
                FindToy();

            if (mainTarget != null)
                FindPath();
        }
    }

    private void FindToy()
    {
        if (GameObject.FindWithTag("DogToy") != null)
            mainTarget = GameObject.FindWithTag("DogToy").transform;
        else
            mainTarget = playerTarget.transform;
    }

    private void FindPath()
    {
        anim.SetBool("Kiril gad",true);
        
        float distance = agroRadius + 1f;

        if (mainTarget != null)
            distance = Vector3.Distance(mainTarget.position, transform.position);

        if (distance <= agroRadius)
            currentTarget = mainTarget;
        else
        {
            distance = Vector3.Distance(playerTarget.position, transform.position);

            if (distance <= agroRadius)
                currentTarget = playerTarget;
            else
                currentTarget = null;
        }

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);

            if (distance <= biteRadius)
            {
                if (currentTarget == playerTarget && canBite)
                {
                    canBite = false;
                    BiteCooldown();
                }
                else if (currentTarget == mainTarget)
                {
                    // как кушать косточку
                }
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator BiteCooldown()
    {
        canBite = true;
        anim.SetBool("Kiril gad", false);
        yield return new WaitForSeconds(biteCooldown);
        canBite = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, biteRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, agroRadius);
    }

    public static void GetToy(Transform target)
    {
        foreach (Dog doge in instance)
        {
            doge.mainTarget = target.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("DogToy"))
            BiteCooldown();
    }
}