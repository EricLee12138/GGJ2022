using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photoManager : MonoBehaviour
{
  public GameObject photoBlur;
  public float alpha = 0f;//photo opacity
  public float blurIncrement = 0.02f;
  public GameObject gameManager;
  private SpriteRenderer colorControl;
  public int date = 0;
  private void Start() {
    colorControl = photoBlur.GetComponent<SpriteRenderer>();
    setAlpha(alpha);
  }

  private void setAlpha(float _alpha)
  {    
    colorControl.color = new Color(colorControl.color.r,colorControl.color.g,colorControl.color.b, _alpha );

  }

  private void Update() {
    //update blur according to date
    string letterText = "";
    int currentDate = gameManager.GetComponent<GameFlagManager>().day;
    
    if (currentDate != date) {
      alpha += blurIncrement;
      letterText = gameManager.GetComponent<GameFlagManager>().letterText;
      date = currentDate;
    }

    bool isSon = (letterText.IndexOf("Dear mom,")!=-1);
    if (isSon) {
      alpha = 0f;
    }
    setAlpha(alpha);
  }
}
