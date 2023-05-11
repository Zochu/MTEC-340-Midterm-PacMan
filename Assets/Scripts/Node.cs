using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    private List<Vector2> direction = new List<Vector2>();
    [SerializeField] LayerMask obLayer;

    void Start()
    {
        if (IfYouCanGo(Vector2.up))
        {
            this.direction.Add( Vector2.up);
            //Debug.Log("up available");
        }

        if (IfYouCanGo(Vector2.down))
        {
            this.direction.Add(Vector2.down);
            //Debug.Log("down available");
        }

        if (IfYouCanGo(Vector2.left))
        {
            this.direction.Add( Vector2.left);
            //Debug.Log("left available");
        }

        if (IfYouCanGo(Vector2.right))
        {
            this.direction.Add(Vector2.right);
            //Debug.Log("right available");
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("yes");
        if (GameBehaviour.Instance.state != State.Pause)
        {
            if (GameBehaviour.Instance.state == State.Normal)
            {
                Vector2 ghostDirection = -collision.gameObject.GetComponent<Movement>().direction;
                var filteredList = this.direction.Where(dir => dir != ghostDirection).ToList();
                int randomIndex = new System.Random().Next(0, filteredList.Count());
                Vector2 newDirection = filteredList[randomIndex];
                collision.gameObject.GetComponent<Movement>().SetDir(newDirection);
                //Debug.Log(newDirection);
            }
            else if (GameBehaviour.Instance.state == State.Chase)
            {

                //Debug.Log("Chase!!!!");
                Vector2 newDirection = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 direction in this.direction)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                    float distance = (collision.gameObject.GetComponent<Ghost>().pacman.position - newPosition).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        newDirection = direction;
                        minDistance = distance;
                    }
                }
                collision.gameObject.GetComponent<Movement>().SetDir(newDirection);
            }
            else if (GameBehaviour.Instance.state == State.Power)
            {
                Vector2 newDirection = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 direction in this.direction)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                    float distance = (collision.gameObject.GetComponent<Ghost>().pacman.position - newPosition).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        var filteredList = this.direction.Where(dir => dir != new Vector2 (newPosition.x, newPosition.y)).ToList();
                        int randomIndex = new System.Random().Next(0, filteredList.Count());
                        newDirection = filteredList[randomIndex];
                    }
                }
                collision.gameObject.GetComponent<Movement>().SetDir(newDirection);
            }
            else if (GameBehaviour.Instance.state == State.Protect)
            {
                Vector2 newDirection = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 direction in this.direction)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(direction.x, direction.y, 0.0f);
                    float distance = (collision.gameObject.GetComponent<Ghost>().king.position - newPosition).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        newDirection = direction;
                        minDistance = distance;
                    }
                }
                collision.gameObject.GetComponent<Movement>().SetDir(newDirection);
            }
        }
        

    }

    bool IfYouCanGo(Vector2 dir)
    {
        Vector2 area = new Vector2(0.5f, 0.5f);
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, area, 0.0f, dir, 1.0f, obLayer);
        //RaycastHit2D hit = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y) + dir);
        if (hit.collider != null && hit.collider.CompareTag("Ob"))
        {
            //Debug.Log("Walll!!!");
            return false;
        }
        else
        {
            return true;
        }
    }
}

