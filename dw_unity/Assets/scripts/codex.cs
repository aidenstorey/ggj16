using UnityEngine;
using System.Collections;

public class codex : game_state
{
	hide_show[] pages;
    button[] buttons;
    int selected = 0;

    int page_max = 5;
    int page = 0;



    // Use this for initialization
    void Start()
    {
		pages = gameObject.GetComponentsInChildren<hide_show>();
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
                selected = (selected + buttons.Length - 1) % buttons.Length;
                updateSelected();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selected = (selected + 1) % buttons.Length;
                updateSelected();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                GameManager.GetGameManager().SetCurrentState("pause");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                switch (buttons[selected].stateChange)
                {
                    case "flip_left":
                        if (this.page > 0)
                        {
                            this.page--;
                        }
                        break;

                    case "flip_right":
                        if (this.page < this.pages.Length - 1)
                        {
                            this.page++;
                        }
                        break;

                    default:
                        GameManager.GetGameManager().SetCurrentState(buttons[selected].stateChange);
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
				selected = (selected + buttons.Length - 1) % buttons.Length;
				updateSelected();
				break;

			case "right":
				selected = (selected + 1) % buttons.Length;
				updateSelected();
				break;

			case "a":
				switch (buttons[selected].stateChange)
				{
					case "flip_left":
						if (this.page > 0)
						{
							this.page--;
							updateSelected();
						}
						break;

					case "flip_right":
						if (this.page < this.page_max)
						{
							this.page++;
							updateSelected();
						}
						break;

					default:
						GameManager.GetGameManager().SetCurrentState(buttons[selected].stateChange);
						break;
				}
				break;

			case "b":
				GameManager.GetGameManager().SetCurrentState("pause");
				break;
		}
	}

	void updateSelected()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].setSelected(i == selected);
        }

		for (int i = 0; i < pages.Length; i++)
		{
			pages[i].Hidden(i != page);
        }
    }

    protected override void OnEnterState()
	{
		if (buttons == null)
		{
			buttons = gameObject.GetComponentsInChildren<button>();
		}

		if (pages == null)
		{
			pages = gameObject.GetComponentsInChildren<hide_show>();
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
