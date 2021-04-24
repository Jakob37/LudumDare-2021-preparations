using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationText : MonoBehaviour
{
    // Start is called before the first frame update
    private Text uiText;
    void Start()
    {
        uiText = GetComponent<Text>();
    }

    public void AssignText(string text) {
        uiText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
