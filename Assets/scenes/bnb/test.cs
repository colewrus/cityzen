using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour {

    public Vector3 dest;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = dest;
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = dest;
    }
}
