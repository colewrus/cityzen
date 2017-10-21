using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class citizenScript : MonoBehaviour {

    public bool patron;
    public bool vendor;
    public bool citizen;
    public Material highlight;
    public Material defaultMat;

    public GameObject textWindow;


   
   

    //Hunger function variables
    public float hunger;
    public GameObject currentBldg; //what building are you currently inside?
    public Vector3 navTarget; //navMesh target - vector3?
    bool getDining; //do you need to grab a dining location?
    bool dineCollide; //did you hit the trigger for the dining box - prevents the capsule collider from having double contact with trigger
    bool leaveBool; //trigger to start the leave process. Might be obsolete
    GameObject foodObj; //used for consuming food, obj holds values 
    public NavMeshAgent agent;

    //wander variable
    public float wTimer; //wander timer
    public float tempTimer;
    public float yCorrection; //because it the wander point pulls a point from a sphere 
    public List<string> c_PhrasesPatron = new List<string>();
    public bool bool_Wander;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        citizen = true;    
        getDining = false;
        dineCollide = false;
        float tempTimer = wTimer;
    }
	
	// Update is called once per frame
	void Update () {
        Hunger();
        agent.destination = navTarget;
        transform.LookAt(navTarget);
        if (tempTimer > 0){

            tempTimer -= 1 * Time.deltaTime;
        }else if(bool_Wander)
        {
            navTarget = new Vector3(0,0,3) + Random.insideUnitSphere * 15;
            navTarget.y = yCorrection;
            tempTimer = wTimer;
        }
        else
        {
            tempTimer = wTimer;
        }
    }

    private void OnMouseOver()
    {
        var distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position);
        if(distance < 6)
        {
            gameObject.GetComponent<Renderer>().material = highlight;

            if (Input.GetMouseButtonUp(0))
            {
                bnb_FPScontroller.instance.lockCursor = false;
                textWindow.SetActive(true);
                textWindow.transform.GetChild(0).GetComponent<Text>().text = c_PhrasesPatron[0];
            }
        }

    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material = defaultMat;
    }

    public void CloseText()
    {
        textWindow.SetActive(false);
        bnb_FPScontroller.instance.lockCursor = true;
    }


    //Hunger  check script
    public void Hunger()
    {
        hunger -= 0.1f * Time.deltaTime;
        if (hunger <= 40)
        {
            bool_Wander = false;
            if (currentBldg.GetComponent<BuildingScript>().food_Vend)
            {
                if (currentBldg.GetComponent<BuildingScript>().diningLocations.Count > 0)
                {
                    if(!getDining)
                    {
                        navTarget = currentBldg.GetComponent<BuildingScript>().diningLocations[0];
                        navTarget.y = yCorrection;
                        currentBldg.GetComponent<BuildingScript>().diningLocations.Remove(navTarget);
                        getDining = true;
                    }                                          
                }
                else
                {
                    getDining = false;                    
                    navTarget = gameObject.transform.localPosition;                                       
                }
            }            
        }else
        {
            return;
        }
    }


    void OnTriggerEnter(Collider other)
    {       
        if(other.tag == "diningSpot")
        {
            if (!dineCollide && getDining) //make sure they want to get some food and haven't already touched collider
            {
                hunger += 40;
                currentBldg.GetComponent<BuildingScript>().diningLocations.Add(navTarget);
                navTarget = gameObject.transform.localPosition;
                getDining = false;
                Debug.Log("wut");
                dineCollide = true;
                bool_Wander = true;
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
    // on trigger enter
}



