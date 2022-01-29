using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameDatas
{
    public string radioText;
    public string letterText;
}

[System.Serializable]
public class datalist
{
    public GameDatas[] GameData = {};
}

public class GameFlagManager : MonoBehaviour
{
    private void Start()
    {
        readGameData(0);
    }

    private void readGameData (int day){
        //read and parse json data according to date(as row)
        string path;
        string jsonString;
        path = Application.dataPath + "/scripts/gameData.json";
        jsonString = File.ReadAllText (path); 
        Debug.Log("Game data path :" + path);

        datalist gameDatas = JsonUtility.FromJson<datalist> (jsonString);
        print(gameDatas.GameData.Length);
        foreach(GameDatas data in gameDatas.GameData) {
            print(data.radioText);
        }
    }


    //constructor
    public GameFlagManager(int day, int evilPoint, int sonPoint, string radioText, string letterText, string gameText, List<string> gameFlags, string specialEvent, bool decision)
    {
        this.day = day;
        this.evilPoint = evilPoint;
        this.sonPoint = sonPoint;
        this.radioText = radioText;
        this.letterText = letterText;
        this.gameText = gameText;
        this.gameFlags = gameFlags;
        this.specialEvent = specialEvent;
        this.decision = decision;
    }
    
    //init
    public int day;//Player needs to make decision to pass a day
    public int evilPoint;
    public int sonPoint;
    public string radioText;//First Priority, then play random songs
    public string letterText;
    public string gameText;//Show on notebook, player make decision
    public List<string> gameFlags;//A list of game flags
    public string specialEvent;
    public bool decision;
    
    //getter, setter
    public int Day
    {
        get => day;
        set
        {
            day = value;
            readGameData(day);
        }
    }

    public int EvilPoint
    {
        get => evilPoint;
        set => evilPoint = value;
    }

    public int SonPoint
    {
        get => sonPoint;
        set => sonPoint = value;
    }

    public string RadioText
    {
        get => radioText;
        set => radioText = value;
    }

    public string LetterText
    {
        get => letterText;
        set => letterText = value;
    }

    public string GameText
    {
        get => gameText;
        set => gameText = value;
    }

    public List<string> GameFlags
    {
        get => gameFlags;
        set => gameFlags = value;
    }

    public string SpecialEvent
    {
        get => specialEvent;
        set => specialEvent = value;
    }

    public bool Decision
    {
        get => decision;
        set => decision = value;
    }


}
