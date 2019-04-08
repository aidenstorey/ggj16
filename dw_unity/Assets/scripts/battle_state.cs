using UnityEngine;
using System.Collections;

public class battle_state : game_state
{
    button[] buttons;
    int selected = 0;

    // Use this for initialization
    void Start()
    {
        buttons = gameObject.GetComponentsInChildren<button>();
        updateSelected();
    }

	// Update is called once per frame
	void Update()
	{
		if (this.accepting_input)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
                BattleManager.GetInstance().CurrentEnemy.GetComponent<DefaultEnemy>().HideMove();

				selected = (selected + buttons.Length - 1) % buttons.Length;
				updateSelected();
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                BattleManager.GetInstance().CurrentEnemy.GetComponent<DefaultEnemy>().HideMove();

                selected = (selected + 1) % buttons.Length;
				updateSelected();
			}

			if (Input.GetKeyDown(KeyCode.A))
			{
				// TODO:	Call battle manager with appropriate varasfsdagfasdfasd
				switch (this.buttons[this.selected].stateChange)
				{
					case "rock":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_ROCK);
						break;
					case "paper":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_PAPER);
						break;
					case "scissors":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_SCISSORS);
						break;
				}
			}
		}
	}

	protected override void OnHandleInput(string input_string)
	{
		switch (input_string)
		{
			case "left":
                BattleManager.GetInstance().CurrentEnemy.GetComponent<DefaultEnemy>().HideMove();
				selected = (selected + buttons.Length - 1) % buttons.Length;
				updateSelected();
				break;

			case "right":
                BattleManager.GetInstance().CurrentEnemy.GetComponent<DefaultEnemy>().HideMove();
				selected = (selected + 1) % buttons.Length;
				updateSelected();
				break;

			case "a":
				switch (this.buttons[this.selected].stateChange)
				{
					case "rock":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_ROCK);
						break;
					case "paper":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_PAPER);
						break;
					case "scissors":
						BattleManager.GetInstance().PlayRound(RockPaperScissors.RPSChoice.RPS_ID_SCISSORS);
						break;
				}
				break;
		}
	}

	void ResetCauseOfTieOrSomethin()
	{
		this.selected = 0;
		this.updateSelected();
	}

    void updateSelected()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].setSelected(i == selected);
        }
    }

	protected override void OnReset()
	{
		this.ResetCauseOfTieOrSomethin();
	}

	protected override void OnEnterState()
	{
		if (buttons == null)
		{
			buttons = gameObject.GetComponentsInChildren<button>();
		}

		gameObject.SetActive(true);
		selected = 0;
		updateSelected();
	}

	protected override void OnLeaveState()
	{
		gameObject.SetActive(false);
	}
}
