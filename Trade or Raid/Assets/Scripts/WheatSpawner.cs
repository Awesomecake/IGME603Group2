using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class WheatSpawner : MonoBehaviour
{
    [SerializeField] GameObject wheatPrefab;
    [SerializeField] public int wheatDensity; //Number of wheat resources to spawn per area
    [SerializeField] List<int> wheatDensityModifiers;

    //Minimum and maximum bounds for area to spawn wheat in. 
    //0 -> minimum, 1 -> maximum
    [SerializeField] Transform[] areaOneBounds;
    [SerializeField] Transform[] areaTwoBounds;
    [SerializeField] Transform[] areaThreeBounds;

    [SerializeField] List<Resource> spawnedWheat;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnWheat(wheatDensity, areaOneBounds);
        //SpawnWheat(wheatDensity, areaTwoBounds);
        //SpawnWheat(wheatDensity, areaThreeBounds);
    }

    void SpawnWheat(int numberToSpawn, Transform[] areaBounds)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(areaBounds[0].position.x, areaBounds[1].position.x),
                Random.Range(areaBounds[0].position.y, areaBounds[1].position.y)
                );

            Resource newWheat = Instantiate(wheatPrefab, randomPosition, Quaternion.identity, areaBounds[0].parent).GetComponent<Resource>();
            spawnedWheat.Add(newWheat);
        }
    }

    public void NewDay()
    {
        if (wheatDensity > 10)
            wheatDensity -= 15;

        if (wheatDensity < 10)
        {
            wheatDensity = 10;
        }

        //Generate random modifiers such that they balance each other out
        wheatDensityModifiers = new List<int>();
        wheatDensityModifiers.Add(Random.Range(-2, 2));
        wheatDensityModifiers.Add(Random.Range(-2, 2));
        wheatDensityModifiers.Add(-wheatDensityModifiers[0] - wheatDensityModifiers[1]);

        List<int> wheatDensityModifiersCopy = new List<int>(wheatDensityModifiers);

        int randomIndex = Random.Range(0, wheatDensityModifiersCopy.Count);
        int wheatDensityModifier = wheatDensityModifiersCopy[randomIndex];

        SpawnWheat(wheatDensity + wheatDensityModifier * 10, areaOneBounds);
        wheatDensityModifiersCopy.RemoveAt(randomIndex);
        DisplayRandomEventMessage(wheatDensityModifier, 0);

        randomIndex = Random.Range(0, wheatDensityModifiersCopy.Count);
        wheatDensityModifier = wheatDensityModifiersCopy[randomIndex];
        SpawnWheat(wheatDensity + wheatDensityModifier * 10, areaTwoBounds);
        wheatDensityModifiersCopy.RemoveAt(randomIndex);
        DisplayRandomEventMessage(wheatDensityModifier, 1);

        wheatDensityModifier = wheatDensityModifiersCopy[0];
        SpawnWheat(wheatDensity + wheatDensityModifier * 10, areaThreeBounds);
        DisplayRandomEventMessage(wheatDensityModifier, 2);
    }

    private void DisplayRandomEventMessage(int wheatModifier, int playerID)
    {
        switch (wheatModifier)
        {
            case -2:
                Debug.Log("Great famine! Crop yeild drastically reduced for kingdom " + playerID + "!");
                break;
            case -1:
                Debug.Log("Famine! Crop yeild reduced for kingdom " + playerID + "!");
                break;
            case 0:
                Debug.Log("Normal crop yield for kingdom " + playerID + "!");
                break;
            case 1:
                Debug.Log("Bounty! Crop yield increased for kingdom " + playerID + "!");
                break;
            case 2:
                Debug.Log("Great bounty! Crop yeild abundantly increased for kingdom " + playerID + "!");
                break;
            default:
                Debug.LogWarning("Oops! This wasn't supposed to happen...");
                break;
        }

        if (wheatDensity - wheatModifier * 10 <= 0)
        {
            Debug.Log("Oh no! Your kingdom has no crops to harvest! What will you do...?");
        }
    }

    public void DespawnAllWheat()
    {
        foreach (var wheat in spawnedWheat)
        {
            Destroy(wheat.gameObject);
        }

        spawnedWheat.Clear();
    }
}
