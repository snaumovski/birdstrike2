using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StudentAiController : MonoBehaviour
{
        NavMeshAgent agent;
        public GameObject goal;
        public int RandomEventInt;
        public bool idle;
        private Vector3 Destination;
    private void Start()
        
        {
            agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);

    }



        // Update is called once per frame
        void Update()
        {
        if (idle == false)
        {
            RandomEventInt = Random.Range(0, 10000);
            agent.isStopped = false;
        }
        else 
         {
            Debug.Log(gameObject.name + " I am stopped " + RandomEventInt);
            agent.isStopped = true;
        }


        switch (RandomEventInt)
        {
            case 250:
                //pause lost way
                StartCoroutine(IdleTime(3));
                break;
            case 7:
                //pause cell break
                StartCoroutine(IdleTime(8));
                break;
            case 180:
                //break longest
                StartCoroutine(IdleTime(11));
                break;

        }
        }


    IEnumerator IdleTime(float TimeToBeIdle)
    {
        yield return new
            WaitForSeconds(0);
            idle = true;
        yield return new
            WaitForSeconds(TimeToBeIdle);
        idle = false;
    }
    }
