using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointAreaManager : MonoBehaviour
{
    public static PointAreaManager instance;

    
    public List<Transform> spawnPoint = new List<Transform>();
    public List<Transform> SpawnPoint => spawnPoint;
    
    public List<Transform> spawnPointMeteorite = new List<Transform>();
    public List<Transform> spawnPointBomb = new List<Transform>();
    public List<Transform> spawnPointPlayer = new List<Transform>();
    public List<Transform> SpawnPointPlayer => spawnPointPlayer;

    private Transform[] playerSpawnStart = new Transform[4];
    public Transform[] PlayerSpawnStart => playerSpawnStart;

    private Dictionary<Transform, bool> dictInUse = new Dictionary<Transform, bool>();
    public Dictionary<Transform, bool> DictInUse => dictInUse;

    void Awake()
    {
        if (instance == null)
            instance = this;

        RemoveObjectNullFromList(spawnPoint);
        RemoveObjectNullFromList(spawnPointMeteorite);
        RemoveObjectNullFromList(spawnPointBomb);
        RemoveObjectNullFromList(spawnPointPlayer);

        foreach (Transform point in SpawnPoint)
        {
            dictInUse.Add(point, false);
        }

        int i = 0;
        foreach (Transform point in spawnPointPlayer)
        {
            if (point.CompareTag("SpawnPoint"))
            {
                if (point.position.x < 0)
                {
                    if (point.position.z < 0)
                        playerSpawnStart[2] = point;
                    else
                        playerSpawnStart[0] = point;
                }
                else
                {
                    if (point.position.z < 0)
                        playerSpawnStart[3] = point;
                    else
                        playerSpawnStart[1] = point;
                }

                i++;

                if(i >= 4)
                    return;
            }
        }

    }

    private void RemoveObjectNullFromList(List<Transform> listTransform)
    {
        for (int i = 0; i < listTransform.Count;)
        {
            if (!listTransform[i])
            {
                listTransform.Remove(listTransform[i]);
                continue;
            }
            
            i++;
        }
    }

    public Transform GetRandomPosition()
    {
        return RandomPosition(spawnPoint);
    }

    public Transform GetPlayerRandomPos()
    {
        int xcount = Random.Range(0, 3);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.SpawnSound);
        return RandomPosition(spawnPointPlayer);
    }

    public Transform GetBombRandomPos()
    {
        return RandomPosition(spawnPointBomb);
    }

    public Transform GetMeteoriteRandomPos()
    {
        return RandomPosition(spawnPointMeteorite);
    }

    public Transform RandomPosition(List<Transform> listPoint)
    {
        Transform point = null;
        int secuEnfant = 0;
        while (point == null && secuEnfant < 1000)
        {
            RaycastHit hit;
            int i = Random.Range(0, listPoint.Count);

            bool isGood = true;
            if (!dictInUse[listPoint[i]])
            {
                for (int j = -30; j < 360; j += 30)
                {
                    Vector3 origin = new Vector3(listPoint[i].position.x, listPoint[i].position.y + 1, listPoint[i].position.z);
                    Debug.DrawRay(origin, listPoint[i].up * -1 * 4, Color.green, 5.0f);
                    Physics.Raycast(origin, listPoint[i].up * -1, out hit, 4);

                    if (hit.transform == null)
                        isGood = false;
                    else if (hit.transform.tag != "Platform")
                        isGood = false;

                    listPoint[i].eulerAngles = new Vector3(20, j, 0);
                }

                listPoint[i].eulerAngles = Vector3.zero;
            }
            else
            {
                isGood = false;
            }

            if (isGood)
                point = listPoint[i];
            else
                secuEnfant++;
        }


        if (secuEnfant >= 1000)
        {
            Debug.Log("LES GD VOUS FAITES NIMP");
            point = transform;
        }

        return point;
    }
}
