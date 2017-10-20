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
    float hunger;
    public GameObject currentBldg; //what building are you currently inside?
    Vector3 navTarget; //navMesh target - vector3?
    bool getDining; //do you need to grab a dining location?
    bool dineCollide; //did you hit the trigger for the dining box - prevents the capsule collider from having double contact with trigger
    bool leaveBool; //trigger to start the leave process. Might be obsolete
    GameObject foodObj; //used for consuming food, obj holds values 
    



    public List<string> c_PhrasesPatron = new List<string>();

	// Use this for initialization
	void Start () {
        citizen = true;
        navTarget = this.transform.position;
        Debug.Log(navTarget);
        getDining = false;
        hunger = 40;

        dineCollide = false;
    }
	
	// Update is called once per frame
	void Update () {
        Hunger();
        
		
	}

    private void OnMouseOver()
    {
        
        gameObject.GetComponent<Renderer>().material = highlight;

        if (Input.GetMouseButtonUp(0))
        {
            bnb_FPScontroller.instance.lockCursor = false;
            textWindow.SetActive(true);
            textWindow.transform.GetChild(0).GetComponent<Text>().text = c_PhrasesPatron[0];
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
            
            if(currentBldg != null)
            {
                if (currentBldg.GetComponent<BuildingScript>().food_Vend)
                {
                    if (currentBldg.GetComponent<BuildingScript>().diningLocations.Count > 0)
                    {
                        if(!getDining)
                        {
                            navTarget = currentBldg.GetComponent<BuildingScript>().diningLocations[0];
                            currentBldg.GetComponent<BuildingScript>().diningLocations.Remove(navTarget);
                            getDining = true;
                        }
                        NavMeshAgent agent = GetComponent<NavMeshAgent>();
                        
                        
                        agent.destination = navTarget;
                    }
                    else
                    {
                        getDining = false;
                        navTarget = this.transform.position;
                        NavMeshAgent agent = GetComponent<NavMeshAgent>();
                        agent.destination = navTarget;
                    }
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
                navTarget = this.transform.position;
                getDining = false;
                Debug.Log("wut");
                dineCollide = true;
            }
 
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
    // on trigger enter
}



