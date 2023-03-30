using UnityEngine;
using System.Collections;

public class Chest : Interactable
{

    public GameObject loot;
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
            if (loot != null)
            {
                Instantiate(loot, lootDropPoint.position, transform.rotation);
                loot = null;
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
