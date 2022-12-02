using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private GameObject playerCamera;
    [SerializeField] private CinemachineTargetGroup targetAllPlayer;
    [SerializeField] private float radiusPlayer = 2;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

    }

    private void Start()
    {
        targetAllPlayer.m_Targets = new CinemachineTargetGroup.Target[5];
        targetAllPlayer.m_Targets[0] = CreateNewTarget(AudioManager.instance.transform, 1f, 0.2f);
    }

    private CinemachineTargetGroup.Target CreateNewTarget(Transform t, float weight, float radius)
    {
        CinemachineTargetGroup.Target target = new CinemachineTargetGroup.Target();
        target.radius = radius;
        target.target = t;
        target.weight = weight;
        return target;
    }

    public void AddPlayerTarget(Transform playerTransform, int id)
    {
        targetAllPlayer.m_Targets[id] = CreateNewTarget(playerTransform, 1, radiusPlayer);
    }

    public void RemovePlayerTarget(int id)
    {
        targetAllPlayer.m_Targets[id] = new CinemachineTargetGroup.Target();
    }

    public void ChangeCamera()
    {
        if(playerCamera.activeInHierarchy)
            playerCamera.SetActive(false);
        else
            playerCamera.SetActive(true);
        
    }
}
