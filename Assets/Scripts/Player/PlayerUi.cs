using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    // Call this method whenever you want to update the prompt text
    
    // Example usage:
    void Start()
    {
        ;
    }

    // Update is called once per frame
    public void UpdateText(string promptMessege)
    {
        promptText.text = promptMessege;
    }
}
