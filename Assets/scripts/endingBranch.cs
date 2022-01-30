using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endingBranch : MonoBehaviour
{
    [Range(0,10)]
    public int sonPointPass;//if greater than sonPointPass you are good mom
    [Range(0,10)]
    public int evilPointPass;//if greater than evilPointPass you are a$$hole
    
    public GameFlagManager gameFlagManager;
    public string sonText;
    public string neighborText;
    struct EndingBranch {
        public string gameFlag;
        public string branchBody;
        public bool isFromSon;
        public string evilText;
        public string goodText;

        public EndingBranch(string gameFlag, string evilText, string goodText, bool isFromSon) {
            this.gameFlag = gameFlag;
            this.goodText = goodText;
            this.evilText = evilText;
            branchBody = "";
            this.isFromSon = isFromSon;
        }

        public void setBranchBody(string text)
        {
            this.branchBody = text;
        }
    };

    private List<EndingBranch> endingBranches = new List<EndingBranch>();
    private void Start() {
        // Son
        endingBranches.Add(new EndingBranch(
            "isSteal",
            "Dearest Mother, \n\nThank you so much for the delicious corn! I've missed the taste of fresh vegetables here on the front lines.",
            "Hello Mother, \n\nIt may be too late to mend our fractured relationship. ",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isColdKid",
            "I also wanted to thank you for the canned goods, they'll definitely help me keep fighting.",
            "I wasn't able to open the canned food you sent.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpHobo",
            "This medicine will really come in handy as I am not feeling well and unable to be on the front lines right now.",
            "And it appears you have stolen some medical supplies from I don't know where.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isHospital",
            "I fear I may not be able to last much longer given my injuries, the boots are very much appreciated.",
            "I also recieved the boots you sent me. I fear I haven't much longer left on this earth and knowing my mother is a thief weighs on my conscience.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isSon",
            "The blanket is very warm as well, it gives me comfort on these cold nights.",
            "I only hope this blanket wasn't meant for some child.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isMom",
            "I have been enjoying the book you sent. One of the very few pleasures I have left is reading, thanks to you.  ",
            "And this library book you sent, although I appreciate it, is definitely not meant for me.",
            true
        ));
        endingBranches.Add(new EndingBranch(
            "isEeyore",
            "I was able to trade the cigarettes you gave me, for some nice socks. I've been getting colder lately, I fear I may be at my end. Thank you so much Mother. I love you, Decker",
            "Cigarettes are hardly something a dying man needs, I traded them for some woolen socks. It's been a lot colder lately and I may not survive the winter and this war. Regards, Your Son",
            true
        ));
        // endingBranches.Add(new EndingBranch(
        //     "isJoinWar",
        //     "",
        //     "",
        //     true
        // ));

        // Neighbor
        endingBranches.Add(new EndingBranch(
            "isSteal",
            "Dear Diary, I saw my neighbor, Delores Wells, stealing from the Fishers' garden today.",
            "Dear Diary, I saw my neighbor, Delores Wells, today. She seems worried about her son going off to war.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isColdKid",
            "Dear Diary, I noticed Delores bringing in some extra groceries from town, seems like more than she can eat by herself.",
            "Dear Diary, I noticed Delores bringing in groceries from town today and asked if she needed help. She seemed a little sad and said, \"No, but thank you anyway.\"",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isHelpHobo",
            "Dear Diary, Delores has been going into town a lot more these days, I saw she dropped some medicine from her bag as she was entering her home.",
            "Dear Diary, Delores seems to be in a little rut these days.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isHospital",
            "Dear Diary, she came back with men's boots today and I know her son is away at war so who are those boots for?",
            "Dear Diary, she hasn't left the house much lately.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isSon",
            "Dear Diary, Delores also came back with a large blanket, doesn't she have enough?",
            "Dear Diary, Delores is having a rough time with her son being away it seems. I feel bad for her, I wish there was something I could do to help.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isMom",
            "Dear Diary, the neighbor lady brought home a book. I know she only reads the paper so I'm not sure who's going to read the book.",
            "Dear Diary, the neighbor lady went for a walk today, I waved at her but it seemed like she looked right past me.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isEeyore",
            "Dear Diary, I know for certain that she doesn't smoke, I've seen her son smoking outside. But she brought home a fresh pack of cigarettes with her today.",
            "Dear Diary, I caught Delores Wells outside of her house and brought her the paper and pointed out that hopefully many people will help our soldiers with their efforts on the front lines.",
            false
        ));
        endingBranches.Add(new EndingBranch(
            "isJoinWar",
            "Dear Diary, Delores Wells sent out a package yesterday and it seems as though she is leaving for somewhere today. I saw her leave with some luggage, she looked very tired and uneasy.",
            "Dear Diary, Delores Wells sent out a package yesterday and it seems as though she is leaving for somewhere today. I saw her leave with some luggage, she looked very tired and uneasy.",
            false
        ));
        gameFlagManager = this.GetComponent<GameFlagManager>();
        foreach (EndingBranch endingBranch in endingBranches) {
            if (endingBranch.isFromSon) {            
                endingBranch.setBranchBody(gameFlagManager.sonPoint > sonPointPass ? endingBranch.goodText : endingBranch.evilText);
            }
            else {
                endingBranch.setBranchBody(gameFlagManager.evilPoint > evilPointPass ? endingBranch.evilText : endingBranch.goodText);
            }
        }
    }

    public string getSonText() {
        string _sonText = "";
        foreach (var branch in endingBranches) {
            if (branch.isFromSon == true) {
                if (gameFlagManager.hasFlag(branch.gameFlag)) {
                    _sonText += branch.branchBody;
                    _sonText += "\n";
                }
            }
        }
        
        sonText = _sonText;
        return _sonText;
    }

    public string getNeighborText()
    {        
        string _neighborText = "";
        foreach (var branch in endingBranches) {
            if (branch.isFromSon == false) {
                if (gameFlagManager.hasFlag(branch.gameFlag)) {
                    _neighborText += branch.branchBody;
                    _neighborText += "\n";
                }
            }
        }
        
        neighborText = _neighborText;
        return _neighborText;
    }
}
