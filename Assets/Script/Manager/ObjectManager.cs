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
    private GameObject effectSpawn;
    public GameObject EffectSpawn => effectSpawn;
    private List<GameObject> allObjectList = new List<GameObject>();

    [Header("FishBag")]
    [SerializeField] private int goldenRate = 50;
    private GameObject fishBag;

    [Header("Multiplier"), SerializeField] 
    private float cdMultiplier;
    private GameObject multiplierObject;

    [Header("SpeedUp"), SerializeField] 
    private float cdSpeedUp;
    private GameObject speedObject;

    [Header("SlowZone")]
    private GameObject slowZoneObject;

    [Header("BOMB")]
    public float timeBeforeExplosion;
    public float cdBeforeAutoExplosion;
    public int bombStrenght;
    private GameObject bomb;

    [Header("Item Percentage"), SerializeField]
    private int percentMultiplier = 1;
    [SerializeField] private int percentSpeedUp = 1;
    [SerializeField] private int percentSlowZone = 1;
    [SerializeField] private int percentBomb = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        fishBag = Resources.Load<GameObject>("Features/FishBag");

        multiplierObject = Resources.Load<GameObject>("Features/MultiplierObject");
        speedObject = Resources.Load<GameObject>("Features/SpeedUpObject");
        slowZoneObject = Resources.Load<GameObject>("Features/SlowWater");
        bomb = Resources.Load<GameObject>("Features/Bomb");
        effectSpawn = Resources.Load<GameObject>("Features/Spawn");

        SetPercentObject(multiplierObject, percentMultiplier);
        SetPercentObject(speedObject, percentSpeedUp);
        SetPercentObject(slowZoneObject, percentSlowZone);
        SetPercentObject(bomb, percentBomb);
    }

    private void SetPercentObject(GameObject objet, int percent)
    {
        for (int i = 0; i < percent; i++)
            allObjectList.Add(objet);    
    }

    #region Start Spawn

    public void InitSpawnAll()
    {
        StartCoroutine(LaunchFishBag());
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

    #region Fishbag
    private IEnumerator LaunchFishBag()
    {
        for (int i = 0; i < GameManager.instance.NbrFishBag; i++)
        {
            StartCoroutine(Spawn());
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartNextSpawn()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        SpawnBag();
    }

    private void SpawnBag()
    {
        if (GameManager.instance.ActualGameState == GameState.INGAME)
        {
            Transform pos = PointAreaManager.instance.GetRandomPosition();
            GameObject bag = Instantiate(fishBag, pos.position, Quaternion.identity, pos.parent);

            bool i = Random.Range(0, 100) <= goldenRate ? bag.GetComponent<FishBag>().isGolden = true : bag.GetComponent<FishBag>().isGolden = false;
        }

    }
    
    public void CallBarSpePlus(GameObject player, bool isGolden)
    {
        StartCoroutine(BarSpePlus(player, isGolden));
    }

    private IEnumerator BarSpePlus(GameObject player, bool isGolden)
    {
        yield return new WaitForSecondsRealtime(1.4f);
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();

        if (isGolden)//le poisson est goldé
        {
            //Debug.Log("GOLDEEEEEEENNNNNNNN");
            ScoreManager.instance.AddScore(ScoreManager.instance.scoreGoldPointArea, player.GetComponent<Player>());
            if (playerAttack.CurrentSpecial < playerAttack.maxSpecial)
                playerAttack.AddSpeBarrePoint(playerAttack.maxSpecial - playerAttack.CurrentSpecial);
        }
        else//bouh le nul
        {
            ScoreManager.instance.AddScore(ScoreManager.instance.scorePointArea, player.GetComponent<Player>());
            if (playerAttack.CurrentSpecial < playerAttack.maxSpecial)
                playerAttack.AddSpeBarrePoint(1);
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
        playerData.speedCoroutine = StartCoroutine(StopSpeedUpCD(player, playerData));
    }

    private IEnumerator StopSpeedUpCD(PlayerMovement player, Player playerData)
    {
        StartCoroutine(SpawnObject());
        yield return new WaitForSeconds(cdSpeedUp);
        if(playerData.IsSlow)
            player.Speed = GameManager.instance.MinMoveSpeed;
        else
            player.Speed = GameManager.instance.MoveSpeed;
        
        playerData.IsSpeedUp = false;
        playerData.speedCoroutine = null;
    }

    #endregion

    #region Spawn / Despawn Gestion

    public void SpawnNextObject()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(cdSpawn);
        
        if (GameManager.instance.ActualGameState == GameState.INGAME)
        {
            int random = Random.Range(0, allObjectList.Count);

            Transform pos = transform;
            if (!allObjectList[random].GetComponent<Bomb>())
                pos = PointAreaManager.instance.GetRandomPosition();
            else
                pos = PointAreaManager.instance.GetBombRandomPos();

            PointAreaManager.instance.DictInUse[pos] = true;

            GameObject obj = allObjectList[random];
            if (!obj.GetComponent<SlowWater>())
            {
                GameObject effect = Instantiate(effectSpawn, pos.position, Quaternion.identity, pos.parent);
                yield return new WaitForSeconds(2f);
                Instantiate(obj, pos.position, Quaternion.identity, pos.parent);
                yield return new WaitForSeconds(2f);
                Destroy(effect);
            }
            else
            {
                pos.position = new Vector3(pos.position.x, pos.position.y - 0.4f, pos.position.z);
                Instantiate(obj, pos.position, Quaternion.identity, pos.parent);
            }
            PointAreaManager.instance.DictInUse[pos] = false;
        }
    }

    public IEnumerator DestroyObject(GameObject objet)
    {
        yield return new WaitForSeconds(cdDespawn);
        StartCoroutine(SpawnObject());
        Destroy(objet);
    }

    #endregion
}
