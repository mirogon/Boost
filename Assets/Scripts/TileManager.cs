using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour 
{
	public GameObject[] tilePrefabs;

	private float tileLength = 19.76f;
    private int maxTiles = 5;
	private List<GameObject> activeTiles;
    private Transform playerTransform;


	// Use this for initialization
	private void Start ()
    {

        activeTiles = new List<GameObject> ();
        activeTiles.Add(Instantiate(tilePrefabs[0]));
        activeTiles[activeTiles.Count - 1].transform.SetParent(GameObject.FindGameObjectWithTag("TileManager").transform);
        activeTiles[0].transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	private void Update ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    
        //Debug.unityLogger.Log("Player Z: ", playerTransform.position.z);
        //Debug.unityLogger.Log("Tile Z: ", activeTiles[0].transform.position.z);
        if (playerTransform.position.z > activeTiles[0].transform.position.z + tileLength + 10)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
        
        while (activeTiles.Count < maxTiles)
        {
            SpawnTile();
            
        }
	}

	private void SpawnTile(int prefabIndex = 0) 
	{
        activeTiles.Add(Instantiate(tilePrefabs[prefabIndex]));
        activeTiles[activeTiles.Count - 1].transform.SetParent(GameObject.FindGameObjectWithTag("TileManager").transform);
        activeTiles[activeTiles.Count - 1].transform.SetPositionAndRotation(new Vector3(0, 0, tileLength + activeTiles[activeTiles.Count -2].transform.position.z ), Quaternion.identity);

    }



}
