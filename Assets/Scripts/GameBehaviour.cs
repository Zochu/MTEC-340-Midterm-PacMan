using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour Instance;

    public Ghost[] ghost;
    public PacMan pacman;

    public Transform pallet;
    public int score { get; private set; }
    public int ghostKingScore { get; protected set; }
    public int live{ get; private set; }
    public int ghostKingLive { get; protected set; }
    public int ghostPoints = 50;
    public float ghostRespawnTime;
    public State state;
    private State curretnState;

    [SerializeField] TextMeshProUGUI scoreGUI;
    [SerializeField] TextMeshProUGUI livesGUI;
    [SerializeField] protected TextMeshProUGUI kingScoreGUI;
    [SerializeField] protected TextMeshProUGUI kingLiveGUI;
    [SerializeField] string gameOverScene;

    public float normalTime = 10.0f;
    public float chaseTime = 10.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    protected virtual void Start()
    {
        ResetGame();
        StartCoroutine(StateNoramal());
        kingScoreGUI.enabled = false;
        kingLiveGUI.enabled = false;
    }

    IEnumerator StateNoramal()
    {
        if(state == State.Power)
        {
            state = State.Power;
        }
        else
        {
            state = State.Normal;
        }
        curretnState = state;
        yield return new WaitForSeconds(normalTime);
        StartCoroutine(StateChase());
    }

    IEnumerator StateChase()
    {
        if(state == State.Power)
        {
            state = State.Power;
        }
        else
        {
            state = State.Chase;
        }
        curretnState = state;
        yield return new WaitForSeconds(chaseTime);
        StartCoroutine(StateNoramal());
    }


    private void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                GUIBehaviour.Instance.PauseOrNot(false);
            }
            else if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                GUIBehaviour.Instance.PauseOrNot(true);
            }
        }
        

    }

    protected void ResetGame()
    {
        SetScore(0);
        SetLive(3);
        NewRound();
    }

    private void ResetPacMan()
    {
        pacman.Initialize();
    }

    private void ResetGhost()
    {
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].Initialize();
        }
    }

    private void ResetPallet()
    {
        foreach (Transform p in pallet)
        {
            p.gameObject.SetActive(true);
        }
    }

    private void NewRound()
    {
        ResetPallet();
        ResetGhost();
        ResetPacMan();
    }

    public void SetScore(int _score)
    {
        score = _score;
        scoreGUI.text = score.ToString();
    }

    protected void SetKingScore(int _score)
    {
        ghostKingScore = _score;
        kingScoreGUI.text = ghostKingScore.ToString();
    }

    private void SetLive(int _live)
    {
        live = _live;
        livesGUI.text = "= " + live.ToString();
    }

    public void SetKingLive(int _live)
    {
        ghostKingLive = _live;
        kingLiveGUI.text = "= " + ghostKingLive.ToString();
    }

    public void EatGhost(Ghost g)
    {
        g.gameObject.SetActive(false);
        SetScore(score + ghostPoints);
    }

    public void EatPallet(Pallet p, int point)
    {
        p.gameObject.SetActive(false);
        SetScore(score + point);
        if (CheckPallet() == true)
        {
            Invoke(nameof(GameOver), 1.0f);
        }
    }

    bool CheckPallet()
    {
        foreach (Transform p in pallet)
        {
            if (p.gameObject.activeSelf == true)
            {
                return false;
            }
        }

        return true;
    }

    public virtual void PacManDie()
    {
        pacman.gameObject.SetActive(false);
        SetLive(live - 1);
        SetScore(score - 100);

        if (live > 0)
        {
            Invoke(nameof(ResetPacMan), 3.0f);
            // Invoke(nameof(ResetGhost), 1.0f);
            StartCoroutine(GUIBehaviour.Instance.PacManDied());
        }
        else
        {
            Invoke(nameof(GameOver),1.0f);
        }

    }

    public void KingDie()
    {
        //if (ghostKingLive > 0)
        //{
        //    for (int i = 0; i < ghost.Length; i++)
        //    {
        //        ghost[i].gameObject.SetActive(false);
        //        Invoke(nameof(Ghost.GhostBack), ghostRespawnTime * 2);
        //    }
            StartCoroutine(GUIBehaviour.Instance.GhostKingDied());
        //    Invoke(nameof(GhostKing.KingBack), ghostRespawnTime);
        //}
        //else
        //{
        //    GameOver();
        //}
    }

    public void GameOver()
    {
        pacman.gameObject.SetActive(false);

        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].gameObject.SetActive(false);
        }
        
        Invoke(nameof(LoadGameOverScene), 1.0f);
    }

    private void LoadGameOverScene()
    {
        SoundTrackBehaviour.Instance.playIt.Stop();
        SceneManager.LoadScene(gameOverScene, LoadSceneMode.Single);
    }
}
