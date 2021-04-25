using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationStone : MonoBehaviour
{

    [SerializeField] public string text;
    private InformationOverlay informationOverlay;

    void Start()
    {
        informationOverlay = FindObjectOfType<InformationOverlay>();
    }

    void Update()
    { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null) {
            informationOverlay.AssignText(text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null) {
            informationOverlay.DeactivateText();
        }
    }
}
