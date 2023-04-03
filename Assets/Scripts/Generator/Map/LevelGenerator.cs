using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private GameObject exitPrefab;

    [Range(0f, 100f)] [SerializeField] private float chestSpawnChance;
    [Range(0f, 100f)] [SerializeField] private float lightSpawnChance;

    private List<GameObject> initializedCells;

    private void Start()
    {
        StartCoroutine(WaitForMapGeneration());
    }

    private IEnumerator WaitForMapGeneration()
    {
        yield return new WaitUntil(() => GameManager.Instance.IsMapInitialized());
        initializedCells = GameManager.Instance.GetInitializedCells();
        PopulateMaze();
    }

    private void PopulateMaze()
    {
        foreach (GameObject platform in initializedCells)
        {
            Platform platformComponent = platform.GetComponent<Platform>();
            List<Transform> floorSpawns = platformComponent.floorSpawnLocations;
            List<Transform> wallSpawns = platformComponent.wallSpawnLocations;

            if (platform.CompareTag("ExitCell"))
            {
                SpawnDeadEndObject(exitPrefab, platformComponent);
            }
            else
            {
                if (floorSpawns.Count == 3 && wallSpawns.Count == 3)
                {
                    if (SpawnWithChance(chestSpawnChance))
                    {
                        SpawnDeadEndObject(chestPrefab, platformComponent);
                    }
                    else if (SpawnWithChance(lightSpawnChance))
                    {
                        SpawnDeadEndObject(torchPrefab, platformComponent);
                    }
                }
                else
                {
                    foreach (Transform location in floorSpawns)
                    {
                        if (SpawnWithChance(chestSpawnChance))
                        {
                            //Instantiate(chestPrefab, location.position, location.rotation);
                        }
                    }
                    foreach (Transform location in wallSpawns)
                    {
                        if (SpawnWithChance(lightSpawnChance))
                        {
                            //Instantiate(torchPrefab, location.position, location.rotation);
                        }
                    }
                }
            }
        }
    }

    private void SpawnDeadEndObject(GameObject prefab, Platform platform)
    {
        string spawnTag = FindDeadEndObjectPlacement(platform);

        foreach (Transform floorSpawn in platform.floorSpawnLocations)
        {
            if (floorSpawn.CompareTag(spawnTag))
            {
                Instantiate(prefab, floorSpawn.position, floorSpawn.rotation * prefab.transform.rotation);
            }
        }
    }

    private string FindDeadEndObjectPlacement(Platform platform)
    {
        if (!platform.left.gameObject.activeSelf)
        {
            return "RightSpawn";
        }
        if (!platform.right.gameObject.activeSelf)
        {
            return "LeftSpawn";
        }
        if (!platform.top.gameObject.activeSelf)
        {
            return "BottomSpawn";
        }
        if (!platform.bottom.gameObject.activeSelf)
        {
            return "TopSpawn";
        }
        return "";
    }

    private bool SpawnWithChance(float chance)
    {
        return Random.Range(0, 100) <= chance;
    }
}
