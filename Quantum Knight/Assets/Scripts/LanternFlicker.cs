using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFlicker : MonoBehaviour {

    Light glow;
    float intensity;

	// Use this for initialization
	void Start () {
        glow = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        intensity = Random.Range(1f,1.25f);
        glow.intensity = intensity;
	}
}
