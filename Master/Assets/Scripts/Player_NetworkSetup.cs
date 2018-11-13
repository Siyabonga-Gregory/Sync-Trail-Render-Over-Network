using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) 
		{
			GetComponent<CharacterController> ().enabled = true;
			GetComponent<PlayerController> ().enabled = true;
		}
	}
}
