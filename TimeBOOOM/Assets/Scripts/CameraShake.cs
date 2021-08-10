using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    CinemachineVirtualCamera CinemachineVirtualCamera;
    float shakeTimer;

    private void Awake()
    {
        Instance = this;
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin =
            CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        channelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin channelPerlin =
                    CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                channelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
