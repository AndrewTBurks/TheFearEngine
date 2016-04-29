using UnityEngine;
using System.Collections;

public class SanityHealSpawner : MonoBehaviour {
    public Transform player;
    public SanityManagerScript sanityManager;

    public GameObject corner1;
    public GameObject corner2;

    public GameObject sanityHealPrefab;
    public float spawningDelay = 8;
    public float despawnDelay = 10;

	void Awake()
    {
        SpawnHealer();
    }

    void SpawnHealer()
    {
        print("Spawning Regen Obj");

        Vector3 range = corner2.transform.position - corner1.transform.position;
        float xRange = range.x;
        float zRange = range.z;

        GameObject newObj = Instantiate(sanityHealPrefab, 
                                        new Vector3(corner1.transform.position.x + Random.Range(0.0f, xRange), 
                                                    transform.position.y, 
                                                    corner1.transform.position.z + Random.Range(0.0f, zRange)), 
                                        transform.rotation) as GameObject;

        newObj.AddComponent<EnemyTimeout>();
        newObj.GetComponent<EnemyTimeout>().waitTime = despawnDelay;
        newObj.GetComponent<HealSanity>().healObject = newObj;
        newObj.GetComponent<HealSanity>().targetPlayer = player;
        newObj.GetComponent<HealSanity>().sanity = sanityManager;

        StartCoroutine(SpawnHealerWait(spawningDelay));
    }

    IEnumerator SpawnHealerWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnHealer();
    }
}
