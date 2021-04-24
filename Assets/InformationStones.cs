using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InformationStoneEntry {
    TestEntry1,
    TestEntry2,
};

public class InformationStones : MonoBehaviour
{
    private InformationText informationText;

    void Start()
    {
        informationText = FindObjectOfType<InformationText>();
    }


    void Update()
    {
        
    }
}
