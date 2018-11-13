using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPLayer : NetworkBehaviour {


	[SyncVar]
	string playerName="";
	[SyncVar]
	public int playerColorIndex;
	[SyncVar]
	bool hasColor;

	private ColorController colorController;



	void Update () {
		this.GetComponentInChildren<TextMesh> ().text = playerName;

		return; // for now
		colorController = GameObject.FindGameObjectWithTag ("Mananger").GetComponent<ColorController> ();
		colorController.UpdateLocalColor (playerColorIndex,this.transform.gameObject);
	}

	void OnGUI()
	{
		if (isLocalPlayer) 
		{
			playerName = GUI.TextField (new Rect (25, Screen.height - 40, 100, 30), playerName);
			if (GUI.Button (new Rect (130, Screen.height - 40, 80, 30), "OK")) {
				
				CmdChangeName (playerName);
				colorController = GameObject.FindGameObjectWithTag ("Mananger").GetComponent<ColorController> ();
				if (!isServer && !hasColor) {
					
					CmdAssignNewColorToClient ();
				}

				if (isServer) {
					
					GameObject.FindGameObjectWithTag ("Mananger").GetComponent<ColorController> ().status[playerColorIndex] = true;
				}
			}
		}
	}
		
	[Command]                                             // Update player's name
	public void CmdChangeName(string newName)
	{
		this.playerName = newName;
	}
		
	[Command]                                           // This function will only be used by the client, to ask color from the server
	public void CmdAssignNewColorToClient()
	{
		RpcChangeColor ();
	}

	[ClientRpc]                                        // This function will only be used bt the server, to broadcast everyone color
	void RpcChangeColor()
	{
		colorController = GameObject.FindGameObjectWithTag ("Mananger").GetComponent<ColorController> ();
		playerColorIndex = colorController.GetColorIndex (playerColorIndex);
		colorController.status[playerColorIndex] = true;
		GetComponent<MeshRenderer> ().material.color = colorController.colors [playerColorIndex];
		hasColor = true;
	}
}
