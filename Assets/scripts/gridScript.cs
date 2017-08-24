using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gridScript : MonoBehaviour {

    public static gridScript instance = null;

    public GameObject gridTile;
    public float gridSpacing;
    public int gridSize; //defines the X and Y grid, makes a square

    public float cameraSpeed;
    public Vector3 currentTileVector;

    public List<GameObject> spawnObj = new List<GameObject>();

    public GameObject buildMenu;
    
	public GameObject tempObj;
 

    private int rotationPos; //where are we in our list of rotations
    public List<Vector3> rotations = new List<Vector3>(); //list of premade rotatoes for the game objects

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        buildMenu.SetActive(false);
  
    }

    // Use this for initialization
    void Start () {
		tempObj = null;

		for(int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++)
            {

               GameObject tempInst = (GameObject) Instantiate(gridTile, new Vector3(i * gridSpacing, 0, j* gridSpacing), Quaternion.identity);
                MeshCollider b = tempInst.GetComponent<MeshCollider>();
                tempInst.transform.position = new Vector3(i * b.bounds.size.x, 0, j * b.bounds.size.z);
                Debug.Log(b.bounds.size);
            }           
        }
	}
	
	// Update is called once per frame
	void Update () {

        //cameraControl();



		if (Input.GetMouseButtonDown (0)) {
            Debug.Log(currentTileVector);
			tempObj = null;
		}

        if (Input.GetMouseButtonDown(1)) //right click
        {
            if(tempObj != null)
            {
                Destroy(tempObj);
                tempObj = null; //get rid of temp object 
            }else
            {
                buildMenu.GetComponent<Animator>().Play("buildMenu_open"); //open up the menu
                
                if (buildMenu.activeSelf)
                {
                    buildMenu.GetComponent<Animator>().Play("close_buildMenu"); //if menu is already open then close it
                }
                else
                {
                    buildMenu.SetActive(true); 
                }                
            }
        }

        if(tempObj != null && Input.GetKeyDown(KeyCode.R))
        {
            if (rotationPos < rotations.Count - 1)
            {
                rotationPos++;
            }
            else
            {
                rotationPos = 0;
            }

            Vector3 changeRot = rotations[rotationPos];
            tempObj.transform.rotation = Quaternion.Euler(changeRot);
        }

        //update the temporary object, snaps to the grid tile that you hover over
		if (tempObj != null) {
			tempObj.transform.position = currentTileVector;
		}
	}



    void cameraControl()
    {
        if (Input.mousePosition.x >= Screen.width - 50)
        {
            Camera.main.transform.position += new Vector3(cameraSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.mousePosition.x <= 0 + 50)
        {
            Camera.main.transform.position -= new Vector3(cameraSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.mousePosition.y >= Screen.height - 50)
        {
            Camera.main.transform.position += new Vector3(0, 0, cameraSpeed) * Time.deltaTime;
        }

        if (Input.mousePosition.y <= 0 + 50)
        {
            Camera.main.transform.position -= new Vector3(0, 0, cameraSpeed) * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.transform.position += new Vector3(0, cameraSpeed*2, 0) * Time.deltaTime;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.transform.position -= new Vector3(0, cameraSpeed*2, 0) * Time.deltaTime;
        }
    }

    public void objSelect(int num)
    {
        tempObj = (GameObject)Instantiate(spawnObj[num], currentTileVector, Quaternion.identity);
        buildMenu.GetComponent<Animator>().Play("close_buildMenu");
    }
}
//test commenter here. 
