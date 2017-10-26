using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class BuildingScript : MonoBehaviour {

    [System.Serializable]
    public class GuestRoom
    {
        public GameObject guestObj;
        public Vector3 room_Location;
        public bool occupied;
    }




    public static BuildingScript instance = null;

    //Vendor variables 
    public bool food_Vend; //is this a food vendor?
    public List<GameObject> foodList = new List<GameObject>();
    GameObject vendor; //player and/or cityzen. Gonna need a way to scale up for multiple vendors

    //Variables for Spawning Cityzens
    public Vector3 spawnPoint;
    public GameObject zenPrefab;
    GameObject tempZen;
    public GameObject textBox;
    
    //General variables
    public List<GameObject> occupants = new List<GameObject>(); //keeps track of who is in the building

    //Dining variables
    public List<Vector3> diningLocations = new List<Vector3>();

    //Hotel variables
    //public List<GameObject> guests = new List<GameObject>(); //tracks who is staying overnight;
    [SerializeField]
    public List<GuestRoom> guestRooms;


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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddCityzen();
        }       
	}

  

    public void AddCityzen()
    {
        tempZen = Instantiate(zenPrefab, spawnPoint, Quaternion.identity);
        occupants.Add(tempZen);
        tempZen.GetComponent<citizenScript>().currentBldg = this.gameObject;
        GM.instance.MASTER_Occupants.Add(tempZen);
        tempZen.GetComponent<citizenScript>().hunger = 100;
        tempZen.GetComponent<citizenScript>().navTarget = GameObject.FindGameObjectWithTag("buildingController").transform.GetChild(0).transform.position;
        tempZen.GetComponent<citizenScript>().textWindow = textBox;
        tempZen.GetComponent<citizenScript>().lf_Hotel = true;
       
    }
}
