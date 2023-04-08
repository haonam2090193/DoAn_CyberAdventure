using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource cinemachineImpulse;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cinemachineImpulse.GenerateImpulse(Camera.main.transform.forward);
        }
    }
}
