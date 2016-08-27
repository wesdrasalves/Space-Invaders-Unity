using UnityEngine;
using System.Collections;

public class BotaoPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		OnMouseDown ();
		if(Input.GetKey (KeyCode.Escape))
		{
			Application.Quit();
		}

	}

	void OnMouseDown () {
		if (Input.GetKey ("mouse 0")) {
			Application.LoadLevel("GamePlay");
		}
	}
}
