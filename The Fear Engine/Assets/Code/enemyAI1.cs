using UnityEngine;
using System.Collections;

//code found from this youtube video : https://www.youtube.com/watch?v=rG009vWqj74
//other snippets from me

public class enemyAI1 : MonoBehaviour {

    public Transform targetPlayer;
    public Transform myEnemy;

    public GameObject terrain;
    //public Transform terrain;
    public int sValue;
    private int moveSpeed = 3;
    private int rotationSpeed = 3;



    void Awake()
    {
        myEnemy = transform;
        //Physics.IgnoreLayerCollision(8, 0, true);
       // Physics.IgnoreCollision(GetComponent<Collider>(myEnemy), terrain.collider);
    }

    void Start()
    {
        targetPlayer = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        var lookDir = targetPlayer.position - transform.position;
        lookDir.y = 0;
        // zero the height difference
        myEnemy.rotation = Quaternion.Slerp(myEnemy.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        myEnemy.position += myEnemy.forward * moveSpeed * Time.deltaTime;
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
