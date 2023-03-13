using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private List<GameObject> initializedCells;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        initializedCells = new();
    }

    public void AddInitializedCell(GameObject cell)
    {
        initializedCells.Add(cell);
    }

    public void AddInitializedCells(List<GameObject> cells)
    {
        initializedCells.AddRange(cells);
    }

    public List<GameObject> GetInitializedCells()
    {
        return initializedCells;
    }
}
