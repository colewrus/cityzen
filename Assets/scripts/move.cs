using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour {

    public Text text_Console;
    public Camera alignment;
    Vector3 moveDir;
    public float speed;
	// Use this for initialization
	void Start () {
        moveDir = alignment.transform.forward;
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(alignment.transform.forward);

        OVRInput.Controller activeController = OVRInput.GetActiveController();


        moveDir = alignment.transform.forward;
        
        Vector3 pos = OVRInput.GetLocalControllerPosition(activeController);

        text_Console.text = " " + pos;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.W))
        {
            transform.position += moveDir * speed;
            text_Console.text = " " + transform.position;
        }
    
        if (OVRInput.GetDown(OVRInput.Button.Down))
        {
            transform.position += alignment.transform.forward * -speed*3;
            text_Console.text += " " + OVRInput.Button.Down;
        }
        

        if (OVRInput.Get(OVRInput.Button.Right))
        {
            transform.position += alignment.transform.right * speed*3;
        }
        if (OVRInput.Get(OVRInput.Button.Left))
        {
            transform.position += alignment.transform.right * -speed*3;
        }


        /*
         * 
      			// virtual
			new BoolMonitor("One",                      () => OVRInput.Get(OVRInput.Button.One)),
			new BoolMonitor("OneDown",                  () => OVRInput.GetDown(OVRInput.Button.One)),
			new BoolMonitor("OneUp",                    () => OVRInput.GetUp(OVRInput.Button.One)),
			new BoolMonitor("Two",                      () => OVRInput.Get(OVRInput.Button.Two)),
			new BoolMonitor("TwoDown",                  () => OVRInput.GetDown(OVRInput.Button.Two)),
			new BoolMonitor("TwoUp",                    () => OVRInput.GetUp(OVRInput.Button.Two)),
			new BoolMonitor("PrimaryIndexTrigger",      () => OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)),
			new BoolMonitor("PrimaryIndexTriggerDown",  () => OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)),
			new BoolMonitor("PrimaryIndexTriggerUp",    () => OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)),
			new BoolMonitor("Up",                       () => OVRInput.Get(OVRInput.Button.Up)),
			new BoolMonitor("Down",                     () => OVRInput.Get(OVRInput.Button.Down)),
			new BoolMonitor("Left",                     () => OVRInput.Get(OVRInput.Button.Left)),
			new BoolMonitor("Right",                    () => OVRInput.Get(OVRInput.Button.Right)),
			new BoolMonitor("Touchpad (Touch)",         () => OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)),
			new BoolMonitor("TouchpadDown (Touch)",     () => OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad)),
			new BoolMonitor("TouchpadUp (Touch)",       () => OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad)),
			new BoolMonitor("Touchpad (Click)",         () => OVRInput.Get(OVRInput.Button.PrimaryTouchpad)),
			new BoolMonitor("TouchpadDown (Click)",     () => OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)),
			new BoolMonitor("TouchpadUp (Click)",       () => OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad)),
			// raw
			new BoolMonitor("Start",                    () => OVRInput.Get(OVRInput.RawButton.Start)),
			new BoolMonitor("StartDown",                () => OVRInput.GetDown(OVRInput.RawButton.Start)),
			new BoolMonitor("StartUp",                  () => OVRInput.GetUp(OVRInput.RawButton.Start)),
			new BoolMonitor("Back",                     () => OVRInput.Get(OVRInput.RawButton.Back)),
			new BoolMonitor("BackDown",                 () => OVRInput.GetDown(OVRInput.RawButton.Back)),
			new BoolMonitor("BackUp",                   () => OVRInput.GetUp(OVRInput.RawButton.Back)),
			new BoolMonitor("A",                        () => OVRInput.Get(OVRInput.RawButton.A)),
			new BoolMonitor("ADown",                    () => OVRInput.GetDown(OVRInput.RawButton.A)),
			new BoolMonitor("AUp",                      () => OVRInput.GetUp(OVRInput.RawButton.A)),
            
      */

    }
}
