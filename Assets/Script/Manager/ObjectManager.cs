using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [Header("General"), SerializeField]
    private int itemPossible;
    [SerializeField] private float cdSpawn;
    [SerializeField] private float cdDespawn;
    private List<GameObject> allObjectList = new List<GameObject>();

    [Header("Multiplier"), SerializeField] 
    private float cdMultiplier;
    private GameObject multiplierObject;

    [Header("SpeedUp"), SerializeField] 
    private float cdSpeedUp;
    private GameObject speedObject;

    [Header("SlowZone"), SerializeField]
    private float cdSlowZone;
    private GameObject slowZoneObject;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        multiplierObject = Resources.Load<GameObject>("Features/MultiplierObject");
        speedObject = Resources.Load<GameObject>("Features/SpeedUpObject");
        //slowZoneObject = Resources.Load<GameObject>("Features/SpeedUpObject");

        allObjectList.Add(multiplierObject);
        allObjectList.Add(speedObject);
    }

    #region Start Spawn

    private void Start()
    {
        StartCoroutine(SpawnObjectStart());
    }

    private IEnumerator SpawnObjectStart()
    {
        for (int i = 0; i < itemPossible; i++)
        {
            StartCoroutine(SpawnObject());
            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

    #region Multiplier Object

    public void StopMultiplier(Player player)
    {
        if (player.multiplierCoroutine != null)
        {
            StopCoroutine(player.multiplierCoroutine);
            player.multiplierCoroutine = null;
        }
        player.multiplierCoroutine = StartCoroutine(StopMultiplierCD(player));
    }

    private IEnumerator StopMultiplierCD(Player player)
    {
        StartCoroutine(SpawnObject());
        yield return new WaitForSeconds(cdMultiplier);
        player.Multiplier = false;
        player.multiplierCoroutine = null;
    }

    #endregion

    #region SpeedUpObject

    public void StopSpeedUp(PlayerMovement player)
    {
        Player playerData = player.GetComponent<Player>();
        if (playerData.speedCoroutine != null)
        {
            StopCoroutine(playerData.speedCoroutine);
            playerData.speedCoroutine = null;
        }
        playerData.speedCoroutine =  StartCoroutine(StopSpeedUpCD(player, playerData));
    }

    private IEnumerator StopSpeedUpCD(PlayerMovement player, Player playerData)
    {
        Debug.Log("START");
        StartCoroutine(SpawnObject());
        yield return new WaitForSeconds(cdSpeedUp);
        player.Speed = GameManager.instance.MoveSpeed;
        playerData.speedCoroutine = null;
        Debug.Log("STOP");
    }

    #endregion

    #region Spawn / Despawn Gestion

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(cdSpawn);
        int random = Random.Range(0, allObjectList.Count);
        Transform pos = PointAreaManager.instance.GetRandomPosition();
        Instantiate(allObjectList[random], pos.position, Quaternion.identity, pos.parent);
    }

    public IEnumerator DestroyObject(GameObject objet)
    {
        yield return new WaitForSeconds(cdDespawn);
        StartCoroutine(SpawnObject());
        Destroy(objet);
    }

    #endregion
}
