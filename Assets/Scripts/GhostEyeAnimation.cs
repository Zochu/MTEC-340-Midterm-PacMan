using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyeAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    [SerializeField] Sprite[] sprites;
    [SerializeField] int initialSprite;
    private Movement movement;


    private void Awake()
    {
        movement = GetComponentInParent<Movement>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[initialSprite];
    }

    private void Update()
    {
        if (this.movement.direction == Vector2.up)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if (this.movement.direction == Vector2.down)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (this.movement.direction == Vector2.left)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if (this.movement.direction == Vector2.right)
        {
            spriteRenderer.sprite = sprites[3];
        }
    }
}
