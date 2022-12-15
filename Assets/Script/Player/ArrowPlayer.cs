using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrowPlayer : MonoBehaviour
{
    public Color[] flecheColor = new Color[4] { Color.blue, Color.red, Color.yellow, Color.green };
    public GameObject fleche, texte;
    public float offsetArrowZ = 0.75f;
    public float offsetArrowY = 0;
    public float offsetArrowZMiddle = 1;
    public float offsetArrowYMiddle = 1;
    [HideInInspector]
    public Camera cam;
    [SerializeField]
    private Canvas canvasArrow;
    private Player player;

    private void Start()
    {
        cam = GameManager.instance.CameraScene;
        canvasArrow.worldCamera = cam;
        canvasArrow.transform.SetParent(null);
        //Debug.Log(GetComponentInParent<Player>().playerID);
        fleche.GetComponent<Image>().color = flecheColor[GetComponentInParent<Player>().playerID];
        texte.GetComponent<Text>().text = "J" + (GetComponentInParent<Player>().playerID+1);
        texte.GetComponent<Text>().color = flecheColor[GetComponentInParent<Player>().playerID];
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Leaderboard")
        {
            if (GameManager.instance.ActualGameState == GameState.ENDROUND || GameManager.instance.ActualGameState == GameState.ENDSCORING && fleche.activeInHierarchy)
            {
                fleche.SetActive(false);
                texte.SetActive(false);
                //remettre le active a true en début de manche
            }

            if (player.ActualPlayerState == PlayerState.MIDDLE)
            {
                offsetArrowZ = offsetArrowZMiddle;
                offsetArrowY = offsetArrowYMiddle;
            }
            else
            {
                offsetArrowZ = 0.75f;
                offsetArrowY = 0;
            }

            //Remettre la rotation du canvas en opposition a celui du player
            Vector3 arrowPos = transform.position;
            Vector3 canvasRotationOffset = cam.transform.localEulerAngles;
            canvasArrow.transform.localEulerAngles = canvasRotationOffset;
            arrowPos.z += offsetArrowZ;
            arrowPos.y += offsetArrowY;
            canvasArrow.transform.position = arrowPos;
        }
    }
}
