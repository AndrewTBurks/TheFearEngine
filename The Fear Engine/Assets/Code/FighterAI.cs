using UnityEngine;
using System.Collections;


/*
    Written by: Andrew Burks

    This AI has actions it can perform, with the probabilities of 
    choosing those actiosn defined by the probTable given. This is an example
    of a "Bayesian Network" AI concept, or more specifically behavior defined by
    a k-th order Markov Model where, for this implementation, we use k = 2.
    The first two indices of the probTable will be picking the last 2 actions,
    and the third index is the probability of each next action to be performed.

    [0, 0, x] := initial action
    [0, x, y] := second action

    [x, y, z] := probability of z given x and y previous actions

    Action 0: None (First few actions performed)
    Action 1: Normal attack
    Action 2: Slow Regenerate
    Action 3: SPLIT! (Split off 3(?) small copies that live for a few seconds)
*/

public class FighterAI : MonoBehaviour
{
    public GameObject regenEffect;
    private GameObject myRegenEffect;

    public GameObject clonePrefab; // prefab for clones of this monster
    public bool spawnMobs = false;

    private float[,,] probTable;

    private int prevMov1; // last move
    private int prevMov2; // second to last move

    // Action 1 will have the remaining probability to fill total to 1.
    private float pMaxAct1 = 0.8f;
    /*    NOTE:   pAct1 = 1 - (pMaxAct2 + pMaxAct3) when randomized*/
    private float pMaxAct2 = 0.27f;
    private float pMaxAct3 = 0.20f;

    // Minimum probabilities for actions
    private float pMinAct1 = 0.53f;
    private float pMinAct2 = 0.10f;
    private float pMinAct3 = 0.10f;


    // Min values for actions to scale by probability
    private float vMinAct1 = 5;
    private float vMinAct2 = 20;
    private float vMinAct3 = 2;

    // Max bonus amount for low probability of the action
    private float vBonusAct1 = 5;
    private float vBonusAct2 = 10;
    private float vBonusAct3 = 2;


    // Use this for initialization
    void Awake()
    {
        probTable = new float[4, 4, 4];
        // initialize table

        InitProbTable();

        prevMov1 = 0;
        prevMov2 = 0;

        // create the particle system for the regen
        myRegenEffect = Instantiate(regenEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        myRegenEffect.transform.parent = gameObject.transform;
        myRegenEffect.SetActive(false); // hidden to begin with

        // turn off autoShoot in basic enemyAI1 script
        GetComponent<enemyAI1>().autoShoot = false;

        // pick a move from the start, after wait times, subsequent moves picked
        pickMoveAndPerform();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        // unused
    }
    */

    /* Initialize the probability table to a bounded range for each action */
    private void InitProbTable()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                // lower probability of action 2 if it is in the last two
                if (j == 2 || i == 2) // 1/4 probability for Action 2
                {
                    probTable[i, j, 2] = Random.Range(pMinAct2 / 2, pMaxAct2 / 2);
                }
                else // normal probability
                {
                    probTable[i, j, 2] = Random.Range(pMinAct2, pMaxAct2);
                }

                // Lower probability of action 3 if it is in the last two
                if (j == 3 || i == 3) // 1/4 probability for Action 3
                {
                    probTable[i, j, 3] = Random.Range(pMinAct3 / 2, pMaxAct3 / 2);
                }
                else // normal probability
                {
                    probTable[i, j, 3] = Random.Range(pMinAct3, pMaxAct3);
                }

                probTable[i, j, 0] = 0; // not an action
                probTable[i, j, 1] = 1 - (probTable[i, j, 2] + probTable[i, j, 3]);
            }
        }
    }

    private void pickMoveAndPerform()
    {
        float p1 = probTable[prevMov2, prevMov1, 1];
        float p2 = probTable[prevMov2, prevMov1, 2];
        float p3 = probTable[prevMov2, prevMov1, 3];

        float randGen = Random.Range(0.0f, 1.0f);

        if (randGen <= p1)
        {
            StartCoroutine(Attack(p1));
        }
        else if (randGen <= (p1 + p2))
        {
            StartCoroutine(SlowRegen(p2));
        }
        else
        {
            StartCoroutine(SplitEnemy(p3));
        }

        
    }

    /* ====================================== */
    /* ACTION 1: Perform a basic melee attack */
    IEnumerator Attack(float prob)
    {
        print("In Attack");

        // prob := probability of move 1 occurring given previous two

        // advance move history
        prevMov2 = prevMov1;
        prevMov1 = 1;

        // scales by probability
        float scaleFactor = (prob - pMinAct1) / (pMaxAct1 - pMinAct1);
        // smaller scale factor => higher bonus for rarity
        float damage = vMinAct1 + (vBonusAct1 * (1 - scaleFactor));

        /* ========= DO ATTACK ========== */
        gameObject.GetComponent<enemyAI1>().Shoot();
        
        yield return new WaitForSeconds(1);

        pickMoveAndPerform(); // Choose next move after return
    }

    /* ====================================================================== */
    /* ACTION 2: Cast a regen spell on themselves, healing for a small amount */
    IEnumerator SlowRegen(float prob)
    {
        print("In Regen");
        myRegenEffect.SetActive(true);
        // prob := probability of move 2 occurring given previous two

        // advance move history
        prevMov2 = prevMov1;
        prevMov1 = 2;

        // scales by probability
        float scaleFactor = (prob - pMinAct2) / (pMaxAct2 - pMinAct2);
        // smaller scale factor => higher bonus for rarity
        float regenAmt = vMinAct2 + (vBonusAct2 * (1 - scaleFactor));

        float increment = regenAmt / 30;

        EnemyHealthSystem myHPSystem = gameObject.GetComponent<EnemyHealthSystem>();

        for (float i = 0f; i < regenAmt; i += increment)
        {
            /* ========= DO REGEN ========== */
            yield return new WaitForSeconds(0.1f);

            myHPSystem.AddHealth(increment);
        }

        myRegenEffect.SetActive(false);
        pickMoveAndPerform(); // Choose next move after return
    }

    /* ========================================================= */
    /* ACTION 3: Create numEnemies small copies of current enemy */
    IEnumerator SplitEnemy(float prob)
    {
        if (spawnMobs)
        {
            print("In Split");
            // prob := probability of move 3 occurring given the previous two

            // advance move history
            prevMov2 = prevMov1;
            prevMov1 = 3;

            // scales by probability
            float scaleFactor = (prob - pMinAct3) / (pMaxAct3 - pMinAct3);
            // smaller scale factor => higher bonus for rarity
            int numEnemies = (int)Mathf.Floor(vMinAct3 + (vBonusAct3 * (1 - scaleFactor))); // floor since it can't spawn partial enemies..

            /* ========= DO SPLIT ========== */

            Vector3 newChildVector;

            for (int i = 0; i < numEnemies; i++)
            {
                newChildVector = new Vector3(5, 0, 0);
                newChildVector = Quaternion.Euler(0, i * (360 / numEnemies + 1), 0) * newChildVector;
                GameObject newChild = Instantiate(clonePrefab, transform.position + newChildVector, Quaternion.identity) as GameObject;
                newChild.transform.localScale *= 0.4f;
                // temporary enemy with timeout script
                newChild.AddComponent<EnemyTimeout>();
            }

            yield return new WaitForSeconds(5);
            pickMoveAndPerform(); // Choose next move after return
        }
    }

}

