using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] sprites;

    public float frameTime = 0.25f;
    public int spriteIndex { get; private set; }

    private bool isCoroutineRunning = false;

    State currentState;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;
            StartCoroutine(Wait());
        }
    }

    private void Start()
    {
        SpriteUpdate();
    }

    private void SpriteUpdate()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
            spriteIndex = 0;
        spriteRenderer.sprite = sprites[spriteIndex];
        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(frameTime);
        SpriteUpdate();
    }

    public void OnEnable()
    {
        spriteIndex = 0;
        SpriteUpdate();
    }
}
