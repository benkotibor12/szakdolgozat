using NUnit.Framework;
using UnityEngine;

public class MapGeneratorTest
{
    [Test]
    public void SetupPrefabTest()
    {
        // Arrange 
        MapGenerator mapGenerator = new GameObject().AddComponent<MapGenerator>();
        GameObject platformPrefab = (GameObject)Resources.Load("Prefabs/Map/Platform", typeof(GameObject));
        mapGenerator.platformPrefab = platformPrefab;

        // Act
        mapGenerator.SetupPrefab();

        // Assert
        Assert.NotNull(mapGenerator.left);
        Assert.NotNull(mapGenerator.right);
        Assert.NotNull(mapGenerator.top);
        Assert.NotNull(mapGenerator.bottom);
        Assert.NotNull(mapGenerator.floor);
    }

    [Test]
    public void ResetPrefabTest()
    {
        // Arrange
        MapGenerator mapGenerator = new GameObject().AddComponent<MapGenerator>();
        GameObject platformPrefab = (GameObject)Resources.Load("Prefabs/Map/Platform", typeof(GameObject));
        GameObject left = platformPrefab.transform.Find("Left").gameObject;
        GameObject right = platformPrefab.transform.Find("Right").gameObject;
        GameObject top = platformPrefab.transform.Find("Top").gameObject;
        GameObject bottom = platformPrefab.transform.Find("Bottom").gameObject;
        GameObject floor = platformPrefab.transform.Find("Floor").gameObject;

        left.SetActive(false);
        right.SetActive(false);
        top.SetActive(false);
        bottom.SetActive(false);
        floor.SetActive(false);

        mapGenerator.platformPrefab = platformPrefab;

        // Act
        mapGenerator.ResetPrefab(mapGenerator.platformPrefab);

        // Assert
        Assert.IsTrue(left.activeSelf);
        Assert.IsTrue(right.activeSelf);
        Assert.IsTrue(top.activeSelf);
        Assert.IsTrue(bottom.activeSelf);
        Assert.IsTrue(floor.activeSelf);
    }
}
