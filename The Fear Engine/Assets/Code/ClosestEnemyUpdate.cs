using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClosestEnemyUpdate : MonoBehaviour {
    public GameObject player;

    public GameObject enemyDisplay;
    public Text enemyName;
    public GameObject healthBarObj;

    private GameObject currEnemy = null;
    private float currDistance = float.MaxValue;
    private EnemyHealthSystem currEhs = null;

    private bool updateStarted = false;
	
	// Update is called once per frame
	void Update () {
        if (!updateStarted)
        {
            updateStarted = true;
            UpdateCurrentEnemy();
        }

        if (currDistance > 20.0f)
        {
            enemyDisplay.SetActive(false);
        }
        else
        {
            enemyDisplay.SetActive(true);

            // works
            print(currEnemy.name + " - closest Enemy: " + currDistance + " Health: " + currEhs.GetHealth() + "/" + currEhs.maxHealth);

            // refresh UI pieces
            enemyName.text = currEnemy.name + ": (" + ((int)currEhs.GetHealth()) + "/" + currEhs.maxHealth + ") ";
            healthBarObj.transform.localScale = new Vector3((currEhs.GetHealth() / currEhs.maxHealth), 1, 1);
        }


    }

    private void UpdateCurrentEnemy()
    {
        //print("Updating Nearby Enemy");
        
        GameObject closestEnemy = null;
        float enemyDistance = float.MaxValue;
        var enemies = GameObject.FindGameObjectsWithTag("ProximityEnemy");
        // print(enemies.Length);
        
        foreach(var e in enemies)
        {
            if (e.GetComponent<EnemyHealthSystem>() != null)
            {
                float thisDistance = Vector3.Distance(player.transform.position, e.transform.position);

                if (thisDistance < enemyDistance)
                {
                    closestEnemy = e;
                    enemyDistance = thisDistance;
                }
            }
            
        }


        currEnemy = closestEnemy;
        currDistance = enemyDistance;

        if (closestEnemy != null)
        {
            currEhs = closestEnemy.GetComponent<EnemyHealthSystem>();
        }
        else
        {
            currEhs = null;
        }  

        StartCoroutine(WaitForUpdate(1f));
        
    }

    IEnumerator WaitForUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateCurrentEnemy();
    }
}
