using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKing : Ghost
{
    [SerializeField] KeyCode speedUpKey;
    [SerializeField] float speedUpTime;
    [SerializeField] float speedUpChargeTime;
    [SerializeField] int kingCandyScore;

    private bool speedUpCharging;

    protected override void Update()
    {
        if (GameBehaviour.Instance.state == State.Power)
        {
            body.gameObject.SetActive(false);
            eye.gameObject.SetActive(false);
            fear.gameObject.SetActive(true);
        }
        else
        {
            body.gameObject.SetActive(true);
            eye.gameObject.SetActive(true);
            fear.gameObject.SetActive(false);
        }

        if (Input.GetKeyUp(speedUpKey) && speedUpCharging == false)
        {
            StartCoroutine(SpeedUP());
        }

    }

    IEnumerator SpeedUP()
    {
        speedUpCharging = true;
        this.movement.speedUp = 1.3f;
        yield return new WaitForSeconds(speedUpTime);
        this.movement.speedUp = 1.001f;
        yield return new WaitForSeconds(speedUpChargeTime);
        this.movement.speedUp = 1.0f;
        speedUpCharging = false;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pacman"))
        {
            if (GameBehaviour.Instance.state == State.Power)
            {
                AudioBehaviour.Instance.PlayASound(ghostCandy, 0.5f, Random.Range(0.5f, 1.5f));
                GameBehaviour.Instance.EatGhost(this);
                GameBehaviour.Instance.SetScore(kingCandyScore);
                GameBehaviour.Instance.SetKingLive(GameBehaviour.Instance.ghostKingLive - 1);
                GameBehaviour.Instance.KingDie();
                if (GameBehaviour.Instance.ghostKingLive > 0)
                {
                    for (int i = 0; i < GameBehaviour.Instance.ghost.Length; i++)
                    {
                        GameBehaviour.Instance.ghost[i].gameObject.SetActive(false);
                    }
                    AudioBehaviour.Instance.PlayASound(ghostCandy, 0.5f, Random.Range(0.5f, 1.5f));
                    Invoke(nameof(KingBack), respawnTime);
                }
                else
                {
                    GameBehaviour.Instance.GameOver();
                }
            }
            else
            {
                GameBehaviour.Instance.PacManDie();
                AudioBehaviour.Instance.PlayASound(pacmanCandy, 0.5f, Random.Range(0.9f, 1.1f));
            }
        }
    }

    public void KingBack()
    {
        //Debug.Log("KingBack");
        this.gameObject.SetActive(true);
        this.movement.SetDir(Vector2.zero);
        this.transform.position = new Vector3(0.0f, -1.0f, -4.5f);
        for (int i = 0; i < GameBehaviour.Instance.ghost.Length; i++)
        {
            GameBehaviour.Instance.ghost[i].Invoke(nameof(GhostBack), GameBehaviour.Instance.ghostRespawnTime);
        }

    }

    
}
