using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {


    /// <summary>
    /// Make an enum script to choose what item this is, set the editor to only show certain options
    /// </summary>


        // Quest Vars
    public Quest tempQuest;
    public Material highlight;
    public Material default_Mat;

    //node vars
    public Material node_highlight;
    public Material node_default;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        var distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position);
        if (distance <= 15)
        {
            if (gameObject.tag == "questObj")
            {
                QuestItem();
            }

            if (gameObject.tag == "node")
            {
                NodeF();
            }
        }
        
    }

    void QuestItem()
    {
        gameObject.GetComponent<Renderer>().material = highlight;
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < bnb_FPScontroller.instance.myQuests.Count; i++)
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


    void NodeF()
    {
        gameObject.GetComponent<Renderer>().material = node_highlight;
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("node clicked");
            bnb_FPScontroller.instance.nodeModal.SetActive(true);
            bnb_FPScontroller.instance.lockCursor = false;
        }
    }

    private void OnMouseExit()
    {
        if(gameObject.tag == "questObj")
        {
            gameObject.GetComponent<Renderer>().material = default_Mat;
           
        }
        if(gameObject.tag == "node")
        {
            gameObject.GetComponent<Renderer>().material = node_default;            
        }
        
    }
}
