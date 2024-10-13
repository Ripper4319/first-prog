using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerui : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI prompttext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText(string promptmessage)
    {
        prompttext.text = promptmessage;
    }
}
