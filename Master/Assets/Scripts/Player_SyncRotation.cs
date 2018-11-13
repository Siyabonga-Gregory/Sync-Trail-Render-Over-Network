using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PLayer_SyncRotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncPLayerRotation;
	[SyncVar] private Quaternion syncCamRotation;

	[SerializeField]private Transform playerTransform;
	[SerializeField]private Transform camTransform;
	[SerializeField]private float lerpRate = 15;

	void Update () {
		TransmitRotations ();
		LerpRotation ();
	}

	void LerpRotation()
	{
		if (!isLocalPlayer) {
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, syncPLayerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRotation,Quaternion camRotation)
	{
		syncPLayerRotation = playerRotation;
		syncCamRotation = camRotation;
	}

	[ClientCallback]
	void TransmitRotations()
	{
		if (isLocalPlayer) {
			CmdProvideRotationsToServer (playerTransform.rotation, camTransform.rotation);
		}
	}
}
