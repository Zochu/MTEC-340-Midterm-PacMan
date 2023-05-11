using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] Button _button;
    [SerializeField] AudioClip hover;

    private void Start()
    {
        _button.onClick.AddListener(onClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioBehaviour.Instance.PlayASound(hover, 1, 1.5f);
    }

    void onClick()
    {
        Application.Quit();
    }
}
