using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialtyToParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent.gameObject.GetComponent<CommunityScript>().Specialty += 10;
	}



		

	}

