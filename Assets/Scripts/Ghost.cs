using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement;
    [SerializeField] protected bool home;
    [SerializeField] float homeTime;
    [SerializeField] protected float respawnTime;
    [SerializeField] protected Vector3 homePosition;
    [SerializeField] Vector3 goTripPosition;
    [SerializeField] Vector2 homeDirection;
    [SerializeField] Vector2 myFace;
    [SerializeField] protected Transform body;
    [SerializeField] protected Transform eye;
    [SerializeField] protected Transform fear;
    [SerializeField] protected AudioClip ghostCandy;
    [SerializeField] protected AudioClip pacmanCandy;


    public LayerMask obLayer;
    public LayerMask pacmanLayer;

    public Transform pacman;
    public Transform king;


    private void Awake()
    {
        movement = GetComponent<Movement>();
        body = this.gameObject.transform.GetChild(0);
        eye = this.gameObject.transform.GetChild(1);
        fear = this.gameObject.transform.GetChild(3);
    }

    protected void Start()
    {
        Initialize();
        if (home == true)
        {
            this.movement.SetDir(new Vector2(Random.Range(-1, 1) < 0 ? -1.0f : 1.0f, 0.0f));
            StartCoroutine(WaitInHome());
        }
    }

    protected IEnumerator WaitInHome()
    {
        //Debug.Log("Waiting In Home.....");
        //Debug.Log("WaitInHome coroutine started for ghost: " + gameObject.name);
        yield return new WaitForSeconds(this.homeTime);
        StartCoroutine(GoTrip());
    }

    protected IEnumerator GoTrip()
    {
        //Debug.Log("Let's Take a Trip");
        float initialPos = 0.0f;
        float targetPos = 0.5f;

        while (initialPos < targetPos)
        {
            Vector3 nextPos = Vector3.Lerp(this.transform.position, homePosition, initialPos / targetPos);
            this.transform.position = nextPos;
            initialPos += Time.deltaTime;
            yield return null;
        }

        initialPos = 0.0f;
        home = false;

        while (initialPos < targetPos)
        {
            Vector3 nextPos = Vector3.Lerp(homePosition, goTripPosition, initialPos / targetPos);
            this.transform.position = nextPos;
            initialPos += Time.deltaTime;
            yield return null;
        }

        Vector2 letsGo = new Vector2(Random.Range(-1, 1) < 0 ? -1 : 1, 0);
        this.movement.SetDir(letsGo);
        initialPos = 0.0f;
    }

    public void Initialize()
    {
        movement.ResetRound();
        gameObject.SetActive(true);
        if (home == true)
        {
            this.movement.SetDir(new Vector2(Random.Range(-1, 1) < 0 ? -1.0f : 1.0f, 0.0f));
            StartCoroutine(WaitInHome());
        }
    }

    protected virtual void Update()
    {
        if(GameBehaviour.Instance.state == State.Power)
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

        myFace = this.movement.direction;

        if (ISeePacMan(myFace) == true && GameBehaviour.Instance.state != State.Power)
        {
            //Debug.Log("I See You!!!!");
            this.movement.speedUp = 1.3f;
        }
        else
        {
            this.movement.speedUp = 1.0f;
        }
    }

    bool ISeePacMan(Vector2 dir)
    {
        Vector2 area = new Vector2(1.0f, 1.0f);
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, area, 0.0f, dir, 10.0f, pacmanLayer);
        //RaycastHit2D hit = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y) + dir, 20);
        if (hit.collider != null && hit.collider.CompareTag("Pacman"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pacman"))
        {
            if (GameBehaviour.Instance.state == State.Power)
            {
                GameBehaviour.Instance.EatGhost(this);
                AudioBehaviour.Instance.PlayASound(ghostCandy, 0.5f, Random.Range(0.5f, 1.5f));
                Invoke(nameof(GhostBack), GameBehaviour.Instance.ghostRespawnTime);
            }
            else
            {
                GameBehaviour.Instance.PacManDie();
                AudioBehaviour.Instance.PlayASound(pacmanCandy, 0.5f, Random.Range(0.9f, 1.1f));
            }
        }

        if (this.home == true)
        {
            if (col.gameObject.CompareTag("Ob") || col.gameObject.CompareTag("Door"))
            {
                this.movement.SetDir(-this.movement.direction);
            }
        }


    }


    public virtual void GhostBack()
    {
        this.gameObject.SetActive(true);
        this.home = true;
        this.transform.position = new Vector3(0.0f, -1.0f, -4.5f);
        StartCoroutine(WaitInHome());
    }

    //protected void SoldierBack()
    //{
    //    Debug.Log("SoldierBack: " + gameObject.name);
    //    gameObject.SetActive(true);
    //    for (int i = 0; i < GameBehaviour.Instance.ghost.Length; i++)
    //    {
    //        GameBehaviour.Instance.ghost[i].Initialize();
    //    }
    //}

}
