using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pop_Home : MonoBehaviour {


    public int maxPop;
    public int currentPop;
    public float popRate;
    public float popRateSpeed;
    public float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        if(currentPop < maxPop)
        {
            timer += popRateSpeed * Time.deltaTime;
            if (timer >= popRate)
            {
                currentPop += 1;
                timer = 0;
                GameObject temp = (GameObject)Instantiate(popGM.instance.person, (transform.forward * 0.2f)*currentPop, Quaternion.identity);
            }
        }
		
	}
}
