using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingScript : MonoBehaviour {

    public static BuildingScript instance = null;

    public bool food_Vend; //is this a food vendor?
    public List<GameObject> foodList = new List<GameObject>();
    public GameObject vendor; //player and/or cityzen. Gonna need a way to scale up for multiple vendors

    public List<Vector3> diningLocations = new List<Vector3>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
