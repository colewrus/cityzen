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

	private GameObject tempObj;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

  
    }

    // Use this for initialization
    void Start () {
		tempObj = null;

		for(int i = 0; i < gridSize; i++)
        {
            for(int j = 0; j < gridSize; j++)
            {
                Instantiate(gridTile, new Vector3(i * gridSpacing, 0, j* gridSpacing), Quaternion.identity);
            }           
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Input.mousePosition.x + ", " + Screen.width);
        if(Input.mousePosition.x >= Screen.width - 50)
        {           
            Camera.main.transform.position += new Vector3(cameraSpeed, 0, 0) * Time.deltaTime;
        }
        if(Input.mousePosition.x <= 0 + 50)
        {
            Camera.main.transform.position -= new Vector3(cameraSpeed, 0, 0) * Time.deltaTime;
        }

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			tempObj = (GameObject)Instantiate (spawnObj [1], currentTileVector, Quaternion.identity);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			tempObj = (GameObject)Instantiate (spawnObj [0], currentTileVector, Quaternion.identity);
		}

		if (Input.GetMouseButtonDown (0)) {
			tempObj = null;
		}

		if (tempObj != null) {
			tempObj.transform.position = currentTileVector;
		}
	}
}
