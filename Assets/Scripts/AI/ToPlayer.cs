using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPlayer : MonoBehaviour
{
    public Vector3 dist;
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent.enabled != true)
            agent.enabled = true;
    }

    void Update()
    {
        if(agent.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
        {
                agent.SetDestination(target.position);
        }
    }
}
