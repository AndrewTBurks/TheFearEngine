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

public class FighterAI : MonoBehaviour {
    
    private float[, ,] probTable;

    private float prevMov1;
    private float prevMov2;

    // Action 1 will have the remaining probability to fill total to 1.
    /*    NOTE:   pAct1 = 1 - (pMaxAct2 + pMaxAct3) */
    private float pMaxAct2 = 0.27f;
    private float pMaxAct3 = 0.20f;


	// Use this for initialization
	void Awake ()
    {
        probTable = new float[4, 4, 4];
        // initialize table

        prevMov1 = 0;
        prevMov2 = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    /* Initialize the probability table to a bounded range for each action */
    private void InitProbTable()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                probTable[i, j, 0] = 0; // not an action
                probTable[i, j, 2] = Random.Range(0.1f, pMaxAct2);
                probTable[i, j, 3] = Random.Range(0.1f, pMaxAct3);
                probTable[i, j, 1] = 1 - (probTable[i, j, 2] + probTable[i, j, 3]);
            }
        }
    }

    /* ACTION 1: Perform a basic melee attack */
    private void Attack()
    {
        // advance move history
        prevMov2 = prevMov1;
        prevMov1 = 1;

        float damage; // scales by probability
    }

    /* ACTION 2: Cast a regen spell on themselves, healing for a small amount */
    private void SlowRegen()
    {
        // advance move history
        prevMov2 = prevMov1;
        prevMov1 = 1;
        float regenAmt; // scales by probability
    }

    /* ACTION 3: Create numEnemies small copies of current enemy */
    private void SplitEnemy()
    {
        // advance move history
        prevMov2 = prevMov1;
        prevMov1 = 1;
        int numEnemies; // scales by probability
    }

}
