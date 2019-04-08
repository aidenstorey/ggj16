using UnityEngine;
using System.Collections;

public class game_state : MonoBehaviour {

    protected bool accepting_input;

	// Use this for initialization
	void Start ()
    {
        this.accepting_input = false;
	}

    public void EnterState()
    {
        this.accepting_input = true;
        this.OnEnterState();
    }

    public void LeaveState()
    {
        this.accepting_input = false;
        this.OnLeaveState();
    }

	public void Reset()
	{
		this.OnReset();
	}

	public void HandleInput(string input_string)
	{
		this.OnHandleInput(input_string);
	}

    protected virtual void OnLeaveState()
    {

    }

	protected virtual void OnEnterState()
	{

	}

	protected virtual void OnReset()
	{

	}

	protected virtual void OnHandleInput(string input_string)
	{
	}
}
