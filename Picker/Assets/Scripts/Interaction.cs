using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameManager Gm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorder"))
        {
            Gm.OnBorder();
        }
    }
}
