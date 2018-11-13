using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

	public class Draw : NetworkBehaviour {
	public float depth=10.0f;    
	public GameObject pencil;                   //  pencil prefab, would be spawn when issue a new draw
	public GameObject hand;                    //   Would be use for pen start position
	public Material[]pencilColors;
	int colorStartIndex=0;
	List<GameObject> usedPencils= new List<GameObject>();
	int deleteIndex=0;

	void Update () 
	{  
		hand = GameObject.FindGameObjectWithTag ("Player");
		if (hand.activeSelf) {
			var startPos = hand.transform.localPosition;
			var wantedPos = Camera.main.WorldToScreenPoint (new Vector3 (startPos.x, startPos.y, depth));
			transform.position = wantedPos;
		}
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown (0)) {
			Delete (false);
		} else if (Input.GetMouseButtonDown (1)) {
			
			GameObject childObject = hand.transform.GetChild (2).transform.gameObject;
			GameObject oldPencils = GameObject.FindGameObjectWithTag ("UsedPencils");

			if (childObject.GetComponent<TrailRenderer> ().enabled) {
				if (childObject.tag.Equals ("Pencil") || childObject.transform.name.Equals ("Pencil(Clone)")) {
					childObject.transform.SetParent (oldPencils.transform);
					usedPencils.Add (childObject);
				}

				GameObject pen = (GameObject)Instantiate (pencil, hand.transform.localPosition, hand.transform.localRotation);
				NetworkServer.Spawn (pen);
				pen.transform.SetParent (hand.transform);
				pen.GetComponent<TrailRenderer> ().enabled = false;

			}
		} else if (Input.GetMouseButtonDown (2)) {
			GameObject childObject = hand.transform.GetChild (2).transform.gameObject;
			childObject.GetComponent<TrailRenderer> ().material = pencilColors [colorStartIndex];
			hand.GetComponent<MeshRenderer> ().material = pencilColors [colorStartIndex];
			colorStartIndex += 1;

			if (colorStartIndex == pencilColors.Length) {
				colorStartIndex = 0;
			}
		} 
		else if (Input.GetKey(KeyCode.Delete))
		{
			Delete (true);
		}
 	}

	void Delete(bool delete)
	{
		
		GameObject childObject = hand.transform.GetChild (2).transform.gameObject;
		GameObject oldPencils = GameObject.FindGameObjectWithTag ("UsedPencils");

		if (delete) {
			Destroy (childObject);
			//usedPencils.Reverse ();
			//Destroy (usedPencils [usedPencils.Count-1].gameObject);   will look at it later
			//usedPencils.RemoveAt (usedPencils.Count-1);
			SpawnNewPencil ();
		} else {
			if (!childObject.GetComponent<TrailRenderer> ().enabled) {
				Destroy (childObject);
				SpawnNewPencil ();
			}
		}
	}

	void SpawnNewPencil()
	{
		GameObject pen = (GameObject)Instantiate (pencil, hand.transform.localPosition, hand.transform.localRotation);
		NetworkServer.Spawn (pen);
		pen.transform.SetParent (hand.transform);
		pen.GetComponent<TrailRenderer> ().material = pencilColors [colorStartIndex];
		hand.GetComponent<MeshRenderer> ().material = pencilColors [colorStartIndex];
	}
}
