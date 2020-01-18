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
        public bool inClass;
    private void Start()
        
        {
            //Grabs agent navmesh component
            agent = this.GetComponent<NavMeshAgent>();
            // sets destination on start
        agent.SetDestination(goal.transform.position);
		/*
         * agent.SetDestination(goal.transform.position);
         * 
         */
        
	}



	// Update is called once per frame
	void Update()
        {
        if (idle == false)
        {
            //If not standing still r
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
        var Destination = agent.pathEndPosition;
        var Currentpos = agent.gameObject.transform.position;
        //
        float DistanceFromTarget = Vector3.Distance(Destination, Currentpos);

        if  (DistanceFromTarget < 1){

            inClass = true;
            StartCoroutine(IdleTime(25));
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
