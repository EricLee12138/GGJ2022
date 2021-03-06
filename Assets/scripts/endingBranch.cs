using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingBranch : MonoBehaviour
{
    [Range(0,10)]
    public int sonPointPass = 5;//if greater than sonPointPass you are good mom
    [Range(0,10)]
    public int evilPointPass = 5;//if greater than evilPointPass you are a$$hole
    
    public GameFlagManager gameFlagManager;
    public string sonText = "";
    public string neighborText = "";
    public List<EndingBranch> endingBranches = new List<EndingBranch>();

    public struct EndingBranch {
        public string gameFlag;
        public string body;
        public bool isFromSon;
        public string evilText;
        public string goodText;

        public EndingBranch(string gameFlag, string evilText, string goodText, bool isFromSon) {
            this.gameFlag = gameFlag;
            this.goodText = goodText;
            this.evilText = evilText;
            body = "";
            this.isFromSon = isFromSon;
        }

        public void setBranchBody(string text)
        {
            this.body = text;
        }

        public void Debug()
        {
            print("body"+this.body);
            print("good text"+this.goodText);
            print("evil text"+this.evilText);

        }
    };
    private void Start() {
        gameFlagManager = this.GetComponent<GameFlagManager>();
    }
    private void endSum()
    {
        // Son
        endingBranches.Add(new EndingBranch(
            "isCornSteal",
            "Dearest Mother, \n\nThank you so much for the delicious corn! I've missed the taste of fresh vegetables here on the front lines.",
            "Hello Mother, \n\nIt may be too late to mend our fractured relationship. ",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isNeighborSteal",
            "I also wanted to thank you for the canned goods, they'll definitely help me keep fighting.",
            "I wasn't able to open the canned food you sent.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpNeighbor",
            "This medicine will really come in handy as I am not feeling well and unable to be on the front lines right now.",
            "And it appears you have stolen some medical supplies from I don't know where.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isCannedFood",
            "I fear I may not be able to last much longer given my injuries, the boots are very much appreciated.",
            "I also received the boots you sent me. I fear I haven't much longer left on this earth and knowing my mother is a thief weighs on my conscience.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpHobo",
            "The blanket is very warm as well, it gives me comfort on these cold nights.",
            "I only hope this blanket wasn't meant for some child.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "ishospitalSteal",
            "I have been enjoying the book you sent. One of the very few pleasures I have left is reading, thanks to you.  ",
            "And this library book you sent, although I appreciate it, is definitely not meant for me.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isStealBook",
            "I was able to trade the cigarettes you gave me, for some nice socks. I've been getting colder lately, I fear I may be at my end. Thank you so much Mother. I love you, Decker",
            "Cigarettes are hardly something a dying man needs, I traded them for some woolen socks. It's been a lot colder lately and I may not survive the winter and this war. Regards, Your Son",
            true
        ));
        // endingBranches.Add(new EndingBranch(
        //     "IsJoinWar",
        //     "",
        //     "",
        //     true
        // ));

        // Neighbor
        endingBranches.Add(new EndingBranch(
            "isCornSteal",
            "Dear Diary, I saw my neighbor, Delores Wells, stealing from the Fishers' garden today.",
            "Dear Diary, I saw my neighbor, Delores Wells, today. She seems worried about her son going off to war.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isNeighborSteal",
            "Dear Diary, I noticed Delores bringing in some extra groceries from town, seems like more than she can eat by herself.",
            "Dear Diary, I noticed Delores bringing in groceries from town today and asked if she needed help. She seemed a little sad and said, \"No, but thank you anyway.\"",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpNeighbor",
            "Dear Diary, Delores has been going into town a lot more these days, I saw she dropped some medicine from her bag as she was entering her home.",
            "Dear Diary, Delores seems to be in a little rut these days.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isCannedFood",
            "Dear Diary, she came back with men's boots today and I know her son is away at war so who are those boots for?",
            "Dear Diary, she hasn't left the house much lately.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpHobo",
            "Dear Diary, Delores also came back with a large blanket, doesn't she have enough?",
            "Dear Diary, Delores is having a rough time with her son being away it seems. I feel bad for her, I wish there was something I could do to help.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "ishospitalSteal",
            "Dear Diary, the neighbor lady brought home a book. I know she only reads the paper so I'm not sure who's going to read the book.",
            "Dear Diary, the neighbor lady went for a walk today, I waved at her but it seemed like she looked right past me.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isStealBook",
            "Dear Diary, I know for certain that she doesn't smoke, I've seen her son smoking outside. But she brought home a fresh pack of cigarettes with her today.",
            "Dear Diary, I caught Delores Wells outside of her house and brought her the paper and pointed out that hopefully many people will help our soldiers with their efforts on the front lines.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "IsJoinWar",
            "Dear Diary, Delores Wells sent out a package yesterday and it seems as though she is leaving for somewhere today. I saw her leave with some luggage, she looked very tired and uneasy.",
            "Dear Diary, Delores Wells sent out a package yesterday and it seems as though she is leaving for somewhere today. I saw her leave with some luggage, she looked very tired and uneasy.",
            false
        ));
        gameFlagManager = this.GetComponent<GameFlagManager>();
        foreach (var branch in endingBranches) {
            if (branch.isFromSon) {
                branch.setBranchBody(gameFlagManager.sonPoint > sonPointPass ? branch.goodText : branch.evilText);
                if (gameFlagManager.hasFlag(branch.gameFlag)) {
                    sonText += branch.body;
                    sonText += "\n\n";
                }
            }
            else {
                branch.setBranchBody(gameFlagManager.evilPoint > evilPointPass ? branch.evilText : branch.goodText);
                if (!gameFlagManager.hasFlag(branch.gameFlag)) {
                    neighborText += branch.body;
                    neighborText += "\n\n";
                }
            }
            branch.Debug();
        }
    }
    
    public string getSonText() {
        endSum();
        return sonText;
    }

    public string getNeighborText() {        
        return neighborText;
    }
}
