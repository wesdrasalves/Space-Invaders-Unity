using UnityEngine;
using System.Collections;

public class Bloco : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collisor)
	{
		if(collisor.gameObject.tag.IndexOf("Tiro") > -1)
			Destroy(gameObject.transform.parent.gameObject);
	}

}
