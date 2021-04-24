using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationOverlay : MonoBehaviour
{
    public GameObject informationBackground;
    private InformationText informationText;
    void Start()
    {
        informationText = FindObjectOfType<InformationText>();
    }

    public void AssignText(string text) 
    {
        informationText.AssignText(text);
        informationBackground.SetActive(true);
    }

    public void DeactivateText() {
        informationText.AssignText("");
        informationBackground.SetActive(false);
    }
}
