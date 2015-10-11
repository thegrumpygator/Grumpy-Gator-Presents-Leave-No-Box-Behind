using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
    public float xMinDeadRange = 0f; // dead zone to not spawn enemies BLF 20151011
	public float xMaxRange = 25.0f;
    public float xMaxDeadRange = 0f; // dead zone to not spawn enemies BLF 20151011
    public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
    public float zMinDeadRange = 0f; // dead zone to not spawn enemies BLF 20151011
    public float zMaxRange = 25.0f;
    public float zMaxDeadRange = 0f; // dead zone to not spawn enemies BLF 20151011
    public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;

	// Use this for initialization
	void Start ()
	{
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	
	}

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

        // get a random position between the specified ranges
        // Account for dead-zone BLF 20151011

        // Choose x or z priority (if x is pri, any z, if z is pri, any x
        int iXZPriChoice = Random.Range(0, 2);
        if (iXZPriChoice == 0) // x pri
        {
            // make binary choice between max side or min side for x
            int iSide = Random.Range(0, 2);
            if (iSide == 0)
            {
                // you are on min side
                spawnPosition.x = Random.Range(xMinDeadRange, xMinRange);
            }
            else
            {
                // you are on max side
                spawnPosition.x = Random.Range(xMaxDeadRange, xMaxRange);
            }
            // Choose any z
            spawnPosition.z = Random.Range(zMinRange, zMaxRange);
        }
        else // z pri
        {
            // make binary choice between max or min side for z
            int iSide = Random.Range(0, 2);
            if (iSide == 0)
            {
                // you are on min side
                spawnPosition.z = Random.Range(zMinDeadRange, zMinRange);
            }
            else
            {
                // you are on max side
                spawnPosition.z = Random.Range(zMaxDeadRange, zMaxRange);
            }
            // choose any x
            spawnPosition.x = Random.Range(xMinRange, xMaxRange);

        }



        // choose random x location on selected side
        //spawnPosition.x = Random.Range(xMinRange, xMaxRange);

        spawnPosition.y = Random.Range (yMinRange, yMaxRange);




        // choose random z location on selected side
        //spawnPosition.z = Random.Range (zMinRange, zMaxRange);

		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;
	}
}
