using UnityEngine;
using Fungus;
using System.Collections;

public enum EKeyPress
{
	KEYPRESS_INVALID = 0,
	KEYPRESS_UP = 1,
	KEYPRESS_DOWN = 2,
	KEYPRESS_RIGHT = 3,
	KEYPRESS_LEFT = 4,
	KEYPRESS_A = 5,
	KEYPRESS_B = 6,
	KEYPRESS_MAX,
	KEYPRESS_SELECT,
	KEYPRESS_START
}

public class BossBattle : MonoBehaviour {

    public int[] RitualLength = { 3, 3, 4, 5, 5, 6, 7, 7, 8, 9 };
    public int HealthIncrease = 8;
    public int DamageIncrease = 3;

    public RitualItem[,] RitualItems = { { RitualItem.RITUAL_ITEM_CAULDRON, RitualItem.RITUAL_ITEM_BEANS, RitualItem.RITUAL_ITEM_CANDLE },      // Prestidigitation
                                         { RitualItem.RITUAL_ITEM_CANDLE, RitualItem.RITUAL_ITEM_JUNIPER, RitualItem.RITUAL_ITEM_BEANS },       // Pyrefighting
                                         { RitualItem.RITUAL_ITEM_CANDLE, RitualItem.RITUAL_ITEM_CHICKEN, RitualItem.RITUAL_ITEM_POTATO },      // Energy Transfer
                                         { RitualItem.RITUAL_ITEM_JUNIPER, RitualItem.RITUAL_ITEM_CAULDRON, RitualItem.RITUAL_ITEM_FIG },       // Now You See Tea, Now You Don't
                                         { RitualItem.RITUAL_ITEM_CHICKEN, RitualItem.RITUAL_ITEM_CANDLE, RitualItem.RITUAL_ITEM_BEANS },       // Feather Flight
                                         { RitualItem.RITUAL_ITEM_FIG, RitualItem.RITUAL_ITEM_FIG, RitualItem.RITUAL_ITEM_FIG },                // TransFIGuration
                                         { RitualItem.RITUAL_ITEM_CHICKEN, RitualItem.RITUAL_ITEM_POTATO, RitualItem.RITUAL_ITEM_CAULDRON },    // Souper Healing
                                         { RitualItem.RITUAL_ITEM_POTATO, RitualItem.RITUAL_ITEM_JUNIPER, RitualItem.RITUAL_ITEM_CAULDRON },    // Summoning Spirits
                                         { RitualItem.RITUAL_ITEM_CAULDRON, RitualItem.RITUAL_ITEM_JUNIPER, RitualItem.RITUAL_ITEM_TONGUE },    // Verum Serum
                                         { RitualItem.RITUAL_ITEM_TONGUE, RitualItem.RITUAL_ITEM_TENDERCLE, RitualItem.RITUAL_ITEM_JUNIPER },   // Praise the Dead
                                         { RitualItem.RITUAL_ITEM_TENDERCLE, RitualItem.RITUAL_ITEM_POTATO, RitualItem.RITUAL_ITEM_TONGUE },    // Lovecrafting
                                         { RitualItem.RITUAL_ITEM_COMB, RitualItem.RITUAL_ITEM_TONGUE, RitualItem.RITUAL_ITEM_HAT }             // Resting Witch Face
                                        };

    public int RitualNumber = 0;
    public int CurrentStep = 0;
    public ArrayList RitualSteps = new ArrayList();

    float WitchFaceTimer = 20.0f;

    public enum RitualItem
    {
        RITUAL_ITEM_INVALID     = 0,
        RITUAL_ITEM_BEANS,
        RITUAL_ITEM_CANDLE,
        RITUAL_ITEM_CAULDRON,
        RITUAL_ITEM_CHICKEN,
        RITUAL_ITEM_FIG,
        RITUAL_ITEM_JUNIPER,
        RITUAL_ITEM_POTATO,
        RITUAL_ITEM_TONGUE,
        RITUAL_ITEM_TENDERCLE,
        RITUAL_ITEM_COMB,
        RITUAL_ITEM_HAT,
        RITUAL_ITEM_MAX
    }

    void Update()
    {
        if( RitualNumber == 11 ) // Resting witch face
        {
            WitchFaceTimer -= Time.deltaTime;

            if( WitchFaceTimer < 0.0f )
            {
                WinRitual();
            }
        }
    }

    public void StartRitual()
    {
        if( RitualNumber > 10 )
        {
            for( int i = 0; i < RitualLength[RitualNumber]; ++i )
            {
                RitualSteps.Add( (EKeyPress)Random.Range( ( ( int )EKeyPress.KEYPRESS_INVALID + 1 ), ( int )EKeyPress.KEYPRESS_MAX ) );
            }
        }
        else if( RitualNumber == 10 )
        {
            // Konami Code
            RitualSteps.Add( EKeyPress.KEYPRESS_UP );
            RitualSteps.Add( EKeyPress.KEYPRESS_UP );
            RitualSteps.Add( EKeyPress.KEYPRESS_DOWN );
            RitualSteps.Add( EKeyPress.KEYPRESS_DOWN );
            RitualSteps.Add( EKeyPress.KEYPRESS_LEFT );
            RitualSteps.Add( EKeyPress.KEYPRESS_RIGHT );
            RitualSteps.Add( EKeyPress.KEYPRESS_LEFT );
            RitualSteps.Add( EKeyPress.KEYPRESS_RIGHT );
            RitualSteps.Add( EKeyPress.KEYPRESS_B );
            RitualSteps.Add( EKeyPress.KEYPRESS_A );
            RitualSteps.Add( EKeyPress.KEYPRESS_START );
        }
        else if( RitualNumber == 11 )
        {
            // Resting witch face
            RitualSteps.Add( EKeyPress.KEYPRESS_INVALID );
        }
    }

    public int AttemptRitualStep( EKeyPress _PlayerInput )
    {
        if( _PlayerInput == ( EKeyPress )RitualSteps[CurrentStep] )
        {
            CurrentStep++;

            if( CurrentStep == RitualSteps.Capacity )
            {
                WinRitual();
                GameObject.FindGameObjectWithTag( "Boss" ).GetComponent<Animator>().SetTrigger( "die" );
                return 1;
            }

            GameObject.FindGameObjectWithTag( "Boss" ).GetComponent<Animator>().SetTrigger( "hurt" );
            return 0;
        }
        else
        {
            if( RitualNumber == 11 ) // Resting witch face
            {
                WitchFaceTimer = 20.0f;
            }

            GameObject.FindGameObjectWithTag( "Boss" ).GetComponent<Animator>().SetTrigger( "attack" );


            CurrentStep = 0;
            return -1;
        }
    }

    public void WinRitual()
    {
        RitualNumber++;
        SwitchToDialogue(RitualNumber);
        // TODO: Other win stuff - Get badge
        BattleShared PlayerStats = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<BattleShared>();
        GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Animator>().SetTrigger( "victory" );

        PlayerStats.MaxHealth += HealthIncrease;
        PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
        PlayerStats.Damage += DamageIncrease;
        
        EndRitual();
    }

    void SwitchToDialogue(int dia)
    {
        string message = "";
        switch (dia)
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

    public void EndRitual()
    {
		GameManager.GetGameManager().SetCurrentState("town");
        RitualSteps.Clear();
        CurrentStep = 0;
    }

	public string enum_to_string(EKeyPress enummmmm)
	{
		string out_string = "";

		switch (enummmmm)
		{
			case EKeyPress.KEYPRESS_UP:
				out_string = "up";
				break;
			case EKeyPress.KEYPRESS_DOWN:
				out_string = "down";
                break;
			case EKeyPress.KEYPRESS_RIGHT:
				out_string = "right";
				break;
			case EKeyPress.KEYPRESS_LEFT:
				out_string = "left";
				break;
			case EKeyPress.KEYPRESS_A:
				out_string = "a";
				break;
			case EKeyPress.KEYPRESS_B:
				out_string = "b";
				break;
			case EKeyPress.KEYPRESS_SELECT:
				out_string = "select";
				break;
			case EKeyPress.KEYPRESS_START:
				out_string = "start";
				break;
			default:
				break;
		}
		return (out_string);
	}

	public EKeyPress string_to_enum(string enummmmm)
	{
		EKeyPress out_string = EKeyPress.KEYPRESS_INVALID;

		switch (enummmmm)
		{
			case "up":
				out_string = EKeyPress.KEYPRESS_UP;
				break;
			case "down":
				out_string = EKeyPress.KEYPRESS_DOWN;
				break;
			case "right":
				out_string = EKeyPress.KEYPRESS_RIGHT;
				break;
			case "left":
				out_string = EKeyPress.KEYPRESS_LEFT;
				break;
			case "a":
				out_string = EKeyPress.KEYPRESS_A;
                break;
			case "b":
				out_string = EKeyPress.KEYPRESS_B;
				break;
			case "select":
				out_string = EKeyPress.KEYPRESS_SELECT;
				break;
			case "start":
				out_string = EKeyPress.KEYPRESS_START;
				break;
			default:
				break;
		}

		return (out_string);
	}
}
