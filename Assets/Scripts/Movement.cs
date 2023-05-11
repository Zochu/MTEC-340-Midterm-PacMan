using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body { get; private set; }

    public float speed = 7.0f;
    public float speedUp = 1.0f;
    public Vector2 initialDirection;
    public Vector2 direction;
    public Vector2 nextDirection { get; private set; }
    public Vector3 initialPosition { get; private set; }
    public LayerMask obLayer;

    private void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Start()
    {
        ResetRound();
    }

    private void Update()
    {
        if(nextDirection != Vector2.zero)
        {
            SetDir(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        //if(GameBehaviour.Instance.state != State.Pause)
        //{
        Vector2 position = body.position;
        Vector2 translation = direction * speed * speedUp * Time.fixedDeltaTime;
        this.body.MovePosition(position + translation);
        //}
        ////else
        ////{
        ////    Vector2 position = body.position;
        ////    this.body.MovePosition(position + Vector2.zero);
        ////}
        
    }

    public void ResetRound()
    {
        speed = 7.0f;
        speedUp = 1.0f;
        direction = initialDirection;
        transform.position = initialPosition;
        nextDirection = Vector2.zero;
        body.isKinematic = false;
        enabled = true;

    }

    public void SetDir(Vector2 dir)
    {
        if (Valid(dir) == true)
        {
            nextDirection = dir;  
        }
        else
        {
            direction = dir;
            nextDirection = Vector2.zero;
        }
    }

    public bool Valid(Vector2 dir)
    {
        Vector2 area = new Vector2(0.5f, 0.5f);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, area, 0.0f, dir, 1.5f, obLayer);
        //Debug.Log("Valid hit: " + hit.collider);
        return (hit.collider != null);
    }


}
