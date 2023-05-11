using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField] float powerTime = 8.0f;
    [SerializeField] float powerResetTime = 10.0f;
    [SerializeField] LayerMask pacman;
    [SerializeField] int points;
    [SerializeField] Collider2D _collider;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] AudioClip powerCandy;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != pacman)
        {
            Eat();
        }
    }

        private void Eat()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        AudioBehaviour.Instance.PlayASound(powerCandy, 0.5f, Random.Range(0.5f, 1.5f));
        //GameBehaviour.Instance.Power(this, points * 5);
        GameBehaviour.Instance.state = State.Power;
        StartCoroutine(PowerTime());
    }

    IEnumerator PowerTime()
    {
        yield return new WaitForSeconds(powerTime);
        GameBehaviour.Instance.state = State.Normal;
        StartCoroutine(ResetPower());
    }

    IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(powerResetTime);
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
    }

}
