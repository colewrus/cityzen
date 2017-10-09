using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popGM : MonoBehaviour {

    public static popGM instance = null;
    public GameObject person;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
