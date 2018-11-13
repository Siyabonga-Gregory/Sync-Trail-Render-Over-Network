using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;
	[SyncVar]
	public bool  isMoving;

	[SerializeField] Transform myTransForm;
	[SerializeField] float lerpRate=15;

	private Vector3 lastPos;
	private float threshold=0.5f;

	void Update()
	{
		if (this.transform.name.Equals ("cube")) {
			LerpPosition ();	
		}
	}

	void FiredUpdate()
	{
		TransmitPosition ();
		LerpPosition ();	
	}

	void LerpPosition()
	{
		if (!isLocalPlayer) 
		{
			myTransForm.position = Vector3.Lerp (myTransForm.position, syncPos, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdprovidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
		isMoving = true;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer /*&& Vector3.Distance(myTransForm.position,lastPos)>threshold*/) {
			CmdprovidePositionToServer (myTransForm.position);
			lastPos = myTransForm.position;
			isMoving = true;
		}
	}
}
