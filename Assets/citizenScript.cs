using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    bool vendorProx; //close enough to a vendor to interact?
    bool leaveBool; //trigger to start the leave process. Might be obsolete
    GameObject foodObj; //used for consuming food, obj holds values 



    public List<string> c_PhrasesPatron = new List<string>();

	// Use this for initialization
	void Start () {
        citizen = true;
        Hunger();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        Debug.Log(gameObject.name);
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
        if(hunger <= 40)
        {
            if(currentBldg != null)
            {
                if (currentBldg.GetComponent<BuildingScript>().food_Vend)
                {
                    if(currentBldg.GetComponent<BuildingScript>().vendor != null)
                    {
                        navTarget = currentBldg.GetComponent<BuildingScript>().vendor.transform.position;
                        if (vendorProx)
                            Debug.Log("vendorProx");
                                //buy
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
        if(other.tag == "vendor")
        {
            vendorProx = true;
        }
    }
   // on trigger enter
}



