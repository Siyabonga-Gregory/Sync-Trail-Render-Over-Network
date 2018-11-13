using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	void Update () {

		if (!isLocalPlayer)return;

		this.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 1000.0f);
		var x = Input.GetAxisRaw ("Horizontal") * Time.deltaTime * 400.0f;
		var z = Input.GetAxisRaw ("Vertical") * Time.deltaTime * 5.0f;
		this.transform.Rotate (0, x, 0);
		this.transform.transform.Translate (0, 0, z);
	}
}
