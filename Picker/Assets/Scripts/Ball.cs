using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager Gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter"))
        {
            Gm.BallCounter();
        }
    }
}
