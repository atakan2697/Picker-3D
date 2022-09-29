using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 TargetOffSet;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + TargetOffSet, .125f);
    }
}
