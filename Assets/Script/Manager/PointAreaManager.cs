using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAreaManager : MonoBehaviour
{
    public static PointAreaManager instance;

    [SerializeField] private GameObject[] spawnPoint;
    [SerializeField] private GameObject pointArea;
    [SerializeField] private GameObject anneau;

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

    public IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        int i = Random.Range(0, spawnPoint.Length);
        Instantiate(pointArea, spawnPoint[i].transform.position, Quaternion.identity, anneau.transform);
    }
}
