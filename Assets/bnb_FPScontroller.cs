using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bnb_FPScontroller : MonoBehaviour {

    public static bnb_FPScontroller instance = null;

    public Camera playerCam;
    float mouseX;
    float mouseY;
    Vector3 mousePos;

    //UI elements
    public GameObject textWindow;
    public GameObject questTextPanel;


    public List<Quest> myQuests = new List<Quest>();
    public GameObject zenTalk; //zen you are talking to right now

    //cursor lock
    public bool lockCursor;
    bool cursorIsLocked;

    private Quaternion CharacterRot;
    private Quaternion CameraRot;

    public float playerSpeed;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);    
    }

    // Use this for initialization
    void Start () {
        CharacterRot = transform.localRotation;
        CameraRot = playerCam.transform.localRotation;
        cursorIsLocked = true;
	}

    // Update is called once per frame
    void Update() {
        mousePos = Input.mousePosition;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        if (cursorIsLocked)
        {
            CharacterRot *= Quaternion.Euler(0f, mouseX, 0f);
            CameraRot *= Quaternion.Euler(-mouseY, 0f, 0f);

            transform.localRotation = CharacterRot;
            playerCam.transform.localRotation = CameraRot;
        }

        UpdateCursorLock();
        PlayerWASD();
	}



    public void CloseText()
    {
        if (textWindow.active)
        {
            textWindow.SetActive(false);
        }
        else if (questTextPanel.active)
        {
            questTextPanel.SetActive(false);            
        }
        lockCursor = true;
    }


    public void PlayerWASD()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 desiredMove = transform.forward * vert + transform.right * horiz + (transform.up*-1);

        gameObject.GetComponent<Rigidbody>().velocity = desiredMove * playerSpeed;

    }

    public void UpdateCursorLock()
    {
        if (lockCursor)
            InternalLockUpdate();
        else
            Cursor.lockState = CursorLockMode.None;

    }

 
    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorIsLocked = true;
        }

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetCursorLock(bool setLock)
    {
        lockCursor = setLock;
    }

    void Data()
    {
        Debug.Log(CharacterRot);
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
     
    }
}
