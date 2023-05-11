using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{

    [SerializeField] float speed = 5.0f;
    [SerializeField] KeyCode up;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    Vector2 dest;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(up) && valid(Vector2.up))
                dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(right) && valid(Vector2.right))
                dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(down) && valid(-Vector2.up))
                dest = (Vector2)transform.position + Vector2.down;
            if (Input.GetKey(left) && valid(-Vector2.right))
                dest = (Vector2)transform.position + Vector2.left;
        }

        // Animation Parameters
        dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }

    private bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
