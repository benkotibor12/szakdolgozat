using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : Interactable
{

    public List<GameObject> loots;
    public float closeTime;
    private Transform lootDropPoint;
    private bool opened = false;

    private void Start()
    {
        lootDropPoint = transform.Find("Drop");
    }

    protected override void Interact()
    {
        if (opened == false)
        {
            GetComponent<Animator>().SetBool("isOpened", !opened);
            if (loots.Count > 0)
            {
                Instantiate(loots[Random.Range(0, loots.Count)], lootDropPoint.position, transform.rotation);
                loots = new();
            }
            StartCoroutine(CloseChestAfter(closeTime));
        }
    }

    private IEnumerator CloseChestAfter(float time)
    {
        yield return new WaitForSeconds(time);
        opened = false;
        GetComponent<Animator>().SetBool("isOpened", opened);
    }

}
