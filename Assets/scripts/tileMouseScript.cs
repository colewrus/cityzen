using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileMouseScript : MonoBehaviour {

    public Vector3 currentTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {

        gridScript.instance.currentTileVector = this.gameObject.transform.position - (this.transform.localScale / 2);
        Debug.Log(this.transform.localScale);
    }

    
}
