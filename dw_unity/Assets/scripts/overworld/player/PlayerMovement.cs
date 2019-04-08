using UnityEngine;
using System.Collections;

public class PlayerMovement : game_state {

    public string PlayerName = "Booty McButts";

    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if( this.accepting_input )
        {
            if( Input.GetKeyDown( KeyCode.Alpha1 ) )
            {
                GameManager.GetGameManager().SetCurrentState( "pause" );
            }

			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				GameManager.GetGameManager().SetCurrentState("battle");
			}

            // TODO: Switch this to buttons
            if( Input.GetAxis( "Vertical" ) > 0.0f )
            {
                this.gameObject.GetComponent<MovementShared>().HandleInput( MovementShared.EDirection.DIRECTION_UP );
                GameManager.GetGameManager().ActivateEnemyMovement();
            }
            else if( Input.GetAxis( "Vertical" ) < 0.0f )
            {
                this.gameObject.GetComponent<MovementShared>().HandleInput( MovementShared.EDirection.DIRECTION_DOWN );
                GameManager.GetGameManager().ActivateEnemyMovement();
            }
            else if( Input.GetAxis( "Horizontal" ) > 0.0f )
            {
                this.gameObject.GetComponent<MovementShared>().HandleInput( MovementShared.EDirection.DIRECTION_RIGHT );
                GameManager.GetGameManager().ActivateEnemyMovement();
            }
            else if( Input.GetAxis( "Horizontal" ) < 0.0f )
            {
                this.gameObject.GetComponent<MovementShared>().HandleInput( MovementShared.EDirection.DIRECTION_LEFT );
                GameManager.GetGameManager().ActivateEnemyMovement();
            }
        }
	}

	protected override void OnHandleInput(string input_string)
	{
		if (this.accepting_input)
		{
			switch (input_string)
			{
				case "start":
					GameManager.GetGameManager().SetCurrentState("pause");
					break;
                case "up":
                    this.gameObject.GetComponent<MovementShared>().HandleInput(MovementShared.EDirection.DIRECTION_UP);
                    GameManager.GetGameManager().ActivateEnemyMovement();
                    break;
                case "down":
                    this.gameObject.GetComponent<MovementShared>().HandleInput(MovementShared.EDirection.DIRECTION_DOWN);
                    GameManager.GetGameManager().ActivateEnemyMovement();
                    break;
                case "left":
                    this.gameObject.GetComponent<MovementShared>().HandleInput(MovementShared.EDirection.DIRECTION_LEFT);
                    GameManager.GetGameManager().ActivateEnemyMovement();
                    break;
                case "right":
                    this.gameObject.GetComponent<MovementShared>().HandleInput(MovementShared.EDirection.DIRECTION_RIGHT);
                    GameManager.GetGameManager().ActivateEnemyMovement();
                    break;
            }
		}
	}

	protected override void OnEnterState()
	{
        this.transform.position = new Vector3(0.0f, 0.0f);
        this.GetComponent<MovementShared>().Target = new Vector3(0.0f, 0.0f);
    }
}
