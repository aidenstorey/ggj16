using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu : game_state {

    button[] buttons;
    int selected = 0;

    // Use this for initialization
    void Start()
    {
        buttons = gameObject.GetComponentsInChildren<button>();
        updateSelected();
    }

    // Update is called once per frame
    void Update ()
    {
        if (this.accepting_input)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selected = (selected + buttons.Length - 1) % buttons.Length;
                updateSelected();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selected = (selected + 1) % buttons.Length;
                updateSelected();
			}

			if (Input.GetKeyDown(KeyCode.A))
			{
				GameManager.GetGameManager().SetCurrentState(this.buttons[this.selected].stateChange);
			}

			if (Input.GetKeyDown(KeyCode.B)|| Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameManager.GetGameManager().SetCurrentState("game");
            }
        }
	}

	protected override void OnHandleInput(string input_string)
	{
		switch (input_string)
		{
			case "up":
				selected = (selected + buttons.Length - 1) % buttons.Length;
				updateSelected();
				break;
			case "down":
				selected = (selected + 1) % buttons.Length;
				updateSelected();
				break;
			case "a":
				GameManager.GetGameManager().SetCurrentState(this.buttons[this.selected].stateChange);
				break;
			case "b":
			case "start":
				GameManager.GetGameManager().SetCurrentState("game");
				break;
		}
	}

	void updateSelected ()
    {
        for (int i = 0; i < this.buttons.Length; i++)
        {
            this.buttons[i].setSelected(i == this.selected);
        }
    }

    protected override void OnEnterState()
    {
		if (buttons == null)
		{
			buttons = gameObject.GetComponentsInChildren<button>();
		}

        this.gameObject.SetActive(true);
        this.selected = 0;
        this.updateSelected();
    }

    protected override void OnLeaveState()
    {
		this.gameObject.SetActive(false);
    }
}
