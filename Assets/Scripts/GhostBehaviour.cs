using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public static GhostBehaviour Instance;

    public Ghost ghost;

    public float time;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        ghost = GetComponent<Ghost>();

        enabled = false;
    }

    public void Enable()
    {
        EnableForTime(time);
    }

    public virtual void EnableForTime(float time)
    {
        enabled = true;
        StopCoroutine("WaitForSeconds");
        StartCoroutine(WaitForTime(time));
    }

    public virtual void Disable()
    {
        enabled = false;
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        Disable();
    }
}
