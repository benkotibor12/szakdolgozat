using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObject : Interactable
{
    public string sceneName; 
    protected override void Interact()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
