using System.Collections.Generic;
using UnityEngine;

public struct PlatformWalls
{
    public Transform left;
    public Transform right;
    public Transform top;
    public Transform bottom;
}

public class Platform : MonoBehaviour
{
    public PlatformWalls platformWalls;
    private List<bool> activeWalls = new List<bool>();
    public List<Transform> floorSpawnLocations;
    public List<Transform> wallSpawnLocations;

    private void Start()
    {
        GetActiveWallCount();
        SetupSpawnLocations();
    }

    public int GetActiveWallCount()
    {
        Transform left = transform.Find("Left");
        if (left != null && left.gameObject.activeSelf)
        {
            platformWalls.left = left;
            activeWalls.Add(platformWalls.left != null);
        }

        Transform right = transform.Find("Right");
        if (right != null && right.gameObject.activeSelf)
        {
            platformWalls.right = right;
            activeWalls.Add(platformWalls.right != null);
        }

        Transform top = transform.Find("Top");
        if (top != null && top.gameObject.activeSelf)
        {
            platformWalls.top = top;
            activeWalls.Add(platformWalls.top != null);
        }

        Transform bottom = transform.Find("Bottom");
        if (bottom != null && bottom.gameObject.activeSelf)
        {
            platformWalls.bottom = bottom;
            activeWalls.Add(platformWalls.bottom != null);
        }

        return activeWalls.Count;
    }

    public void SetupSpawnLocations()
    {
        if (platformWalls.left)
        {
            AddSpawnLocations(platformWalls.left, "WallSpawnLeft", "FloorSpawnLeft");
        }

        if (platformWalls.right)
        {
            AddSpawnLocations(platformWalls.right, "WallSpawnRight", "FloorSpawnRight");
        }

        if (platformWalls.top)
        {
            AddSpawnLocations(platformWalls.top, "WallSpawnTop", "FloorSpawnTop");
        }

        if (platformWalls.bottom)
        {
            AddSpawnLocations(platformWalls.bottom, "WallSpawnBottom", "FloorSpawnBottom");
        }
    }

    private void AddSpawnLocations(Transform wall, string wallName, string floorName)
    {
        Transform wallSpawn = wall.Find(wallName);
        Transform floorSpawn = wall.Find(floorName);

        if (wallSpawn != null && floorSpawn != null)
        {
            wallSpawnLocations.Add(wallSpawn);
            floorSpawnLocations.Add(floorSpawn);
        }
    }
}