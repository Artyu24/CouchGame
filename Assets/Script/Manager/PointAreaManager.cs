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

    public void StartNextSpawn()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        Transform pos = RandomPosition();
        Instantiate(fishBag, pos.position, Quaternion.identity, pos.parent);
    }

    public Transform RandomPosition()
    {
        Transform point = null;
        int secuEnfant = 0;
        while (point == null && secuEnfant != 1000)
        {
            RaycastHit hit;
            Debug.DrawRay(spawnPoint[0].position, spawnPoint[0].up * -1 * 2, Color.yellow, 5.0f);
            Physics.Raycast(spawnPoint[0].position, spawnPoint[0].up * -1, out hit, 2);

            int i = Random.Range(0, spawnPoint.Count);
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
