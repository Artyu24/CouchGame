using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointAreaManager : MonoBehaviour
{
    public static PointAreaManager instance;

    
    public List<Transform> spawnPoint = new List<Transform>();
    public List<Transform> SpawnPoint => spawnPoint;
    
    
    public List<Transform> spawnPointPlayer = new List<Transform>();
    public List<Transform> SpawnPointPlayer => spawnPointPlayer;

    private Transform[] playerSpawnStart = new Transform[4];
    public Transform[] PlayerSpawnStart => playerSpawnStart;

    private GameObject fishBag;

    void Awake()
    {
        if (instance == null)
            instance = this;

        fishBag = Resources.Load<GameObject>("Features/FishBag");

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

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.instance.NbrFishBag; i++)
            StartCoroutine(Spawn());
    }

    //Test
    //void Update()
    //{
    //    RaycastHit hit;
    //    int i = 0;
    //    Debug.DrawRay(spawnPoint[i].position, spawnPoint[i].up * -1 * 2, Color.yellow, 5.0f);
    //    Physics.Raycast(spawnPoint[i].position, spawnPoint[i].up * -1, out hit, 2);
    //    Debug.Log(hit.transform.gameObject.name);
    //}

    public void StartNextSpawn()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        if (spawnPoint.Count == 0 || spawnPoint.Contains(null))
            Debug.Log("Il manque des spawn Point");
        else
        {
            SpawnBag();
        }
    }

    #region BEURK
    private void SpawnBag()
    {
        Transform pos = GetRandomPosition();
        GameObject bag = Instantiate(fishBag, pos.position, new Quaternion(-45f, 180f, 0, 0), pos.parent);

        bool i = Random.Range(0, 100) % 2 == 0 ? bag.GetComponent<FishBag>().isGolden = true : bag.GetComponent<FishBag>().isGolden = false;
    }
    #endregion

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

    public Transform RandomPosition(List<Transform> listPoint)
    {
        Transform point = null;
        int secuEnfant = 0;
        while (point == null && secuEnfant < 1000)
        {
            RaycastHit hit;
            int i = Random.Range(0, listPoint.Count);
            Debug.DrawRay(listPoint[i].position, listPoint[i].up * -1 * 2, Color.yellow, 5.0f);
            Physics.Raycast(listPoint[i].position, listPoint[i].up * -1, out hit, 2);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Platform")
                    point = listPoint[i];
            }

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
