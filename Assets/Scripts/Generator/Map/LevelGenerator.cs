using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    List<GameObject> initializedCells;
    //Ide egy listaval celszerubb lesz majd behuzni mindent, akar kategoriakra bontva
    public GameObject torchPrefab;
    public GameObject chestPrefab;
    public GameObject chairPrefab;
    public GameObject exitPrefab;

    void Start()
    {
        StartCoroutine(WaitForMapGeneration());
    }

    IEnumerator WaitForMapGeneration()
    {
        yield return new WaitUntil(() => GameManager.Instance.GetInitializedCells().Count > 0);
        PopulateMaze();
    }

    public void PopulateMaze()
    {
        initializedCells = GameManager.Instance.GetInitializedCells();
        foreach (GameObject platform in initializedCells)
        {
            if (platform.CompareTag("ExitCell"))
            {
                Transform location = platform.GetComponent<Platform>().floorSpawnLocations[0];
                Instantiate(exitPrefab, location.position, exitPrefab.transform.rotation);
            }
            else
            {
                foreach (Transform location in platform.GetComponent<Platform>().floorSpawnLocations)
                {
                    if (SpawnWithChance(77))
                    {
                        Instantiate(chestPrefab, location.position, chestPrefab.transform.rotation);
                    }
                }
                foreach (Transform location in platform.GetComponent<Platform>().wallSpawnLocations)
                {
                    if (SpawnWithChance(88))
                    {
                        //Instantiate(torchPrefab, location.position, torchPrefab.transform.rotation);
                    }
                }
            }
        }
    }

    public bool SpawnWithChance(int chance)
    {
        return Random.Range(0, 100) >= chance ? true : false;
    }
}
