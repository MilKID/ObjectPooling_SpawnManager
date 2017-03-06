sing UnityEngine;u

public class SpawnManager : MonoBehaviour {

	[Header("Settings:")]
	public int maxAmountInScene;
	public float spawnRange;
	public LayerMask unspawnableLayer;
	public float objectRadius;
	public bool logMessages;

	private Vector3 currentSpawnpoint;
	private int spawnIndex;

	private void Start ()
	{
		spawnIndex = 0;	
	}

	private void Update ()
	{
		if (Pooling.activeObjectA.Count < maxAmountInScene) {
			GenerateSpawnpoint ();
		}
		if (spawnIndex > Pooling.objectAPool.Count) {
			spawnIndex = 0;
		}
	}

	private void GenerateSpawnpoint ()
	{
		float randomizedX = Random.Range(transform.position.x + spawnRange, transform.position.x - spawnRange);
		float randomizedZ = Random.Range(transform.position.z + spawnRange, transform.position.z - spawnRange);
		Vector3 newSpawnpoint = new Vector3(randomizedX, transform.position.y ,randomizedZ);
		CheckSpawnpoint(newSpawnpoint);
	}

	private void CheckSpawnpoint (Vector3 spawnpoint)
	{
		if(!Physics.CheckSphere(spawnpoint,objectRadius,unspawnableLayer)){u
		obj.SetActive (true);
		Pooling.activeObjectA.Add (obj);
		Pooling.objectAPool.Remove (obj);
		spawnIndex++;
		if (logMessages) {
			print("Spawned Object");
		}
	}
}
