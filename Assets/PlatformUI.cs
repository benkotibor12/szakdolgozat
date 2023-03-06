using UnityEngine;
using TMPro;
public class PlatformUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI debugText;

    public void UpdateText(string text)
    {
        debugText.text = text;
    }
}

