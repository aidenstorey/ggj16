using UnityEngine;
using Fungus;
using System.Collections;

public class TestFungus : MonoBehaviour {
    private int dialogue = 0;

	void Update () {
	    
        if(Input.GetKeyDown(KeyCode.P))
        {
            dialogue++;
            SwitchToDialogue(dialogue);
        }

	}

    void SwitchToDialogue (int dia)
    {
        string message = "";
        switch(dia)
        {
            case 1:
                message = "parlour";
                break;
            case 2:
                message = "incendiarist";
                break;
            case 3:
                message = "physicist";
                break;
            case 4:
                message = "scryer";
                break;
            case 5:
                message = "harpy";
                break;
            case 6:
                message = "fig";
                break;
            case 7:
                message = "soul";
                break;
            case 8:
                message = "brewmaster";
                break;
            case 9:
                message = "troothsayer";
                break;
            case 10:
                message = "high";
                break;
            case 11:
                message = "amourist";
                break;
            case 12:
                message = "queen";
                break;
        }

        Flowchart.BroadcastFungusMessage(message);
    }

}
