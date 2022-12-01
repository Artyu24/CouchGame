using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UIInputManager : MonoBehaviour
{

    bool isPauseOpen = false;
    bool isOptionOpen = false;
  /*public GameObject pausePanel, optionsPanel;
    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;*/

    /*private void Start()
    {
        pausePanel = PlayerManager.instance.pausePanel;
        optionsPanel = PlayerManager.instance.optionsPanel;
        FillChild();
    }

    private void FillChild()
    {
        pauseFirstButton = pausePanel.transform.GetChild(1).GetChild(0).GetComponent<GameObject>();
        optionsFirstButton = optionsPanel.transform.GetChild(1).GetChild(0).GetComponent<GameObject>();
        optionsClosedButton = pausePanel.transform.GetChild(1).GetChild(1).GetComponent<GameObject>();
    }*/

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            CloseOptionMenu();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.pausePanel.SetActive(false);
    }
    public void OpenOptionMenu()
    {
        if (GameManager.instance.optionsPanel != null)
        {
            GameManager.instance.optionsPanel.SetActive(true);
            isOptionOpen = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameManager.instance.optionsFirstButton);
        }
    }

    public void CloseOptionMenu()
    {
        GameManager.instance.optionsPanel.SetActive(false);
        isOptionOpen = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GameManager.instance.optionsClosedButton);
    }

    public void OpenClosePausePanel(InputAction.CallbackContext ctx)
    {
        Debug.Log("Sesame ouvre toi");

        if (GameManager.instance.pausePanel != null)
        {
            if (ctx.started && !isPauseOpen)
            {
                Time.timeScale = 0.0f;
                GameManager.instance.pausePanel.SetActive(true);
                isPauseOpen= true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameManager.instance.pauseFirstButton);
            }
            if (ctx.started && isPauseOpen)
            {
                Time.timeScale = 1.0f;
                GameManager.instance.pausePanel.SetActive(false);
                isPauseOpen= false;
            }
        }
        else
        {
            Debug.Log("Le panel pause n'a pas était référencé dans le gamemanager");
        }
    }
    public void OpenCloseOptionsPanel(InputAction.CallbackContext ctx)
    {
        if (GameManager.instance.optionsPanel != null)
        {
            if (ctx.started && !isOptionOpen)
            {
                GameManager.instance.optionsPanel.SetActive(true);
                isOptionOpen= true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameManager.instance.optionsFirstButton);
            }
            if (ctx.started && isOptionOpen)
            {
                GameManager.instance.optionsPanel.SetActive(false);
                isOptionOpen= false;
                //EventSystem.current.SetSelectedGameObject(null);
                //EventSystem.current.SetSelectedGameObject(GameManager.instance.optionsClosedButton);
            }
        }
    }
    
}
