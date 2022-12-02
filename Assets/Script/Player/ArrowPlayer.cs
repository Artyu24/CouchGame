using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPlayer : MonoBehaviour
{
    public Color[] flecheColor = new Color[4] { Color.blue, Color.red, Color.yellow, Color.green };
    public GameObject fleche, texte;
    public float offsetArrow = 2;
    Camera cam;

    private void Start()
    {
        cam = GameManager.instance.CameraScene;
        //Debug.Log(GetComponentInParent<Player>().playerID);
        fleche.GetComponent<Image>().color = flecheColor[GetComponentInParent<Player>().playerID];
        texte.GetComponent<Text>().text = "J" + (GetComponentInParent<Player>().playerID+1);
        texte.GetComponent<Text>().color = flecheColor[GetComponentInParent<Player>().playerID];
    }

    void Update()
    {
        Vector3 screenPos = new Vector3(transform.position.x, transform.position.y + offsetArrow, transform.position.z);
        fleche.transform.position = cam.WorldToScreenPoint(screenPos);
    }
}
