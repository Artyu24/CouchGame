using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;

    [Header("General"), SerializeField]
    private int itemPossible;
    [SerializeField] private float cdGeneral;
    private List<GameObject> allObjectList = new List<GameObject>();

    [Header("Multiplier"), SerializeField] 
    private float cdMultiplier;
    private GameObject multiplierObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allObjectList.Add(multiplierObject);
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

    public IEnumerator StopMultiplier(Player player)
    {
        yield return new WaitForSeconds(cdMultiplier);
        player.Multiplier = false;
        //Reload
    }

    #endregion

    #region Spawn Gestion

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(cdGeneral);
        int random = Random.Range(0, allObjectList.Count);
        Transform pos = PointAreaManager.instance.GetRandomPosition();
        Instantiate(allObjectList[random], pos.position, Quaternion.identity, pos.parent);
    }

    #endregion
}
