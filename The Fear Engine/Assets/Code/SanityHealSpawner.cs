using UnityEngine;
using System.Collections;

public class SanityHealSpawner : MonoBehaviour {
    public Transform player;
    public SanityManagerScript sanityManager;

    public GameObject corner1;
    public GameObject corner2;

    public GameObject sanityHealPrefab;
    public float spawningDelay = 8;

    public float spawnRadius = 30;

	void Awake()
    {
        SpawnHealer();
    }

    void SpawnHealer()
    {
        print("Spawning Regen Obj");

        Vector3 playerPos = player.transform.position;

        float xMin = Mathf.Min(corner1.transform.position.x, corner2.transform.position.x);
        float xMax = Mathf.Max(corner1.transform.position.x, corner2.transform.position.x);
        float zMin = Mathf.Min(corner1.transform.position.z, corner2.transform.position.z);
        float zMax = Mathf.Max(corner1.transform.position.z, corner2.transform.position.z);

        float xRand = Random.Range(playerPos.x - spawnRadius, playerPos.x + spawnRadius);
        float zRand = Random.Range(playerPos.z - spawnRadius, playerPos.z + spawnRadius);

        float xSpawn;
        float zSpawn;

        // find corrext X spawn
        if (xRand > xMax)
            xSpawn = xMax;
        else if (xRand < xMin)
            xSpawn = xMin;
        else
            xSpawn = xRand;

        // find correct Z spawn
        if (zRand > zMax)
            zSpawn = zMax;
        else if (zRand < zMin)
            zSpawn = zMin;
        else
            zSpawn = zRand;



        GameObject newObj = Instantiate(sanityHealPrefab, 
                                        new Vector3(xSpawn, 
                                                    transform.position.y, 
                                                    zSpawn), 
                                        transform.rotation) as GameObject;

        newObj.GetComponent<HealSanity>().healObject = newObj;
        newObj.GetComponent<HealSanity>().targetPlayer = player;
        newObj.GetComponent<HealSanity>().sanity = sanityManager;

        StartCoroutine(SpawnHealerWait(spawningDelay, newObj));
    }

    IEnumerator SpawnHealerWait(float waitTime, GameObject thisHealObj)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(thisHealObj);
        SpawnHealer();
    }
}
