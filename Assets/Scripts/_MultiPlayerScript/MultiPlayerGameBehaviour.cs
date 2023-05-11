using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiPlayerGameBehaviour : GameBehaviour
{
    [SerializeField] KeyCode normalCommand;
    [SerializeField] KeyCode chaseCommand;
    [SerializeField] KeyCode protectCommand;
    [SerializeField] Rigidbody2D pacmanPhysics;
    [SerializeField] SpriteRenderer pacmanVisible;
    [SerializeField] float robustTime;

    protected override void Start()
    {
        pacmanVisible = GameObject.Find("PacMan").GetComponent<SpriteRenderer>();
        pacmanPhysics = GameObject.Find("PacMan").GetComponent<Rigidbody2D>();
        ResetGame();
        state = State.Normal;
        ghostKingLive = 3;
        kingScoreGUI.enabled = true;
        kingLiveGUI.enabled = true;
    }

    private void Update()
    {
        if (state != State.Power)
        {
            if (Input.GetKeyUp(normalCommand))
                state = State.Normal;
            if (Input.GetKeyUp(chaseCommand))
                state = State.Chase;
            if (Input.GetKeyUp(protectCommand))
                state = State.Protect;
        }
        ghostKingScore = -score;
        SetKingScore(ghostKingScore);
    }

    public override void PacManDie()
    {
        base.PacManDie();
        if (live >0)
            Invoke(nameof(HeroRespawn), 3.0f);
    }

    private void HeroRespawn()
    {
        StartCoroutine(Robust());
        StartCoroutine(HeroCloseEye(0));
    }

    IEnumerator Robust()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 12, true);
        yield return new WaitForSeconds(robustTime);
    }

    IEnumerator HeroCloseEye(int i)
    {
        yield return new WaitForSeconds(0.2f);
        pacmanVisible.enabled = false;
        StartCoroutine(HeroOpenEye(i));
    }

    IEnumerator HeroOpenEye(int i)
    {
        yield return new WaitForSeconds(0.2f);
        pacmanVisible.enabled = true;
        if (i <= 5)
        {
            i += 1;
            StartCoroutine(HeroCloseEye(i));
        }
        else
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
            Physics2D.IgnoreLayerCollision(6, 8, false);
            Physics2D.IgnoreLayerCollision(6, 12, false);
        }
    }
}