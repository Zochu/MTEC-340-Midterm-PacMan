using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    [SerializeField] protected int points = 1;

    [SerializeField] LayerMask pacman;

    [SerializeField] AudioClip smallCandy;

    protected virtual void Eat()
    {
        GameBehaviour.Instance.EatPallet(this, points);
        AudioBehaviour.Instance.PlayASound(smallCandy, 0.5f, Random.Range(0.5f, 1.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != pacman)
        {
            Eat();
        }
}

}
