using System.Collections.Generic;
using UnityEngine;

public enum WallName
{
    Left,
    Right,
    Bottom,
    Top
}

public class Platform : MonoBehaviour
{
    public List<Transform> floorSpawnLocations;
    public List<Transform> wallSpawnLocations;
    public Transform left, right, bottom, top;
    public Transform leftGate, rightGate, bottomGate, topGate;

    private void Start()
    {
        FindWallsAndGates();
        HideGates();
        GetActiveWallCount();
        SetupSpawnLocations();
        AddPillars();
    }

    private void FindWallsAndGates()
    {
        left = transform.Find("Left");
        right = transform.Find("Right");
        bottom = transform.Find("Bottom");
        top = transform.Find("Top");
        leftGate = transform.Find("LeftGate");
        rightGate = transform.Find("RightGate");
        bottomGate = transform.Find("BottomGate");
        topGate = transform.Find("TopGate");
    }

    public int GetActiveWallCount()
    {
        int counter = 0;
        if (left != null && left.gameObject.activeSelf)
        {
            counter++;
        }

        if (right != null && right.gameObject.activeSelf)
        {
            counter++;
        }

        if (top != null && top.gameObject.activeSelf)
        {
            counter++;
        }

        if (bottom != null && bottom.gameObject.activeSelf)
        {
            counter++;
        }

        return counter;
    }

    private void SetupSpawnLocations()
    {
        AddSpawnLocations(left, "WallSpawnLeft", "FloorSpawnLeft");
        AddSpawnLocations(right, "WallSpawnRight", "FloorSpawnRight");
        AddSpawnLocations(top, "WallSpawnTop", "FloorSpawnTop");
        AddSpawnLocations(bottom, "WallSpawnBottom", "FloorSpawnBottom");
    }

    private void AddSpawnLocations(Transform wall, string wallSpawnName, string floorSpawnName)
    {
        if (wall.gameObject.activeSelf)
        {
            Transform wallSpawn = wall.Find(wallSpawnName);
            Transform floorSpawn = wall.Find(floorSpawnName);

            if (wallSpawn != null && floorSpawn != null)
            {
                wallSpawnLocations.Add(wallSpawn);
                floorSpawnLocations.Add(floorSpawn);
            }
        }
    }

    private bool IsWallActive(WallName name)
    {
        return name switch
        {
            WallName.Left => left && left.gameObject.activeSelf,
            WallName.Right => right && right.gameObject.activeSelf,
            WallName.Bottom => bottom && bottom.gameObject.activeSelf,
            WallName.Top => top && top.gameObject.activeSelf,
            _ => false,
        };
    }


    private void AddPillars()
    {
        int activeWallsCount = GetActiveWallCount();
        Debug.Log(activeWallsCount);
        if (activeWallsCount < 2)
        {
            if (!IsWallActive(WallName.Bottom))
            {
                bottomGate.gameObject.SetActive(true);
            }
            if (!IsWallActive(WallName.Top))
            {
                topGate.gameObject.SetActive(true);
            }
            if (!IsWallActive(WallName.Left))
            {
                leftGate.gameObject.SetActive(true);
            }
            if (!IsWallActive(WallName.Right))
            {
                rightGate.gameObject.SetActive(true);
            }
        }
    }

    private void HideGates()
    {
        bottomGate.gameObject.SetActive(false);
        topGate.gameObject.SetActive(false);
        leftGate.gameObject.SetActive(false);
        rightGate.gameObject.SetActive(false);
    }
}