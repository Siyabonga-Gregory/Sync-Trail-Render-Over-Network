using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeSpawner : NetworkBehaviour {

	public GameObject cubePrefab;
	public int numberOfCubes;

	public override void OnStartServer()
	{
		for (int i=0; i < numberOfCubes; i++)
		{
			var spawnPosition = new Vector3(Random.Range(-8.0f,8.0f),0.0f,Random.Range(-8.0f, 8.0f));
			var spawnRotation = Quaternion.Euler( 0.0f,Random.Range(0,180),0.0f);
			var cube = (GameObject)Instantiate(cubePrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn(cube);
		}
	}
}
