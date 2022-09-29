using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameManager Gm;
    [SerializeField] private Animator Gateway;

    public void OpenGateway()
    {
        Gateway.Play("Gateway");
    }

    public void GoPicker()
    {
        Gm.StatusPicker = true;
    }
}
