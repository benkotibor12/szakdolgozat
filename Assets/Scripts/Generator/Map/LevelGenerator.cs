using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spawn
{
    Wall,
    Floor
}

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject torchPrefab;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private GameObject[] exitSwitchPrefabs;
    [SerializeField] private int exitObjectCount;

    [Range(0f, 100f)] [SerializeField] private float chestSpawnChance;
    [Range(0f, 100f)] [SerializeField] private float lightSpawnChance;

    private List<GameObject> initializedCells;
    private List<Platform> deadEndPlatforms;

    private void Start()
    {
        StartCoroutine(WaitForMapGeneration());
        exitObjectCount = exitSwitchPrefabs.Length;
    }

    private IEnumerator WaitForMapGeneration()
    {
        yield return new WaitUntil(() => GameManager.Instance.IsMapInitialized());
        initializedCells = GameManager.Instance.GetInitializedCells();
        deadEndPlatforms = new();
        FindDeadEnds();
        SetupExit();
        PopulateMaze();
    }

    private void FindDeadEnds()
    {
        foreach (GameObject cell in initializedCells)
        {
            Platform platform = cell.GetComponent<Platform>();
            if (IsPlatformDeadEnd(platform))
            {
                deadEndPlatforms.Add(platform);
            }
        }
    }

    private void SetupExit()
    {
        foreach (Platform platform in deadEndPlatforms)
        {
            if (platform.gameObject.CompareTag("ExitCell"))
            {
                SpawnDeadEndObject(exitPrefab, platform, Spawn.Floor);
            }
        }

        while (exitObjectCount > 0)
        {
            exitObjectCount -= 1;
            ExitSwitch currentExitobject = exitSwitchPrefabs[exitObjectCount].GetComponent<ExitSwitch>();
            currentExitobject.buttonId = exitObjectCount;
            SpawnDeadEndObject(exitSwitchPrefabs[exitObjectCount], deadEndPlatforms[Random.Range(0, deadEndPlatforms.Count)], Spawn.Wall);
        }
    }

    private bool IsPlatformDeadEnd(Platform platform)
    {
        return platform.GetActiveWallCount() == 3;
    }

    private void PopulateMaze()
    {
        foreach (GameObject cell in initializedCells)
        {
            Platform platform = cell.GetComponent<Platform>();
            List<Transform> floorSpawns = platform.floorSpawnLocations;
            List<Transform> wallSpawns = platform.wallSpawnLocations;

            if (SpawnWithChance(chestSpawnChance))
            {
                SpawnDeadEndObject(chestPrefab, platform, Spawn.Floor);
            }
            else if (SpawnWithChance(lightSpawnChance))
            {
                SpawnDeadEndObject(torchPrefab, platform, Spawn.Floor);
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

    private void SpawnDeadEndObject(GameObject prefab, Platform platform, Spawn spawn)
    {
        if (IsPlatformDeadEnd(platform))
        {
            string spawnTag = FindDeadEndObjectPlacement(platform);
            List<Transform> floorSpawnLocationsCopy = new List<Transform>(platform.floorSpawnLocations);
            List<Transform> wallSpawnLocationsCopy = new List<Transform>(platform.wallSpawnLocations);

            if (spawn == Spawn.Floor)
            {
                foreach (Transform floorSpawn in floorSpawnLocationsCopy)
                {
                    if (floorSpawn.CompareTag(spawnTag))
                    {
                        Instantiate(prefab, floorSpawn.position, floorSpawn.rotation * prefab.transform.rotation);
                        platform.floorSpawnLocations.Remove(floorSpawn);

                        // remove the corresponding wall spawn
                        foreach (Transform wallSpawn in wallSpawnLocationsCopy)
                        {
                            if (wallSpawn.CompareTag(spawnTag))
                            {
                                platform.wallSpawnLocations.Remove(wallSpawn);
                                break;
                            }
                        }
                    }
                }
            }
            else if (spawn == Spawn.Wall)
            {
                foreach (Transform wallSpawn in wallSpawnLocationsCopy)
                {
                    if (wallSpawn.CompareTag(spawnTag))
                    {
                        Instantiate(prefab, wallSpawn.position, wallSpawn.rotation * prefab.transform.rotation);
                        platform.wallSpawnLocations.Remove(wallSpawn);

                        // remove the corresponding floor spawn
                        foreach (Transform floorSpawn in floorSpawnLocationsCopy)
                        {
                            if (floorSpawn.CompareTag(spawnTag))
                            {
                                platform.floorSpawnLocations.Remove(floorSpawn);
                                break;
                            }
                        }
                    }
                }
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
