using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ReadyButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Button pacManButton;
    [SerializeField] Button kingButton;
    [SerializeField] string sceneName;
    [SerializeField] bool pacManReady = false;
    [SerializeField] bool kingReady = false;

    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip select;

    private void Start()
    {
        pacManButton.onClick.AddListener(PacmanClick);
        kingButton.onClick.AddListener(KingClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioBehaviour.Instance.PlayASound(hover, 1, 1.5f);
    }

    void PacmanClick()
    {
        pacManReady = true;
        AudioBehaviour.Instance.PlayASound(select, 1, 1);
    }

    void KingClick()
    {
        kingReady = true;
        AudioBehaviour.Instance.PlayASound(select, 1, 1);
    }

    private void Update()
    {
        if (pacManReady == true & kingReady == true)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
