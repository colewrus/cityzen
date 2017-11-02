using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;




public class citizenScript : MonoBehaviour {

    //unique identifier
    public int ZEN_ID;

    //room?
    

    public bool patron;
    public bool vendor;
    public bool citizen;
    public Material highlight;
    public Material defaultMat;

    public GameObject textWindow;


    //general vars
    public float contactDistance; //how far can you be from the citzen for you to interact?
    public GameObject currentBldg; //what building are you currently inside?
    public GameObject home; //permanent home, set some despawn point on map edge if homeless for now
    public bool inside;
    public Vector3 navTarget; //navMesh target - vector3?
    public Transform walkingSphere;

    //quest var
    public GameObject questTextPanel;
    public bool questGiver;
    public bool questTurnIn; //are you waiting for a player to turn in an active quest. We need a much better system.....
    Quest tempQuest;

    //Customer variables
    public bool lf_Hotel; //looking for hotel

    //Chat vars
    public List<GameObject> chatZenList = new List<GameObject>();

    //Hunger function variables
    public float hunger;    
    
    bool getDining; //do you need to grab a dining location?
    bool dineCollide; //did you hit the trigger for the dining box - prevents the capsule collider from having double contact with trigger
    bool leaveBool; //trigger to start the leave process. Might be obsolete
    GameObject foodObj; //used for consuming food, obj holds values 
    public NavMeshAgent agent;

    //exit variable
    public bool check_Exit; //do you have pending exit command? should you be heading to exit


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
        bool_Wander = false;
        ZEN_ID = GM.instance.MASTER_Occupants.Count;
        agent.destination = navTarget;
        Debug.Log(agent.destination + " !: " + navTarget);
        walkingSphere = GameObject.FindGameObjectWithTag("walkSphere").transform;

        //quest init
        questTextPanel = GameObject.Find("questPanel");
        questTextPanel.SetActive(false);
        questTurnIn = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Hunger();
        agent.destination = navTarget;
       if(agent.destination != navTarget)
            transform.LookAt(new Vector3(navTarget.x, gameObject.transform.position.y, navTarget.z));
        Wander();


    }

    void Wander()
    {
        if (tempTimer > 0)
        {

            tempTimer -= 1 * Time.deltaTime;
        }
        else if (bool_Wander)
        {
            navTarget = walkingSphere.position + Random.insideUnitSphere * walkingSphere.GetComponent<SphereCollider>().radius;
            Vector3 navCheck = navTarget + transform.position;
            Debug.Log(navCheck);
            NavMeshHit hit;
            NavMesh.SamplePosition(navCheck, out hit, walkingSphere.GetComponent<SphereCollider>().radius, 1);
            Debug.Log(hit.position);
            navTarget.y = gameObject.transform.position.y;
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
        if(distance < contactDistance)
        {
            gameObject.GetComponent<Renderer>().material = highlight;

            if (Input.GetMouseButtonUp(0))
            {
                bnb_FPScontroller.instance.lockCursor = false;
                if (questGiver)
                {
                    //bnb_FPScontroller.instance.lockCursor = false;
                                    
                    if (!questTurnIn)
                    {
                        questTextPanel.SetActive(true);

                        bnb_FPScontroller.instance.myQuests.Add(new Quest("test", ZEN_ID, 0));
                       // bnb_FPScontroller.instance.myQuests.Add(GM.instance.zenQuests[0]);
                        bnb_FPScontroller.instance.myQuests[bnb_FPScontroller.instance.myQuests.Count - 1].ID = ZEN_ID;
                        questTurnIn = true;
                    }
                    else if(questTurnIn)
                    {
                        
                        for (int i = 0; i < bnb_FPScontroller.instance.myQuests.Count; i++)
                        {
                            if (bnb_FPScontroller.instance.myQuests[i].ID == ZEN_ID && bnb_FPScontroller.instance.myQuests[i].objCollected)
                            {
                                Debug.Log("you did the thing!");
                                questTurnIn = false;
                            }
                        }
                    }
                      

                }else if (!questGiver)
                {
                    bnb_FPScontroller.instance.lockCursor = false;
                    textWindow.SetActive(true);
                    textWindow.transform.GetChild(0).GetComponent<Text>().text = c_PhrasesPatron[0];
                }

            }
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material = defaultMat;
    }

    public void CloseText()
    {
        if (textWindow.active)
        {
            textWindow.SetActive(false);
        }else if (questTextPanel.active)
        {
            questTextPanel.SetActive(false);
        }        
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
                        //navTarget.y = gameObject.transform.position.y;
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

        //collide with the check in 
            //show that check in is occupied, set a spot in the waiting room (sphere near the desk)
        if(other.gameObject.name == "check_In")
        {
            CheckIn_Hotel();
        }

        if(other.gameObject.tag == "exit")
        {
            if (check_Exit)
            {
                currentBldg = null;
                if (home != null)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                    //need some way to throw them back into the pool of prefabs to spawn (oh hey, object pooling). 
                    //Maybe creation process can just randomly roll attributes, initial load of game can instantiate then deactivate a pool of characters.
                    //Pool scales as the town grows
                }
                check_Exit = false;
                bool_Wander = false;                
            }
        }

        if(other.gameObject.tag == "zen")
        {
            chatZenList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "zen")
        {
            chatZenList.Remove(other.gameObject);
        }
    }

    void CheckIn_Hotel()
    {
        if (lf_Hotel)
        {
            for (int i = 0; i < currentBldg.GetComponent<BuildingScript>().guestRooms.Count; i++)
            {
                if (!currentBldg.GetComponent<BuildingScript>().guestRooms[i].occupied)
                {
                    if (GM.instance.clock > 120)
                    {
                        bool_Wander = true;
                        tempTimer = 0;
                    }
                    else   //go into town
                    {
                        navTarget = currentBldg.transform.GetChild(1).transform.position;
                        //navTarget.y = this.transform.position.y;
                        check_Exit = true;
                        bool_Wander = false;                        
                    }

                    lf_Hotel = false; //no longer looking for hotel
                    currentBldg.GetComponent<BuildingScript>().guestRooms[i].guestObj = this.gameObject;
                    home = currentBldg;
                    currentBldg.GetComponent<BuildingScript>().guestRooms[i].occupied = true; //make the hotel room occupied
                    break; //get out of the loop 
                }
                else  //no rooms available. Leave some sort of feedback so the player knows who they missed. 
                {
                    navTarget = currentBldg.transform.GetChild(1).transform.position;
                    //navTarget.y = this.transform.position.y;

                    check_Exit = true;
                }
            }
        }
    }

    public void GoHome()
    {
        if(GM.instance.currentPerspective == GM.Perspective.indoors)
        {
            if (currentBldg == null)
            {
                check_Exit = false;
                gameObject.SetActive(true);
                currentBldg = home;
                gameObject.transform.position = currentBldg.transform.GetChild(1).transform.position;
                bool_Wander = false;

                //do Zens spend time just chilling in home areas? What sets their behavior at home?
                //Start with just time of day, maybe a morning > day > evening > sleep cycle
                //gonna need to check if they are a guest or homeowner
                for(int i=0; i< currentBldg.GetComponent<BuildingScript>().guestRooms.Count; i++)
                {
                    GameObject clone = currentBldg.GetComponent<BuildingScript>().guestRooms[i].guestObj;
                    if(clone.GetComponent<citizenScript>().ZEN_ID == ZEN_ID)
                    {
                        Debug.Log(currentBldg.GetComponent<BuildingScript>().guestRooms[i].room_Location);
                        navTarget = currentBldg.GetComponent<BuildingScript>().guestRooms[i].room_Location;
                    }
                }
                //need to compare this game object identifier with the unique identifiers in the                
            }
        }
        //navTarget = home;
        //if Zen is outside send to the building of the room
        //if inside, send up to room location
    }


    public void Chat()
    {

    }
}



