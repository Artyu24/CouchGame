using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private GameObject playerCamera, hyperSpaceCamera;
    [SerializeField] private CinemachineTargetGroup targetAllPlayer;
    [SerializeField] private float radiusPlayer = 2;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        hyperSpaceCamera.SetActive(false);

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
        if (GameManager.instance.ActualGameState == GameState.INGAME)
        {
            bool anybody = true;
            for (int i = 1; i < 5; i++)
            {
                if (targetAllPlayer.m_Targets[i].weight != 0)
                    anybody = false;
            }
            
            if(anybody)
                ChangeCamera();
        }

        targetAllPlayer.m_Targets[id] = CreateNewTarget(playerTransform, 1, radiusPlayer);
    }

    public void RemovePlayerTarget(int id)
    {
        targetAllPlayer.m_Targets[id] = new CinemachineTargetGroup.Target();

        for (int i = 1; i < 5; i++)
        {
            if (targetAllPlayer.m_Targets[i].weight != 0)
                return;
        }
        ChangeCamera();
    }

    public void ChangeCamera()
    {
        if(playerCamera.activeInHierarchy)
            playerCamera.SetActive(false);
        else
            playerCamera.SetActive(true);
    }

    public void ActivateHyperSpace()
    {
        hyperSpaceCamera.SetActive(true);
    }
}
