using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Timer
    private Text timerText;
    private Text timerSet;

    [SerializeField]
    private float timerForStart = 4.0f;
    private bool startGame = false;

    private float timerCountDown = 5;

    private int minutes, seconds;

    bool fiveSecondLeft;
    bool canBePlay = true;

    #endregion

    #region Scoreboard
    private bool scoreWindowRoundIsActive = false;
    private bool scoreWindowGeneralIsActive = false;

    [SerializeField] private GameObject scoreWindowRound, scoreWindowGeneral;
    [SerializeField] private Sprite[] ppWindowRound = new Sprite[4];
    [SerializeField] private Sprite[] backgroundWindowRound = new Sprite[4];
    [SerializeField] private GameObject textParentRound, textParentGeneral;
    public GameObject roundScoreTextPrefab, generalScoreTextPrefab; 

    private Text[] scorePlayerText = new Text[4];
    private Text[] scoreGeneralPlayerText = new Text[4];

    private GameObject[] medals = new GameObject[3];
    private int numberOfMedal = 3;
    #endregion

    private void Awake()
    {
        timerText = GetComponent<Text>();
        medals[0] = Resources.Load<GameObject>("UI/GoldMedalParent");
        medals[1] = Resources.Load<GameObject>("UI/SilverMedalParent");
        medals[2] = Resources.Load<GameObject>("UI/CopperMedalParent");
    }

    private void Start()
    {
        PlayerManager.instance.FindCanvas();

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            Debug.Log(PlayerManager.instance.players[i].name + " : " + PlayerManager.instance.players[i].medals.Count);
            PlayerManager.instance.Init(i, PlayerManager.instance.players[i].gameObject);
        }
        
        StartCoroutine(StartingGame());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(GameManager.instance.TimerSound());
            GameManager.instance.ActualGameState = GameState.INIT;
        }

        if (GameManager.instance.ActualGameState == GameState.ENDROUND)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
                ReloadScene();
        }

        if (GameManager.instance.ActualGameState == GameState.INGAME)
        {
            if (GameManager.instance.Timer <= 0.0f && !scoreWindowRoundIsActive && ScoreManager.instance.terrain.Count != 0)
            {
                GameManager.instance.ActualGameState = GameState.ENDROUND;
                StartCoroutine(FinishAllActions());
            }            

            if (GameManager.instance.Timer <= 7f)
            {                
                fiveSecondLeft = true;
                if(canBePlay == true)
                {
                    StartCoroutine(FiveSecond());
                }
            }

            if (!scoreWindowRoundIsActive)
            {
                GameManager.instance.Timer -= Time.deltaTime;
                minutes = Mathf.FloorToInt(GameManager.instance.Timer / 60f);
                seconds = Mathf.FloorToInt(GameManager.instance.Timer % 60f);
            }

            if (GameManager.instance.Timer >= 0.0f)
            {
                timerText.text = minutes.ToString("00") + " : " + seconds.ToString("00");
            }
            else
            {
                timerText.text = "00 : 00";
            }
        }
        
        else if (GameManager.instance.ActualGameState == GameState.INIT)
        {
            if (timerCountDown <= 0)
            {
                CameraManager.Instance.ChangeCamera();
                ObjectManager.Instance.InitSpawnAll();
                GameManager.instance.ActualGameState = GameState.INGAME;
                FindObjectOfType<AudioManager>().PlayRandom(SoundState.Music);
                StartCoroutine(GameManager.instance.TargetMeteorite());
                
                GameManager.instance.ButtonToPress.SetActive(false);
            }

            timerCountDown -= Time.deltaTime;
        }
        else
        {
            timerText.text = "";
        }
    }

    private IEnumerator StartingGame()
    {
        yield return new WaitForSeconds(timerForStart);
        StartCoroutine(GameManager.instance.TimerSound());
        GameManager.instance.ActualGameState = GameState.INIT;
    }

    public IEnumerator FiveSecond()
    {
        if(fiveSecondLeft == true)
        {
            FindObjectOfType<AudioManager>().PlayRandom(SoundState.CountdownFinal5sSound);
            yield return new WaitForSeconds(1f);
            fiveSecondLeft = false;
            canBePlay = false;
            yield return new WaitForSeconds(3f);
            StartCoroutine(GameManager.instance.TimerVisuFin());
        }
    }
    
    private IEnumerator FinishAllActions()
    {
        FindObjectOfType<AudioManager>().Stop(SoundState.Music);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.WinSound);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.EndPublicSound);

        if (GameManager.instance.PlayerInMiddle != null)
        {
            EjectPlayerCenter.EjectPlayer();
        }

        yield return new WaitForSeconds(2); 

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        GameManager.instance.CameraScene.gameObject.transform.parent = null;
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            CameraManager.Instance.RemovePlayerTarget(i + 1);
        }

        CameraManager.Instance.ActivateHyperSpace();
        ScoreManager.instance.hyperSpeed.SetActive(true);
        PrintGeneralScoreWindow();
    }

    public void PrintGeneralScoreWindow()
    {
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            DontDestroyOnLoad(PlayerManager.instance.players[i].gameObject);
        }

        if (!scoreWindowGeneralIsActive)
        {
            scoreWindowGeneralIsActive = true;
            scoreWindowGeneral.SetActive(scoreWindowGeneralIsActive);
            Sequence mySequence3 = DOTween.Sequence();
            mySequence3.Append(scoreWindowGeneral.GetComponent<RectTransform>().DOAnchorPosY(0, 2));
            mySequence3.onComplete += () =>
            {
                int position = 0;


                int bestScore = int.MinValue;
                Player playerTemp = null;
                List<Player> tempPlayerListPlayer = new List<Player>();

                while (tempPlayerListPlayer.Count < PlayerManager.instance.players.Count)//tri les joueurs du plus gros score au plus petit
                {
                    foreach (var player in PlayerManager.instance.players)
                    {
                        if (!tempPlayerListPlayer.Contains(player.Value) && player.Value.score > bestScore)
                        {
                            bestScore = player.Value.score;
                            playerTemp = player.Value;
                        }
                    }
                    tempPlayerListPlayer.Add(playerTemp);
                    bestScore = int.MinValue;
                }

                GameObject temp = null;
                for (int p = 0; p < tempPlayerListPlayer.Count; p++)
                {
                    temp = Instantiate(generalScoreTextPrefab, textParentGeneral.transform); // spawn des prefab pour les score avec les oeufs
                    scoreGeneralPlayerText[p] = temp.GetComponentInChildren<Text>();
                    temp.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = ppWindowRound[tempPlayerListPlayer[p].playerID];
                    temp.GetComponent<Image>().sprite = backgroundWindowRound[p];
                    temp.name = "Player " + (tempPlayerListPlayer[p].playerID + 1);
                    scoreGeneralPlayerText[p].text = "Player " + (tempPlayerListPlayer[p].playerID + 1) + " : ";

                    for (int i = 0; i < PlayerManager.instance.players[tempPlayerListPlayer[p].playerID].medals.Count; i++)//Medilles ancien round
                    {
                        Instantiate(medals[0], temp.transform);
                    }

                    for (int i = 0; i < numberOfMedal; i++)//nouvelles medailles
                    {
                        if (tempPlayerListPlayer[p].score >= 0)
                        {
                            //Anim d'apparition
                            StartCoroutine(InstantiateMedals(temp.transform, position, i));
                        }
                    }
                    position++;
                    numberOfMedal--;
                }
            };
        }
    }
    private IEnumerator InstantiateMedals(Transform t, int position, int p)
    {
        yield return new WaitForSeconds(p);
        GameObject temp2 = Instantiate(medals[Mathf.Abs(position)], t);
        Tween a = temp2.transform.DOScale(new Vector3(1.1f, 1.1f), 0.5f);
        Tween b = temp2.transform.DOScale(new Vector3(1, 1), 0.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(a).Append(b).SetLoops(-1);
        PlayerManager.instance.players[position].medals.Add(medals[Mathf.Abs(position)]);
    }

    public void ReloadScene()
    {
        Array.Clear(PlayerManager.instance.PlayersInterface, 0, PlayerManager.instance.PlayersInterface.Length);

        bool isEnd = false;
        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            if (PlayerManager.instance.players[i].medals.Count >= GameManager.instance.PointToWin)
                isEnd = true;
        }
        GameManager.instance.GetComponent<MonoBehaviour>().StartCoroutine(ReloadSceneStart(isEnd));
    }

    private IEnumerator ReloadSceneStart(bool isEnd)
    {
        CameraManager.Instance.AnimTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            PlayerManager.instance.players[i].HideGuy(true);
            PlayerManager.instance.players[i].ActualPlayerState = PlayerState.FIGHTING;
        }

        if (isEnd)
            SceneManager.LoadSceneAsync(GameManager.instance.LeaderBoardScene);
        else
            SceneManager.LoadScene(GameManager.instance.NextSceneID);
    }
}
