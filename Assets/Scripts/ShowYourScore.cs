using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowYourScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI yourScore;

    protected virtual void Start()
    {
        yourScore.text = "Your Score = " + GameBehaviour.Instance.score.ToString();
    }
}
