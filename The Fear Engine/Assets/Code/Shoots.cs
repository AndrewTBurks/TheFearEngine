using UnityEngine;
using System.Collections;

public class Shoots : MonoBehaviour
{
    public Rigidbody web;
    public Transform targetPlayer;
    public Transform myEnemy;
    public float bulletImpulse = 20.0f;
    public GameObject terrain;
    //public Transform terrain;
    private bool onRange;
    private float aggroRange = 30;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float seperation = Vector3.Distance(this.transform.position, targetPlayer.transform.position);
        if (seperation <= aggroRange)
        {
            onRange = true;
            Shoot();
        }
    }
    void Shoot()
    {

        if (onRange)
        {

            //Rigidbody projectile = (Rigidbody)Instantiate(web, transform.position + transform.forward, transform.rotation);
            Rigidbody project = Instantiate(web) as Rigidbody;
            project.transform.position = transform.position + transform.forward;
            project.velocity = this.transform.forward * 40;
            //web.AddForce(transform.forward * bulletImpulse, ForceMode.Impulse);

            //Destroy(web.gameObject, 2);
            onRange = false;
        }
    }
}
