using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowYourScoreMultiPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pacManScore;
    [SerializeField] TextMeshProUGUI kingScore;

    protected virtual void Start()
    {
        pacManScore.text = "Pac-Man Score = " + GameBehaviour.Instance.score.ToString();
        kingScore.text = "Ghost King Score = " + GameBehaviour.Instance.ghostKingScore.ToString();
    }
}
