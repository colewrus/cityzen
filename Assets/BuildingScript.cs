using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingScript : MonoBehaviour {

    public bool food_Vend; //is this a food vendor?
    public List<GameObject> foodList = new List<GameObject>();
    public GameObject vendor; //player and/or cityzen. Gonna need a way to scale up for multiple vendors


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
