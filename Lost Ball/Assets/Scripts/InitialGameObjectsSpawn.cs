using UnityEngine;
using System.Collections;

public class InitialGameObjectsSpawn : MonoBehaviour
{
	// public variables

	public float xMinRange = -4.0f;
	public float xMaxRange = 4.0f;
	public float yMinRange = 0.5f;
	public float yMaxRange = 0.5f;
	public float zMinRange = -50.0f;
	public float zMaxRange = 130.0f;
	public float numberOfEnemies = 20.0f;
	public float timeToPoplulateLevel = 3.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn

	//private variables
	private float nextSpawnTime;
	private float secondsBetweenSpawning;


	// init
	void Start ()
	{	// definicija vremena za instanciranje sljedećeg objekta (u koliko vremena se svi insanciraju)
		secondsBetweenSpawning = timeToPoplulateLevel / numberOfEnemies;
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	// this function is called once per frame
	void Update ()
	{
		//jesu li svi neprijatelji kreirani? ako nisu provjeri treba li novog
		if (numberOfEnemies>0){
		// je li vrijeme za generiranje novog neprijatelja
			if (Time.time >= nextSpawnTime) {
				// Spawn the game object through function below
				MakeThingToSpawn ();
				//reduce the number of enemies
				numberOfEnemies = numberOfEnemies - 1;

				// definicija sljedećeg termina generiranja objekta
				nextSpawnTime = Time.time + secondsBetweenSpawning;
			}
		}	
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		// randomly calculating the position in the specified boundaries
		spawnPosition.x = Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = Random.Range (yMinRange, yMaxRange);
		spawnPosition.z = Random.Range (zMinRange, zMaxRange);

		// generation of the objects according to randomly generated positions
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// instancing of the objects
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// placing the generated objects as the child of the spawner objects (maintains the hierarhcy clean)
		spawnedObject.transform.parent = gameObject.transform;
	}
}
