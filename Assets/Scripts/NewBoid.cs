using UnityEngine;
using System.Collections;

public class NewBoid : MonoBehaviour {

    private Rigidbody rig;
    private Vector3 v;

    private Vector3 dir;
    private int neighborCount;
    private int evadeCount;

    private float speed = 1;
    private float maxSpeed = 5;
    private float rotateSpeed = 0.1f;

    public GameObject target;

    public GameObject[] Boids;
    public GameObject[] Evade;
    private float distance = 1;

    // Use this for initialization

    void OnEnable(){

            rig.velocity = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        }
    void Start () {

        rig = GetComponent<Rigidbody>();
        

        Evade = GameObject.FindGameObjectsWithTag("Evade");
        Boids = GameObject.FindGameObjectsWithTag("Boid");
       
        

    }
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.LookRotation(rig.velocity);

        Vector3 alignment = computeAlignment();
        Vector3 cohesion = computeCohesion();
        Vector3 separation = computeSeparation();

        Vector3 evade = computeEvade();

        rig.velocity += alignment + cohesion + separation + evade;
        rig.velocity = Vector3.ClampMagnitude(rig.velocity, maxSpeed);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            rig.velocity = target.transform.position;
        }
	}

    public Vector3 computeAlignment()
    {
        foreach (GameObject boid in Boids)
        {

            if (Vector3.Distance(boid.transform.position, transform.position) < distance)
            {
                

                v.x += boid.GetComponent<Rigidbody>().velocity.x;
                //v.y += boid.GetComponent<Rigidbody>().velocity.y;
                v.z += boid.GetComponent<Rigidbody>().velocity.z;
                neighborCount++;

            }
        }

        if (neighborCount == 0)
        {
            return v;
        }
            
        v.x /= neighborCount;
        //v.y /= neighborCount;
        v.z /= neighborCount;
        v.Normalize();
        return v;

    }

    public Vector3 computeCohesion()
    {
        Vector3 c;
        foreach (GameObject boid in Boids)
        {

            if (Vector3.Distance(boid.transform.position, transform.position) < distance)
            {
                v.x += boid.transform.position.x *20;
                //v.y += boid.transform.position.y*20;
                v.z += boid.transform.position.z*20;
                


            }
        }

        if (neighborCount == 0)
        {
            return v;
        }

        v.x /= neighborCount;
        //v.y /= neighborCount;
        v.z /= neighborCount;
        //c = new Vector3(v.x - transform.position.x, v.y - transform.position.y, v.z - transform.position.z);
        c = new Vector3(v.x - transform.position.x, 0, v.z - transform.position.z);
        c.Normalize();
        return c;

    }

    public Vector3 computeSeparation()
    {
        foreach (GameObject boid in Boids)
        {

            if (Vector3.Distance(boid.transform.position, transform.position) < distance)
            {
                v.x += boid.transform.position.x - transform.position.x;
                //v.y += boid.transform.position.y - transform.position.y;
                v.z += boid.transform.position.z - transform.position.z;
                


            }
        }

        if (neighborCount == 0)
        {
            return v;
        }

        v.x /= neighborCount;
        //v.y /= neighborCount;
        v.z /= neighborCount;

        v.x *= -0.5f;
        //v.y *= -0.5f;
        v.z *= -0.5f;

        v.Normalize();
        return v;

    }

    public Vector3 computeEvade()
    {
        
        foreach (GameObject evade in Evade)
        {

            if (Vector3.Distance(evade.transform.position, transform.position) < 2)
            {
                Debug.DrawLine(transform.position, evade.transform.position);

                v.x += evade.transform.position.x - transform.position.x;
                //v.y += evade.transform.position.y - transform.position.y;
                v.z += evade.transform.position.z - transform.position.z;
                evadeCount++;


            }
        }

        if (evadeCount == 0)
        {
            return v;
        }

        v.x /= evadeCount;
        //v.y /= evadeCount;
        v.z /= evadeCount;

        v.x *= -1;
        //v.y *= -1;
        v.z *= -1;

        v.Normalize();
        return v;

    }


}
