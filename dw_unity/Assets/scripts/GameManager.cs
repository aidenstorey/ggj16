using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public float TileSize = 0.64f;
    public float MoveSpeed = 0.05f;

    public int EnemyDetectionRange = 10;

    public ArrayList Enemies = new ArrayList();

    public game_state current_state = null;

    [SerializeField]
    public game_state state_game = null;

    [SerializeField]
    public game_state state_pause = null;

    [SerializeField]
    public game_state state_badges = null;

    [SerializeField]
    public game_state state_codex = null;

	[SerializeField]
	public game_state state_battle = null;

	[SerializeField]
	public game_state state_boss_battle = null;

	[SerializeField]
	public game_state state_town = null;

	public bool debug_commands = false;

	[SerializeField]
	public level_def def_grass = null;

	[SerializeField]
	public level_def def_evil = null;

	[SerializeField]
	public level_def def_mystery = null;

	[SerializeField]
	public level_def def_rock = null;

	[SerializeField]
	public level_def def_desert = null;

	System.Random rand;

    public ArrayList PlayerInventory = new ArrayList();

    void Start ()
    {
        if( Instance == null )
        {
            Instance = this;
		}
		rand = new System.Random(Time.time.GetHashCode());

		MovementShared.TileSize = TileSize;
        MovementShared.MoveSpeed = MoveSpeed;

        EnemyMovement.DetectionRange = EnemyDetectionRange * TileSize;
        this.SetCurrentState("town");
    }

    public void ActivateEnemyMovement()
    {
        foreach( EnemyMovement enemy in Enemies )
        {
            enemy.GenerateMove();
        }
    }

    public static GameManager GetGameManager()
    {
        return Instance;
    }

    game_state GetState(string state_name)
    {
        game_state out_state = null;

        switch (state_name)
		{
			case "game":
				out_state = this.state_game;
				break;
			case "pause":
				out_state = this.state_pause;
				break;
			case "codex":
				out_state = this.state_codex;
				break;
			case "badges":
				out_state = this.state_badges;
				break;
			case "battle":
				out_state = this.state_battle;
				break;
			case "boss_battle":
				out_state = this.state_boss_battle;
				break;
			case "town":
				out_state = this.state_town;
				break;
		}

        return out_state;
    }

	public level_def GetLevelDef(string level_name)
	{
		level_def out_level = null;

		switch (level_name)
		{
			case "grass":
				out_level = this.def_grass;
				break;
			case "evil":
				out_level = this.def_evil;
				break;
			case "mystery":
				out_level = this.def_mystery;
				break;
			case "rock":
				out_level = this.def_rock;
				break;
			case "desert":
				out_level = this.def_desert;
				break;
		}
		
		return out_level;
	}

    public void SetCurrentState(string state_name)
    {
        game_state state = this.GetState(state_name);

        if (this.current_state != state)
        {
            if (this.current_state != null)
            {
                this.current_state.LeaveState();
            }

            this.current_state = state;

            if (this.current_state != null)
            {
                this.current_state.EnterState();
            }
        }
    }

    public int LoseRandomIngredient()
    {
        if( PlayerInventory.Capacity > 0 )
        {
            PlayerInventory.RemoveAt( Random.Range( 0, PlayerInventory.Capacity ) );

            return 0;
        }
        else
        {
            return -1;
        }
    }

    public bool CanPerformRitual( BossBattle.RitualItem IngredientA, BossBattle.RitualItem IngredientB, BossBattle.RitualItem IngredientC )
    {
        bool A = false, B = false, C = false;

        foreach( BossBattle.RitualItem currentIngredient in PlayerInventory )
        {
            if( !A && currentIngredient == IngredientA )
            {
                A = true;
                continue;
            }

            if( !B && currentIngredient == IngredientB )
            {
                B = true;
                continue;
            }

            if( !C && currentIngredient == IngredientC )
            {
                C = true;
                continue;
            }
        }

        if( A && B && C )
        {
            PlayerInventory.Remove( IngredientA );
            PlayerInventory.Remove( IngredientB );
            PlayerInventory.Remove( IngredientC );

            return true;
        }

        return false;
    }
}
