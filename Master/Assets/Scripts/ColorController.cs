using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
   This class will contain all the colors and will need to be attached to a gameobject that is  registered as spawnable prefab by the "Network Manager" . 
*/

public class ColorController : NetworkBehaviour {
   
	public Color[] colors;
	public bool [] status;

	public int GetColorIndex(int playerColorIndex)                     // Return index for un occupied color
	{
		bool isFound = false;
		playerColorIndex = 0;
		for (int i = 0; i < status.Length; i++) 
		{
			if (!isFound) {
				if(!status[i])
				{
					isFound = true;
					playerColorIndex = i;
					return i;
				}
			}
		}
		return playerColorIndex;
	}
		
	public void UpdateLocalColor(int playerColorIndex,GameObject player) // This function will be call on the update from the client side to make sure that client reflect the same color as it appears on the server
	{
		status[playerColorIndex] = true;
		player.GetComponent<MeshRenderer> ().material.color = colors [playerColorIndex];
	}

}
