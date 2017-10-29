using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {


    public Quest tempQuest;
    public Material highlight;
    public Material default_Mat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material = highlight;
        if (Input.GetMouseButtonDown(0))
        {           
            for(int i=0; i<bnb_FPScontroller.instance.myQuests.Count; i++)
            {
                tempQuest = bnb_FPScontroller.instance.myQuests[i];

                if (tempQuest.item.questItem = gameObject)
                {
                    Debug.Log("tally fucking ho");
                    bnb_FPScontroller.instance.myQuests[i].objCollected = true;
                    break;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material = default_Mat;
    }
}
