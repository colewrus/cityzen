using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif



public enum B_Type
{
    commercial, community
}



public class building : MonoBehaviour {

    public List<GameObject> masterBuilding = new List<GameObject>();
    public B_Type myType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


[CustomEditor(typeof(building))]
public class Building_editor : Editor
{
    public override void OnInspectorGUI()
    {               
        building b_var = target as building;
        b_var.myType = (B_Type)EditorGUILayout.EnumPopup(b_var.myType);
    }

}