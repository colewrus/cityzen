using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingScript : MonoBehaviour {

    public static BuildingScript instance = null;


    public bool food_Vend; //is this a food vendor?
    public List<GameObject> foodList = new List<GameObject>();
    GameObject vendor; //player and/or cityzen. Gonna need a way to scale up for multiple vendors

    //Variables for Spawning Cityzens
    public Vector3 spawnPoint;
    public GameObject zenPrefab;
    GameObject tempZen;
    public GameObject textBox;
    public List<GameObject> occupants = new List<GameObject>();

    public List<Vector3> diningLocations = new List<Vector3>();

    public float clock; 

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
    }

    // Use this for initialization
    void Start () {
        tempZen = null;
        clock = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            AddCityzen();
        }
        Hours();
	}

    void Hours(){
        clock += 3 * Time.deltaTime;
        if(clock >= 14)
        {
            Debug.Log("day end");
            clock = 0;
        }
    }

    public void AddCityzen()
    {
        tempZen = Instantiate(zenPrefab, spawnPoint, Quaternion.identity);
        occupants.Add(tempZen);
        tempZen.GetComponent<citizenScript>().hunger = 100;
        tempZen.GetComponent<citizenScript>().navTarget = GameObject.FindGameObjectWithTag("buildingController").transform.GetChild(0).transform.position;
        tempZen.GetComponent<citizenScript>().textWindow = textBox;       
       
    }
}
