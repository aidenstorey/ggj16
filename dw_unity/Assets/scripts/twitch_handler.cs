using UnityEngine;
using System.Collections;
using Irc;

public class twitch_handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TwitchIrc.Instance.OnChannelMessage += OnChannelMessage;
	}

	void OnChannelMessage(ChannelMessageEventArgs channelMessageArgs)
	{
		switch (channelMessageArgs.Message.ToLower())
		{
			case "up":
			case "down":
			case "left":
			case "right":
			case "start":
			case "select":
				twitch_output.GetInstance().AddName(channelMessageArgs.From.ToUpper());
				twitch_output.GetInstance().AddInput(channelMessageArgs.Message.ToUpper());
				GameManager.GetGameManager().current_state.HandleInput(channelMessageArgs.Message.ToLower());
				break;

			case ":a":
				twitch_output.GetInstance().AddName(channelMessageArgs.From.ToUpper());
				twitch_output.GetInstance().AddInput("A");
				GameManager.GetGameManager().current_state.HandleInput("a");
				break;

			case ":b":
				twitch_output.GetInstance().AddName(channelMessageArgs.From.ToUpper());
				twitch_output.GetInstance().AddInput("B");
				GameManager.GetGameManager().current_state.HandleInput("b");
				break;
		}
	}
}
