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


    [Header("Temporaire")] 
    private int noGold = 1;
    private int oneGold = 1;
    private int twoGold = 1;
    private int treeGold = 97;

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

        if (noGold + oneGold + twoGold + treeGold != 100)
            Debug.Log("LES PROBA SONT PAS BONNE ({noGold + oneGold + twoGold + treeGold} c'est pas 100%)");

        int _g = Random.Range(0, 100);
        if (_g < noGold)
        {
            //Debug.Log("0 / " + _g);
            bag.GetComponent<FishBag>().goldenFish.Add(999);
        }
        else if(_g < noGold + oneGold)
        {
            //Debug.Log("1 / " + _g);
            int firstGFish = Random.Range(0, bag.transform.GetChild(0).childCount);

            bag.transform.GetChild(0).GetChild(firstGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;
            bag.GetComponent<FishBag>().goldenFish.Add(firstGFish);
        }
        else if (_g < noGold + oneGold + twoGold)
        {
            //Debug.Log("2 / " + _g);
            int firstGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            int secondGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            while (secondGFish == firstGFish)
                secondGFish = Random.Range(0, bag.transform.GetChild(0).childCount);

            bag.transform.GetChild(0).GetChild(firstGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;
            bag.transform.GetChild(0).GetChild(secondGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;

            bag.GetComponent<FishBag>().goldenFish.Add(firstGFish);
            bag.GetComponent<FishBag>().goldenFish.Add(secondGFish);
        }
        else
        {
            //Debug.Log("3 / " + _g);
            int firstGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            int secondGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            while (secondGFish == firstGFish)
                secondGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            int thirdGFish = Random.Range(0, bag.transform.GetChild(0).childCount);
            while(thirdGFish == secondGFish || thirdGFish == firstGFish)
                thirdGFish = Random.Range(0, bag.transform.GetChild(0).childCount);

            bag.transform.GetChild(0).GetChild(firstGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;
            bag.transform.GetChild(0).GetChild(secondGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;
            bag.transform.GetChild(0).GetChild(thirdGFish).GetComponent<MeshRenderer>().material = Resources.Load("GoldenFish") as Material;

            bag.GetComponent<FishBag>().goldenFish.Add(firstGFish);
            bag.GetComponent<FishBag>().goldenFish.Add(secondGFish);
            bag.GetComponent<FishBag>().goldenFish.Add(thirdGFish);
        }

        bag.GetComponent<FishBag>().goldenFish.Sort();

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
