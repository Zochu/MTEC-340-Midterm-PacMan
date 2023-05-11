using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletSpeedUp : Pallet
{

    Movement movement;
    [SerializeField] float speedUpTime;

    [SerializeField] AudioClip bigCandy;

    private void Awake()
    {
        movement = GameObject.Find("PacMan").GetComponent<Movement>();
    }
    protected override void Eat()
    {
        GameBehaviour.Instance.EatPallet(this, points);
        movement.speedUp = 1.3f;
        AudioBehaviour.Instance.PlayASound(bigCandy, 0.5f, Random.Range(0.5f, 1.5f));
        Invoke(nameof(ResetSpeed), speedUpTime);
    }

    void ResetSpeed()
    {
        movement.speedUp = 1.0f;
    }
}
