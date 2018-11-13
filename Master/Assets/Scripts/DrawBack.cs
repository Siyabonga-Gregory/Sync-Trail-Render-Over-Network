using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBack : MonoBehaviour {

	public float depth=10.0f;
	public GameObject hand;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			return;   // for now 
			var startPos = hand.transform.localPosition;
			var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3 (startPos.x, startPos.y, startPos.z));
			this.gameObject.transform.position = wantedPos;
		}
	}
}
