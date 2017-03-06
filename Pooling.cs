using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject[] pooledObjectA;
	public GameObject[] pooledObjectB;

	[Header("Settings:")]
	public int poolsizeA;
	public int poolsizeB;
	public bool shufflePoolA;
	public bool shufflePoolB;
	public bool logMessages;

	public static List<GameObject> objectAPool = new List<GameObject>();
	public static List<GameObject> objectBPool = new List<GameObject>();
	public static List<GameObject> activeObjectA = new List<GameObject>();
	public static List<GameObject> activeObjectB = new List<GameObject>();

	private void Awake ()
	{
		PopulatePool (pooledObjectA, poolsizeA, objectAPool);
		PopulatePool (pooledObjectB, poolsizeB, objectBPool);
		if (shufflePoolA) {
			ShufflePool (objectAPool);
		}
		if (shufflePoolB) {
			ShufflePool (objectBPool);
		}
	} 

	private void Update()
	{
		CheckInactive(objectAPool,activeObjectA);
		CheckInactive(objectBPool,activeObjectB);
	}

	private void PopulatePool (GameObject[] prefab, int poolsize, List<GameObject> pool)
	{
		for (int i = 0; i < poolsize; i++) {
			for (int j = 0; j < prefab.Length; j++) {
				GameObject obj = (GameObject)Instantiate (prefab [j], new Vector3 (-20, -20, 0), Quaternion.identity);
				obj.transform.parent = transform;
				obj.SetActive (false);
				pool.Add (obj);
			}
		}
		if (logMessages) {
			print("Populated " + pool + " with " + pool.Count + "GameObjects!");
		}
	}

	private void ShufflePool (List<GameObject> pool)
	{
		System.Random rand = new System.Random ();
		int r = pool.Count;
		while (r > 1) {
			r--;
			int n = rand.Next (r + 1);
			GameObject temp = pool [n];
			pool [n] = pool [r];
			pool [r] = temp;
		}
		if (logMessages) {
			print("Shuffled List " + pool);
		}
	}

	private void CheckInactive (List<GameObject> pool, List<GameObject> activeObjs)
	{
		for (int i = 0; i < activeObjectA.Count; i++) {
			GameObject obj = activeObjectA [i];
			if (!obj.activeSelf) {
				pool.Add (obj);
				activeObjs.Remove (obj);
				if (logMessages) {
					print("Added Object back to Pool!");
				}
			}
		}
	}
}
