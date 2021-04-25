using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private Animator myAnimator;
    public EnemyHealth enemyHealth;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (enemyHealth.GetIsInjured()) {
            myAnimator.SetBool("isInjured", true);
        }
    }
}
