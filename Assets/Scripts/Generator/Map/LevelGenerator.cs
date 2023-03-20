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

    private bool called;

    void Start()
    {
        called = false;
    }

    private void Update()
    {
        if (!called)
        {
            called = true;
            PopulateMaze();
        }
    }

    public void PopulateMaze()
    {
        initializedCells = GameManager.Instance.GetInitializedCells();
        foreach (GameObject platform in initializedCells)
        {
            if (platform.CompareTag("ExitCell"))
            {
                Vector3 location = platform.GetComponent<Platform>().transform.position;
                Instantiate(exitPrefab, location, exitPrefab.transform.rotation);
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
