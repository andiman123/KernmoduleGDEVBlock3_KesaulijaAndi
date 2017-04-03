using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

    public GameObject boid;
  

    public GameObject evade;

    public List <GameObject> Evade;
    public List <GameObject> Boids;

    public int maxEvade;
    public int maxBoid;

    public static Vector3 center;

	// Use this for initialization

    void OnEnable()
    {
        Boids = new List<GameObject>();
        for (int i = 0; i < maxBoid; i++)
        {
            Boids.Add((GameObject)Instantiate(boid, transform.position, Quaternion.Euler(Random.RandomRange(-360, 360), Random.RandomRange(-360, 360), Random.RandomRange(-360, 360))));
        }
        for (int i = 0; i < maxEvade; i++)
        {
            Evade.Add((GameObject)Instantiate(evade, new Vector3(Random.RandomRange(-20, 20), 30, Random.RandomRange(-20, 20)), Quaternion.identity));

        }

    }
	void Start () {



        //



    }
    void Update()
    {

    }
}
