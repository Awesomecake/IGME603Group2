using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSpawner : MonoBehaviour
{
    [SerializeField] GameObject wheatPrefab;
    [SerializeField] int wheatDensity; //Number of wheat resources to spawn per area

    //Minimum and maximum bounds for area to spawn wheat in. 
    //0 -> minimum, 1 -> maximum
    [SerializeField] Transform[] areaOneBounds;
    [SerializeField] Transform[] areaTwoBounds;
    [SerializeField] Transform[] areaThreeBounds;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWheat(wheatDensity, areaOneBounds);
        SpawnWheat(wheatDensity, areaTwoBounds);
        SpawnWheat(wheatDensity, areaThreeBounds);
    }

    void SpawnWheat(int numberToSpawn, Transform[] areaBounds)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(areaBounds[0].position.x, areaBounds[1].position.x),
                Random.Range(areaBounds[0].position.y, areaBounds[1].position.y)
                );

            Instantiate(wheatPrefab, randomPosition, Quaternion.identity, areaBounds[0].parent);
        }
    }
}
