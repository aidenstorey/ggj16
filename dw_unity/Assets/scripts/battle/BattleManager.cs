using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public GameObject CurrentEnemy;
	public GameObject BossManagerObj;

	public BossBattle BossManager;

	private static BattleManager Instance;

	void Start()
	{
		BossManager = BossManagerObj.GetComponent<BossBattle>();

		if (Instance == null)
		{
			Instance = this;
		}
	}

	public static BattleManager GetInstance()
	{
		return (Instance);
	}
    
    public void PlayRound( RockPaperScissors.RPSChoice _PlayerChoice )
    {
        bool EndBattle = false;
        
        switch( RockPaperScissors.PlayRPS( _PlayerChoice, CurrentEnemy.GetComponent<DefaultEnemy>().GenerateRPSMove() ) )
        {
        case 1: // Player wins
            EndBattle = CurrentEnemy.GetComponent<BattleShared>().TakeDamage( GameObject.FindGameObjectWithTag( "Player" ).GetComponent<BattleShared>().Damage );
            if( EndBattle ) { GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Animator>().SetTrigger( "victory" ); }
            break;

        case 2: // Enemy wins
            CurrentEnemy.GetComponent<Animator>().SetTrigger( "attack" );
                GetShadowChild().SetTrigger("attack");

                EndBattle = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<BattleShared>().TakeDamage( CurrentEnemy.GetComponent<BattleShared>().Damage );
            if( EndBattle ){ GameManager.GetGameManager().LoseRandomIngredient(); }
            break;

        default: // Tie
				 // TODO: Anything happen on a tie?
				GameManager.GetGameManager().current_state.Reset();
            break;
        }

        CurrentEnemy.GetComponent<DefaultEnemy>().DisplayMove();

        if( EndBattle )
        {
            DestroyObject( CurrentEnemy );
            GameObject.FindGameObjectWithTag( "Player" ).GetComponent<BattleShared>().CurrentHealth = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<BattleShared>().MaxHealth;

            StopBattle();
        }
    }

    public void StartBattle( GameObject EnemyObject )
    {
        CurrentEnemy = EnemyObject;

        CurrentEnemy.transform.GetChild( 0 ).gameObject.SetActive( true );
        CurrentEnemy.GetComponent<Animator>().SetTrigger( "attack" );
        GetShadowChild().SetTrigger("attack");
        GameManager.GetGameManager().SetCurrentState("battle");
    }

    public void StopBattle()
    {
		GameManager.GetGameManager().SetCurrentState("game");
    }

    private Animator GetShadowChild()
    {
        Animator shadow = transform.FindChild("Shadow").GetComponent<Animator>();
        return shadow;
    }
}
