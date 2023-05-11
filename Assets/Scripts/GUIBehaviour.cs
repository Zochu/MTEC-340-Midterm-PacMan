using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIBehaviour : MonoBehaviour
{
    public static GUIBehaviour Instance;

    [SerializeField] GameObject normalState;
    [SerializeField] GameObject chaseState;
    [SerializeField] GameObject powerState;
    [SerializeField] GameObject pacManDie;
    [SerializeField] GameObject ghostKingDie;
    [SerializeField] GameObject pause;

    [SerializeField] TextMeshProUGUI normal;
    [SerializeField] TextMeshProUGUI chase;
    [SerializeField] TextMeshProUGUI power;


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

    private void Start()
    {
        ResetAll();
    }

    private void LateUpdate()
    {
        if (GameBehaviour.Instance.state == State.Normal || GameBehaviour.Instance.state == State.Protect && normalState.activeSelf == false)
        {
            ResetText();
            StopAllCoroutines();
            StartCoroutine(StateChange(normalState, normal, GameBehaviour.Instance.normalTime - 3.0f));
        }
        else if (GameBehaviour.Instance.state == State.Chase && chaseState.activeSelf == false)
        {
            ResetText();
            StopAllCoroutines();
            StartCoroutine(StateChange(chaseState, chase, GameBehaviour.Instance.chaseTime - 3.0f));
        }
        else if (GameBehaviour.Instance.state == State.Power && powerState.activeSelf == false)
        {
            ResetText();
            StopAllCoroutines();
            StartCoroutine(StateChange(powerState, power, 5.0f));
        }
    }

    void ResetText()
    {
        chaseState.SetActive(false);
        powerState.SetActive(false);
        normalState.SetActive(false);
    }

    void ResetAll()
    {
        ResetText();
        pacManDie.SetActive(false);
        ghostKingDie.SetActive(false);
        pause.SetActive(false);
    }

    public void PauseOrNot(bool p)
    {
        if (p == true)
            pause.SetActive(true);
        else if (p == false)
            pause.SetActive(false);
    }

    public IEnumerator PacManDied()
    {
        pacManDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        pacManDie.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        pacManDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        pacManDie.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        pacManDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        pacManDie.SetActive(false);
    }

    public IEnumerator GhostKingDied()
    {
        ghostKingDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ghostKingDie.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ghostKingDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ghostKingDie.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ghostKingDie.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ghostKingDie.SetActive(false);
    }

    IEnumerator StateChange(GameObject state, TextMeshProUGUI text, float time)
    {
        state.SetActive(true);
        text.enabled = true;
        yield return new WaitForSeconds(time);
        text.enabled = false;
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        text.enabled = false;
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        text.enabled = false;
        yield return new WaitForSeconds(0.5f);
        text.enabled = true;
    }
}
