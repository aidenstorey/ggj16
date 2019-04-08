using UnityEngine;
using System.Collections;

public class town_state : game_state
{
	public GameObject player;

	public level_def[] defs;
	public position_script[] positions;
	int current_portal = 0;

	// Use this for initialization
	void Start()
	{
		this.positions = this.GetComponentsInChildren<position_script>();
		updatePlayer();
	}

	// Update is called once per frame
	void Update()
	{
		if (this.accepting_input)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				current_portal = Mathf.Max(current_portal -1, 0);
				updatePlayer();
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				current_portal = Mathf.Min(current_portal + 1, 5);
				updatePlayer();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				if (this.current_portal < 5)
				{
					level_generation.GetInstance().GenerateBetterMap(defs[current_portal]);
					GameManager.GetGameManager().SetCurrentState("game");
				}
				else if (this.current_portal == 5)
				{
					GameManager.GetGameManager().SetCurrentState("boss_battle");
				}
			}
		}
	}

	protected override void OnHandleInput(string input_string)
	{
		switch (input_string)
		{
			case "left":
				current_portal = Mathf.Max(current_portal - 1, 0);
				updatePlayer();
				break;
			case "right":
				current_portal = Mathf.Min(current_portal + 1, 5);
				updatePlayer();
				break;
			case "a":
				if (this.current_portal < 5)
				{
					level_generation.GetInstance().GenerateBetterMap(defs[current_portal]);
					GameManager.GetGameManager().SetCurrentState("game");
				}
				else if (this.current_portal == 5)
				{
					if (GameManager.GetGameManager().CanPerformRitual( BattleManager.GetInstance().BossManager.RitualItems[ BattleManager.GetInstance().BossManager.RitualNumber, 0 ],
																	   BattleManager.GetInstance().BossManager.RitualItems[ BattleManager.GetInstance().BossManager.RitualNumber, 1 ],
																	   BattleManager.GetInstance().BossManager.RitualItems[ BattleManager.GetInstance().BossManager.RitualNumber, 2 ] ) )
					{
						GameManager.GetGameManager().SetCurrentState("boss_battle");
					}
				}
				break;
		}
	}



	void updatePlayer()
	{
		player.transform.position = this.positions[this.current_portal].transform.position;
	}

	protected override void OnEnterState()
	{
		this.gameObject.SetActive(true);
		hide_show[] hi = this.GetComponentsInChildren<hide_show>();
		for (int i = 0; i < hi.Length; i++)
		{
			hi[i].Hidden(false);
		}
	}

	protected override void OnLeaveState()
	{
		this.gameObject.SetActive(false);
	}
}
