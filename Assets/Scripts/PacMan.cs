using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{
    public Movement movement { get; private set; }

    //SpriteAnimation spriteAnimation;
    [SerializeField] KeyCode up;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;

    //[SerializeField] Rigidbody2D pacmanPhysics;
    //[SerializeField] SpriteRenderer pacmanVisible;

    [SerializeField] bool rotate;
    //[SerializeField] float robustTime;

    private void Awake()
    {
        //pacmanVisible = GetComponent<SpriteRenderer>();
        //pacmanPhysics = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(up))
            movement.SetDir(Vector2.up);
        else if (Input.GetKeyDown(down))
            movement.SetDir(Vector2.down);
        else if (Input.GetKeyDown(left))
            movement.SetDir(Vector2.left);
        else if (Input.GetKeyDown(right))
            movement.SetDir(Vector2.right);

        if (rotate == true)
        {
            if (movement.direction == Vector2.up)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            else if (movement.direction == Vector2.down)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            else if (movement.direction == Vector2.left)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            else if (movement.direction == Vector2.right)
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        
    }

    public void Initialize() 
    {
        movement.ResetRound();
        gameObject.SetActive(true);
        //this.spriteAnimation.ResetFrame();
    }

    //public void HeroRespawn()
    //{
    //    StartCoroutine(Robust());
    //    StartCoroutine(HeroBlink());
    //}

    //IEnumerator Robust()
    //{
    //    Physics2D.IgnoreLayerCollision(7, 8, true);
    //    yield return new WaitForSeconds(robustTime);
    //    Physics2D.IgnoreLayerCollision(7, 8, false);
    //}

    //IEnumerator HeroBlink()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = false;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = true;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = false;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = true;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = false;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = true;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = false;
    //    yield return new WaitForSeconds(0.5f);
    //    pacmanVisible.enabled = true;
    //}

}
