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

	// Use this for initialization
	void Awake () {

        UpdateCurrentEnemy();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateCurrentEnemy();

        if (currEnemy != null)
        {
            enemyName.text = string.Format("{}: ", currEnemy.name);
            healthBarObj.transform.localScale = new Vector3((currEhs.GetHealth() / currEhs.maxHealth), 1, 1);
        }
    }

    void UpdateCurrentEnemy()
    {
        print("Updating Nearby Enemy");
        GameObject closestEnemy = null;
        float enemyDistance = float.MaxValue;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ProximityEnemy");

        foreach(var e in enemies)
        {
            if (e.GetComponent<EnemyHealthSystem>() != null) // has health system
            {
                float thisDistance = Vector3.Distance(player.transform.position, e.transform.position);
                print(string.Format("{} at distance {}", e.name, thisDistance));
                if(thisDistance < enemyDistance)
                {
                    closestEnemy = e;
                    enemyDistance = thisDistance;
                }
            }
        }

        if(enemyDistance <= currDistance)
        {
            currEnemy = closestEnemy;
            currDistance = enemyDistance;
            currEhs = closestEnemy.GetComponent<EnemyHealthSystem>();
        }

        // StartCoroutine(WaitForUpdate(0.5f));
        
    }

    IEnumerator WaitForUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateCurrentEnemy();
    }
}
