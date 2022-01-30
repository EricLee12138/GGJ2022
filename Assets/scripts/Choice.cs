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

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        text.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData data)
    {
        text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerClick(PointerEventData data)
    {
        text.fontStyle = FontStyles.Normal;
        storyManager.Choose(yes);
    }
}
