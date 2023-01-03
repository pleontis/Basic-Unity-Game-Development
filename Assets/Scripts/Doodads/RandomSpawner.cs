using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject townHall,Barracks,empty;
    public GameObject[] myObjects;
    public int maxTrees;
    public int maxRocks;
    private int objectIndex = 0;
    private int treeCounter=0, rockCounter=0;
    private static RandomSpawner _instance;
    public static RandomSpawner Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool same = false;
        Vector3 randomSpawnPos = new Vector3(Random.Range(-45, 45), 2, Random.Range(-45, 45));
        do
        {
            foreach (GameObject myObject in myObjects)
            {
                same = false;
            
                float myObjectDistance, townHallDistance,barracksDistance;
                myObjectDistance = Vector3.Distance(randomSpawnPos, myObject.transform.position);
                townHallDistance = Vector3.Distance(randomSpawnPos, townHall.transform.position);
                barracksDistance = Vector3.Distance(randomSpawnPos, Barracks.transform.position);
                //Object is about to be spwaned on same position with another one
                if (myObjectDistance < 10f || townHallDistance < 7f || barracksDistance<7f)
                {
                    //Calculate a new Random position for spawning and re-check
                    randomSpawnPos = new Vector3(Random.Range(-45, 45), 2, Random.Range(-45, 45));
                    same = true;
                    break;
                }
            }
        } while (same);

        //Preference of the user for the maximum number of trees into map
        if (treeCounter<maxTrees || rockCounter < maxRocks)
        {
            if (myObjects[objectIndex].transform.GetComponent<ResourceSrc>().type.Equals(ResourceType.Wood))
            {
                Instantiate(myObjects[objectIndex], randomSpawnPos, Quaternion.identity);
                treeCounter++;
                
            }
            if (myObjects[objectIndex].transform.GetComponent<ResourceSrc>().type.Equals(ResourceType.Rock))
            {
                randomSpawnPos.y = -1.2f;
                Instantiate(myObjects[objectIndex], randomSpawnPos, Quaternion.identity);
                rockCounter++;
            }
            objectIndex++;
        }
    }
}
