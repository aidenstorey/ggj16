using UnityEngine;
using System.Collections;

public class badges : game_state {

    // Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.accepting_input)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GameManager.GetGameManager().SetCurrentState("pause");
            }
        }

	}

	protected override void OnHandleInput(string input_string)
	{
		switch (input_string)
		{
			case "b":
				GameManager.GetGameManager().SetCurrentState("pause");
				break;
		}
	}

	protected override void OnEnterState()
    {
        gameObject.SetActive(true);
    }

    protected override void OnLeaveState()
    {
        gameObject.SetActive(false);
    }
}
