using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAreaManager : MonoBehaviour
{
    public static PointAreaManager instance;

    [SerializeField] private List<Transform> spawnPoint = new List<Transform>();
    public List<Transform> SpawnPoint => spawnPoint;
    [SerializeField] private GameObject fishBag;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            Transform pos = RandomPosition();
            Instantiate(fishBag, pos.position, Quaternion.identity, pos.parent);
        }
    }

    public Transform RandomPosition()
    {
        Transform point = null;
        int secuEnfant = 0;
        while (point == null && secuEnfant != 1000)
        {
            RaycastHit hit;
            int i = Random.Range(0, spawnPoint.Count);
            Debug.DrawRay(spawnPoint[i].position, spawnPoint[i].up * -1 * 2, Color.yellow, 5.0f);
            Physics.Raycast(spawnPoint[i].position, spawnPoint[i].up * -1, out hit, 2);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Platform")
                    point = spawnPoint[i];
            }

            secuEnfant++;
        }


        if (secuEnfant == 1000)
        {
            Debug.Log("LES GD VOUS FAITES NIMP");
            point = transform;
        }

        return point;
    }
}
