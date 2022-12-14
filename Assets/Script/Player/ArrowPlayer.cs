using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrowPlayer : MonoBehaviour
{
    public Color[] flecheColor = new Color[4] { Color.blue, Color.red, Color.yellow, Color.green };
    public GameObject fleche, texte;
    public float offsetArrow = 2;
    [HideInInspector]
    public Camera cam;
    [SerializeField]
    private Canvas canvasArrow;

    private void Start()
    {
        cam = GameManager.instance.CameraScene;
        canvasArrow.worldCamera = cam;
        canvasArrow.transform.SetParent(null);
        //Debug.Log(GetComponentInParent<Player>().playerID);
        fleche.GetComponent<Image>().color = flecheColor[GetComponentInParent<Player>().playerID];
        texte.GetComponent<Text>().text = "J" + (GetComponentInParent<Player>().playerID+1);
        texte.GetComponent<Text>().color = flecheColor[GetComponentInParent<Player>().playerID];
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Leaderboard")
        {
            if (GameManager.instance.ActualGameState == GameState.ENDROUND && fleche.activeInHierarchy)
            {
                fleche.SetActive(false);
                texte.SetActive(false);
                //remettre le active a true en début de manche
            }
            //Remettre la rotation du canvas en opposition a celui du player
            Vector3 arrowPos = transform.position;
            Vector3 canvasRotationOffset = cam.transform.localEulerAngles;
            canvasArrow.transform.localEulerAngles = canvasRotationOffset;
            arrowPos.z += offsetArrow;
            canvasArrow.transform.position = arrowPos;
        }
    }
}
