using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatePointCircle : MonoBehaviour
{
    private Object firstEmpty;
    public Object FirstEmpty { get => firstEmpty; set => firstEmpty = value; }

    private Object circleParent;
    public Object CircleParent { get => circleParent; set => circleParent = value; }

    private int nbrSpawnPoint;
    public int NbrSpawnPoint { get => nbrSpawnPoint; set => nbrSpawnPoint = value; }

    private List<Transform> spawnPointList = new List<Transform>();
    public List<Transform> SpawnPointList => spawnPointList;
}
