using UnityEngine;
using System.Collections;

//code found from this youtube video : https://www.youtube.com/watch?v=rG009vWqj74
//other snippets from me

public class enemyAI1 : MonoBehaviour {

    public GameObject web;
    public Transform targetPlayer;
    public Transform myEnemy;
    public float bulletImpulse = 20.0f;
    public GameObject terrain;
    //public Transform terrain;
    public int sValue;
    private int moveSpeed = 3;
    private int rotationSpeed = 3;
    private bool onRange;
    private float aggroRange = 30;
    private float waitingTime = 5;
    private float timer;

    public bool autoShoot; // to turn off auto shooting behavior in Update() when Shoot() is used in FighterAI

    void Awake()
    {
        myEnemy = transform;
        //Physics.IgnoreLayerCollision(8, 0, true);
        // Physics.IgnoreCollision(GetComponent<Collider>(myEnemy), terrain.collider);

        targetPlayer = GameObject.FindWithTag("Player").transform;
        autoShoot = true;
    }

    void Update()
    {
        var lookDir = targetPlayer.position - transform.position;
        lookDir.y = 0;
        // zero the height difference
        myEnemy.rotation = Quaternion.Slerp(myEnemy.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        myEnemy.position += myEnemy.forward * moveSpeed * Time.deltaTime;
        float seperation = Vector3.Distance(this.transform.position, targetPlayer.transform.position);
        timer += Time.deltaTime;
        if (seperation <= aggroRange && timer > waitingTime && autoShoot)
        {
            onRange = true;
            Shoot();
            timer = 0;
        }
    }

    public void Shoot()
    {

        if (onRange)
        {

            //Rigidbody projectile = (Rigidbody)Instantiate(web, transform.position + transform.forward, transform.rotation);
            GameObject project = Instantiate(web) as GameObject;
            project.transform.position = transform.position + transform.forward * 3f;
            Rigidbody rb = project.GetComponent<Rigidbody>();
            rb.velocity = this.transform.forward * 30 + Vector3.up * 5f;
            //web.AddForce(transform.forward * bulletImpulse, ForceMode.Impulse);

            //Destroy(web.gameObject, 2);
            onRange = false;
        }

        /*void OnCollisionExit(Collision collisionInfo)
        {
            //changeScore();
            scorePoints.score += sValue;
        }*/
        /* void changeScore()
         {

             scorePoints.score += sValue;
         }*/



    }
}
