using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Button startButton;
    [SerializeField] string sceneName;
    [SerializeField] BaseEventData hilight;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip select;

    private void Start()
    {
        startButton.onClick.AddListener(onClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioBehaviour.Instance.PlayASound(hover, 1, 1.5f);
    }

    void onClick()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        AudioBehaviour.Instance.PlayASound(select, 1, 1);
    }
}
