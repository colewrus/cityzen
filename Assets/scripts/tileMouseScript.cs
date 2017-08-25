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
        MeshCollider b = gameObject.GetComponent<MeshCollider>();
        if(gridScript.instance.tempObj != null)
        {
            if(gridScript.instance.tmpPrefab.size > 1)
            {
                if (gridScript.instance.tempObj.transform.eulerAngles.y == 0)
                {
                    gridScript.instance.currentTileVector = b.bounds.center + new Vector3(-b.bounds.size.x / 2, 0, b.bounds.size.z / 2);
                }
                if (gridScript.instance.tempObj.transform.eulerAngles.y == 180)
                {
                    gridScript.instance.currentTileVector = b.bounds.center + new Vector3(b.bounds.size.x / 2, 0, -b.bounds.size.z / 2);
                }
            }else if(gridScript.instance.tmpPrefab.size == 1)
            {
                gridScript.instance.currentTileVector = b.bounds.center + new Vector3(-b.bounds.size.x / 2, 0, b.bounds.size.z / 2);
            }
       

           
        }
              
        

        //this.gameObject.GetComponent<MeshCollider>().bounds;
        Debug.Log(b.bounds.center  + new Vector3(-b.bounds.size.x/2, 0, -b.bounds.size.z/2));
        
    }

    
}
