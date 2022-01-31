using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class GameData
{
    public string newsText;//everyday newspaper, do not contain decision
    public string newsTitle;//everyday newspaper, do not contain decision
    public string gameText;
    public string radioText;
    public string letterText;
    public int[] yes;
    public int[] no;
    public string conditionSpecialEvent;
    public string conditionRadio;
    public string specialEventFlag; //special event flag
    public string specialEventBody;
    public string decisionFrom;//letter, radio, special
    public string choiceA;
    public string choiceB;
}

[System.Serializable]
public class DataList {
    public GameData[] GameData = {};
}

public class GameFlagManager : MonoBehaviour {
    private void Start() {
        ///initialize
        readGameData(0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Pressed primary button.");
            dayPass();
        }
    }

    private void OnDrawGizmos() {
        ///debug
        // readGameData(0);
    }

    //public string jsonString;


    private void readGameData (int day){
        //read and parse json data according to date(as row)
        //string path;
        string webGL = "{\n  \"GameData\": [\n    {\n      \"newsTitle\": \"War Looms as Tensions Rise\",\n      \"newsText\": \"War has broken out on the peninsula! The government has ordered all men of age to aid in our war against tyranny and cruelty. We are sending our boys to war but they will come back as heroes. \",\n      \"decisionFrom\": \"letter\",\n      \"gameText\": \"Dear Diary,\\n\\nI'm fighting over the decision to steal from my neighbors since they had such a great harvest this season. What should I do? \",\n      \"radioText\": \"Help your men at battle! Send a taste of home with what ever food you can to our boys on the front lines and remind them what they're fighting for!\",\n      \"letterText\": \"Howdy Delores,\\n\\nHope all is well with you and your boy, Decker. He's fighting the good fight for all of us. Did you get to plant anything without his help?\\nWe just had a great season with our garden. I think we'll have just enough to get us through this summer.\\n\\nThinking of you,\\nThe Fishers\",\n      \"specialEventFlag\": \"isCornSteal\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"\",\n      \"choiceA\": \"I stole from my neighbors field today, it will help nourish my son who's on the battlefield.\",\n      \"choiceB\": \"I decided not to steal from my neighbors, I can sleep with a sound conscious that my neighbors will have enough food for themselves.\"\n    },\n\n    {\n      \"newsTitle\": \"Food Shortage for Our Men at Arms\",\n      \"newsText\": \"As our men go to battle, we need to fill their bellies so they can fight our enemies. The government asks for your help in sending everything you can to our boys in battle.\",\n      \"decisionFrom\": \"radio\",\n      \"gameText\": \"Dear Diary,\\n\\nI'm only able to make enough to barely feed myself but I know my boy is hungry out on the front lines. I'd hate to see him starve or get injured or worse from lack of nourishment. Is it right to steal from a family store to feed my son?\",\n      \"radioText\": \"Hello there neighbor, stop by Johnson's Family Store where we just stocked up on all your canned good needs. And don't forget to try our new family recipe baked goods, it's like shopping at our home! Hope to see you there!\",\n      \"letterText\": \"\",\n      \"specialEventFlag\": \"isNeighborSteal\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"\",\n      \"choiceA\": \"I took some canned food from the store today, my son will be able have the strength to fight for all of us.\",\n      \"choiceB\": \"I was at the store but was unable to steal anything for my son, I hope he doesn't starve out there.\"\n    },\n\n    {\n      \"newsTitle\": \"Sick of Fighting\",\n      \"newsText\": \"A strain of influenza has come over some of the soldiers at war. Their conditions are not the most ideal so they are fighting with what they can but some have not been able to avoid this sickness. Hopefully our men will pull through.\",\n      \"decisionFrom\": \"letter\",\n      \"gameText\": \"Dear Diary,\\n\\nI'm thinking of faking an ailment to get access to some medical supplies that my son desperately needs. Should I go to the hospital or not?\",\n      \"radioText\": \"\",\n      \"letterText\": \"Hello mom,\\n\\nI haven't been feeling well as of late. I've caught something from being stuck in the cold and wet trenches with my worn out boots, but I am resolved to regain my health and keep fighting for you and the rest of our countrymen. \\n\\nThinking of you often.\\nLove, Decker\",\n      \"specialEventFlag\": \"isHelpNeighbor\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"There's a note from the local hospital \",\n      \"choiceA\": \"I went to the hospital and stole some medicine for my son, I'll sleep well knowing he'll recover faster.\",\n      \"choiceB\": \"I decided not to go to the hospital, it isn't right to take from people who may need it more.\"\n    },\n\n    {\n      \"newsTitle\": \"A Great Year for Gardening\",\n      \"newsText\": \"April showers bring May flowers...and some nice produce too. Many local gardeners have boasted about their crops bring more then expected this summer. \\\"Yields have been more than we anticipated,\\\" said Mr. Fisher, one local gardener.\",\n      \"decisionFrom\": \"special\",\n      \"gameText\": \"Should I give them back or keep them?\",\n      \"radioText\": \"Police looking for possible thief\",\n      \"letterText\": \"\",\n      \"specialEventFlag\": \"isCannedFood\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"isCornSteal = TRUE || isNeighborSteal = TRUE\",\n      \"specialEventBody\": \"Dear Diary,\\n\\nWhile waiting in line at the grocery store today I noticed a pack of cigarettes left in my cart, it made think of my son who may need them since he can't buy them himself.\",\n      \"choiceA\": \"I stuffed them in my pocket and sent them to my boy, he'll be so pleased.\",\n      \"choiceB\": \"I gave them back to the cashier, she was thankful of my honesty and gave me a coupon for my next time shopping.\"\n    },\n\n    {\n      \"newsTitle\": \"Battle Rages On\",\n      \"newsText\": \"AS the war continues, remember to think of our soldiers fighting for us today. Any letters of thanks would be greatly appreciated and give our sons the push to carry on the good fight.\",\n      \"decisionFrom\": \"letter\",\n      \"gameText\": \"Dear Diary,\\nMy grouchy sister wrote me a letter recently about the homeless boys in her neighborhood. It made me think of my Decker and if he were homeless, would I help him. My boy needs help on the lines right now though. I may get a ride in town from my neighbors tomorrow... Should I steal from the less needy to help my boy at war?\",\n      \"radioText\": \"\",\n      \"letterText\": \"Greetings sister,\\nYou should consider yourself grateful you don't have to live downtown with the urchins like I do. There are beggar children on the street, some the same age as your Decker. They have nothing to their name but the clothes on their back and boots on their feet. But if you ask me, they must have stolen those boots, they're too new to be their own.\\nRegards,\\nBuela\",\n      \"specialEventFlag\": \"isHelpHobo\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"Raining loudly outside \",\n      \"choiceA\": \"I stole the boots off a homeless boy to help my son, it wasn't easy but the boy had passed already so he didn't need them but my son does.\",\n      \"choiceB\": \"I couldn't go into town today, I didn't want to see my son in those poor boys' faces.\"\n    },\n\n    {\n      \"newsTitle\": \"New Store Opening\",\n      \"newsText\": \"Johnson's Family Store opened this weekend with a large crowd awaiting the Grand Opening. One customer had this to say, \\\"It feels like I'm shopping at my neighbors, everyone is so friendly and the whole Johnson family actually works there. I love it!\\\"\",\n      \"decisionFrom\": \"radio\",\n      \"gameText\": \"Dear Diary,\\n\\nI'm trying to decide if I should go to the abbey today and claim an extra blanket for my son or let some other poor soul have them. What should I choose to do?\",\n      \"radioText\": \"Hello neighbor, it's starting to get cold out and there are people in need of warm blankets. So send your extras to the Sisterhood of Saint John or drop them off today!\",\n      \"letterText\": \"\",\n      \"specialEventFlag\": \"ishospitalSteal\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        0,\n        1\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"\",\n      \"choiceA\": \"I went to the abbey today and took a blanket that might have helped warm someone in need, but my son need it too.\",\n      \"choiceB\": \"I didn't get my son a blanket today, I hope he can weather the cold.\"\n    },\n\n    {\n      \"newsTitle\": \"New Jobs Available as Men Leave for War\",\n      \"newsText\": \"With our men away on the front lines, women are finding their place in the workforce and raising a helping hand to aid in the war effort. Whether it's helping make clothes for our boys or ammunition for their war toys, our women are ready to help!\",\n      \"decisionFrom\": \"special\",\n      \"gameText\": \"I wasn't sure whether to tell him or to take it for my son.\",\n      \"radioText\": \"\",\n      \"letterText\": \"\",\n      \"specialEventFlag\": \"isStealBook\",\n      \"yes\": [\n        1,\n        1\n      ],\n      \"no\": [\n        -1,\n        0\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"Dear Diary,\\n\\nI saw a man drop a book leaving the library today, he didn't seem to notice.\",\n      \"choiceA\": \"I didn't tell him, it was just the kind of book my son loves. I'm sure he will be happy!\",\n      \"choiceB\": \"I told the man he dropped his book, he was so thankful for my kindness.\"\n    },\n    \n    {\n      \"newsTitle\": \"The Cost of War\",\n      \"newsText\": \"As the number of casualties rise, the worry of victory on the battlefield seems uncertain. Will we be able to win this war? Is the price of our sons' lives worth it if we lose in the end?\",\n      \"decisionFrom\": \"radio\",\n      \"gameText\": \"Dear Diary,\\n\\nI'm thinking of going to the peninsula to help my son, even though I don't have much experience I know I can help him.\",\n      \"radioText\": \"Help our sons at war! If you have any experience in nursing or caretaking, you may be able to help our soldiers on the front lines. Please come to the Redemption Rangers Headquarters for an interview.\",\n      \"letterText\": \"\",\n      \"specialEventFlag\": \"IsJoinWar\",\n      \"yes\": [\n        0,\n        1\n      ],\n      \"no\": [\n        0,\n        1\n      ],\n      \"conditionSpecialEvent\": \"\",\n      \"conditionRadio\": \"\",\n      \"specialEventBody\": \"\",\n      \"choiceA\": \"I'm going to do it, I need to help my son.\",\n      \"choiceB\": \"I'm going to do it, I need to help my son.\"\n    }\n  ]\n}";
        //path = Application.streamingAssetsPath + "/gameData.json";
        //jsonString = File.ReadAllText (path); 
        // Debug.Log("Game data path :" + path);
        DataList gameDatas = JsonUtility.FromJson<DataList> (webGL);
        // print(gameDatas.GameData.Length);
        GameData current = gameDatas.GameData[day];
        this.letterText = current.letterText;
        this.yes = current.yes;
        this.no = current.no;
        this.decisionFrom = current.decisionFrom;
        this.gameText = current.gameText;
        this.newsTitle = current.newsTitle;
        this.newsText = current.newsText;
        this.choiceA = current.choiceA;
        this.choiceB = current.choiceB;
        
        if (current.conditionSpecialEvent != "") {
            if (hasFlag(current.conditionSpecialEvent)) {
                if (current.specialEventFlag != "") {
                    this.specialEventFlag = current.specialEventFlag;
                    this.specialEventBody = current.specialEventBody;
                }
                else {
                    this.specialEventFlag = "";
                    this.specialEventBody = "";
                }
            }
        }
        else{
            this.specialEventFlag = current.specialEventFlag;
            this.specialEventBody = current.specialEventBody;
        }

        if (current.conditionRadio != "") {
            if (hasFlag(current.conditionRadio)) {
                this.radioText = current.radioText;
            }
            else {
                this.radioText = "";
            }
        }
        else {
            this.radioText = current.radioText;
        }

        this.debug();
    }
    public void makeDecision(bool decision){
        ///Input: current date
        ///Edit evilPoint & sonPoint base on decision, add gameFlag if applicable
        this.evilPoint = this.evilPoint + (decision ? this.yes[0] : this.no[0]);
        this.sonPoint = this.sonPoint + (decision ? this.yes[1] : this.no[1]);
        if (decision) {
            if(this.specialEventFlag!=null){
                this.gameFlags.Add(this.specialEventFlag);
            }
        }
    }

    public bool hasFlag(string gameflag) {
        ///look for a gameflag
        foreach (string flag in this.gameFlags) {
            if (gameflag == flag)
                return true;
        }
        return false;
    }

    //constructor
    public GameFlagManager(int day, int evilPoint, int sonPoint, int[] yes, int[] no, string radioText, string letterText, string gameText, List<string> gameFlags, string specialEventFlag,string specialEventBody, bool decision, string decisionFrom, string newsText) {
        this.day = day;
        this.evilPoint = evilPoint;
        this.sonPoint = sonPoint;
        this.yes = yes;
        this.no = no;
        this.radioText = radioText;
        this.letterText = letterText;
        this.gameText = gameText;
        this.gameFlags = gameFlags;
        this.specialEventFlag = specialEventFlag;
        this.specialEventBody = specialEventBody;
        this.decision = decision;
        this.decisionFrom = decisionFrom;
        this.newsText = newsText;
    }

    public void debug() {
        ///Print level info in a json format
        string log = JsonUtility.ToJson(this);
        // Debug.Log(log);
    }
    public void dayPass() {
        ///One day passed, update level info
        this.day++;
        readGameData(day);
    }

    //init
    public int day = 0;//Player needs to make decision to pass a day
    public int evilPoint = 0;
    public int sonPoint = 0;
    public int[] yes = null;
    public int[] no = null;
    public string radioText = "";//First Priority, then play random songs
    public string letterText = "";
    public string gameText = "";//Show on notebook, player make decision
    public List<string> gameFlags = null;//A list of game flags
    public string specialEventFlag = "";//Flag that will add to gameFlags
    public string specialEventBody = "";//Event text
    public bool decision = false;
    public string decisionFrom = "special";
    public string newsText = "";
    public string newsTitle;//everyday newspaper, do not contain decision
    public string choiceA = "";
    public string choiceB = "";
}
