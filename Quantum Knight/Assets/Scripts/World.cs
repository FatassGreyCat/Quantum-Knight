using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    // Use this for initialization
    public List<GameObject> worldThings;
    public List<Vector2> objectLocations;
    int i;
    void Start () {
        Debug.Log("World object list contains " + worldThings.Capacity);
        for (i = 0; !(worldThings[i] == null);i++)
        {
            Instantiate(worldThings[i], objectLocations[i], Quaternion.identity);
            Debug.Log(worldThings[i].tag + " object " + i + " created");
        }

	}
	
	// Update is called once per frame
	void Update () {
	}
}
