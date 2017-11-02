using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestItem {
    public GameObject questItem;
    public bool collectible; //does it disappear
    public bool q_Note;
    public string q_Note_text;


}

[System.Serializable]
public class Quest
{
    public string name;
    public QuestItem item;
    public int ID;    
    public string questText;
    //tracking
    public bool objCollected; //is it in your inventory/have you visited the obj?

    public Quest(string N, int Identify, int Pos) //Pos indicates the point in the GM quest list so you can pull those public assets
    {
        name = N;        
        ID = Identify;
        item = GM.instance.zenQuests[Pos].item;
        questText = GM.instance.zenQuests[Pos].questText;
        objCollected = false;
    }

}

public class GM : MonoBehaviour {

    public enum Perspective    {  indoors, outside, map}

    public static GM instance = null;

    public List<GameObject> MASTER_Occupants = new List<GameObject>();

    //Quest vars
    [SerializeField]
    public List<Quest> zenQuests = new List<Quest>();


    //Time variables - Day/Night cycle
    public float clock;
    public bool check_GoHome;
    public float nightTime;
    public Perspective currentPerspective;
    


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        check_GoHome = false;
	}
	
	// Update is called once per frame
	void Update () {
        Hours();
        NightCheck();
        
	}

    void Hours()
    {
        clock += 2 * Time.deltaTime;
        if (clock >= 240)
        {
            Debug.Log("day end");
            clock = 0;
            check_GoHome = false;
        }
    }

    void NightCheck()
    {
        if (clock >= nightTime)
        {
            if (!check_GoHome)
            {
                for (int i = 0; i < MASTER_Occupants.Count; i++)
                {
                    MASTER_Occupants[i].GetComponent<citizenScript>().GoHome();
                }
            }
            check_GoHome = true;
        }
     }
}
