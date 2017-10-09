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

    public List<string> c_PhrasesPatron = new List<string>();

	// Use this for initialization
	void Start () {
        citizen = true;
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

}
