using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class HoverOutline : MonoBehaviour
{

    Color White = new Color(.8f, .8f, .8f);
    Color Black = new Color(0, 0, 0);
    
    Color color = new Color(0, 0, 0);
    Color TargetColor = new Color(0, 0, 0);
    
    void Update()
    {
        color = Vector4.MoveTowards(color, TargetColor, 3f * Time.deltaTime);
        GetComponent<SpriteRenderer>().material.SetColor("_Color", color);
    }

    void OnMouseEnter()
    {
        TargetColor = White;
    }

    void OnMouseExit()
    {
        TargetColor = Black;
    }
}
