using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivicSupportScript : MonoBehaviour {
	public float radius = 0f;
	public Collider[] colliders;
	//public int colliders;
	public int Community = 0;
	public int CivicSupport=0;
	public int EconOpp=0;
	public int Specialty=0;
	//private float Com;
	public GameObject Circle;
	public LayerMask mask;
	public LayerMask mask2;
	public LayerMask mask3;
	public LayerMask mask4;

	void LateUpdate()
	{
		colliders = Physics.OverlapSphere (transform.position, radius, mask);
		Community = colliders.Length;

		colliders = Physics.OverlapSphere (transform.position, radius, mask2);
		CivicSupport = colliders.Length-1;

		colliders = Physics.OverlapSphere (transform.position, radius, mask3);
		EconOpp = colliders.Length;

		colliders = Physics.OverlapSphere (transform.position, radius, mask4);
		Specialty = colliders.Length;
	}

	void Update()
	{
		Circle.transform.localScale = new Vector3 (radius *2, .001f, radius *2);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}