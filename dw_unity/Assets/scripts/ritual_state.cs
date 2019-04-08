using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public struct image_thing
{
	public string name;
	public Sprite hello_is_it_me;
}

public class ritual_state : game_state {

    public GameObject BossObject;

	// U
	hide_show[] hide_show;
	[SerializeField]
	public image_thing[] asdfasdf;
	Image[] imagessss;

	other_thing text;

	void Start()
	{
		hide_show = this.GetComponentsInChildren<hide_show>();
		imagessss = this.GetComponentsInChildren<Image>();
		text = this.GetComponentInChildren<other_thing>();
	}

	protected override void OnEnterState()
	{

		gameObject.SetActive(true);
		// Spawn Boss
		// Spawn Player/Move Player?
		BattleManager.GetInstance().BossManager.StartRitual();

		text.Hidden(true);

		if (BattleManager.GetInstance().BossManager.RitualNumber == 11)
		{
			for (int i = 0; i < hide_show.Length; i++)
			{
				hide_show[i].Hidden(true);
				text.Hidden(false);
			}
		}
		else
		{

			for (int i = 0; i < hide_show.Length; i++)
			{
				string name = BattleManager.GetInstance().BossManager.enum_to_string((EKeyPress)BattleManager.GetInstance().BossManager.RitualSteps[i]);
				Sprite sprite = null;

				foreach (image_thing i_t in asdfasdf)
				{
					if (i_t.name == name)
					{
						sprite = i_t.hello_is_it_me;
						break;
					}
				}

				imagessss[i].sprite = sprite;

				hide_show[i].Hidden(i >= BattleManager.GetInstance().BossManager.RitualNumber);
			}
		}
	}

	// Update is called once per frame
	void Update ()
    {

		BattleManager.GetInstance().BossManager.WinRitual();

		int iRet = -2;

        if( this.accepting_input )
        {
            if( Input.GetKeyDown( KeyCode.UpArrow ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep(EKeyPress.KEYPRESS_UP );
            }

            if( Input.GetKeyDown( KeyCode.DownArrow ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_DOWN );
            }

            if( Input.GetKeyDown( KeyCode.RightArrow ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_RIGHT );
            }

            if( Input.GetKeyDown( KeyCode.LeftArrow ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_LEFT );
            }

            if( Input.GetKeyDown( KeyCode.A ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_A );
            }

            if( Input.GetKeyDown( KeyCode.B ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_B );
            }

            if( Input.GetKeyDown( KeyCode.Alpha1 ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_START );
            }

            if( Input.GetKeyDown( KeyCode.Alpha2 ) )
            {
                BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_SELECT );
            }
        }

        switch( iRet )
        {
        case -1:
            // Boss attack
            break;

        case 0:
            // Boss hurt animation
            break;

        case 1:
            // Boss death
            break;
        }
    }

    protected override void OnHandleInput( string input_string )
    {
        int iRet = -2;

        switch( input_string )
        {
        case "up":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_UP );
            break;

        case "down":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_DOWN );
            break;

        case "right":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_RIGHT );
            break;

        case "left":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_LEFT );
            break;

        case "a":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_A );
            break;

        case "b":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_B );
            break;

        case "select":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_SELECT );
            break;

        case "start":
            iRet = BattleManager.GetInstance().BossManager.AttemptRitualStep( EKeyPress.KEYPRESS_START );
            break;

        default:
            break;
        }

        switch( iRet )
        {
        case -1:
            // Boss attack
            break;

        case 0:
            // Boss hurt animation
            break;

        case 1:
            // Boss death
            break;
        }
    }

    protected override void OnLeaveState()
    {
        gameObject.SetActive( false );
    }
}
