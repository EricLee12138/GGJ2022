using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Choice : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] StoryManager storyManager;
    [SerializeField] bool yes;

    TMP_Text text;

    Color Green = new Color(0.12f, 0.56f, 0.10f);

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        text.color = Green;
    }

    public void OnPointerExit(PointerEventData data)
    {
        text.color = Color.black;
    }

    public void OnPointerClick(PointerEventData data)
    {
        text.color = Color.black;
        storyManager.Choose(yes);
    }
}
