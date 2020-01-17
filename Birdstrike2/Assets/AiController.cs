using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{

    NavMeshAgent agent;
    public GameObject goal;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
