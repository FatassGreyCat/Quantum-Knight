using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private Transform bar;
    float move;
    public GameObject qk;
    public float energyLevel = 0.5f;
    // Use this for initialization
    private void Start () {
        bar = GameObject.Find("BarSprite").transform;
        bar.localScale = new Vector3(.5f,1f);
    }
	
	// Update is called once per frame
	void Update()
    {
        energyLevel = GameObject.Find("QuantumKnight").GetComponent<Run>().energy;
        if (energyLevel>0)
        {
            bar.localScale = new Vector3(energyLevel, 1, 1);
        }

    }

}
