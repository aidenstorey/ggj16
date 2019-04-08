using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class twitch_output : MonoBehaviour
{
	private static twitch_output Instance;

	Queue names = new Queue();
	Queue inputs = new Queue();

	string latest_name = null;

	int max = 11;

	Text[] text_boxes;

	// Use this for initialization
	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		this.text_boxes = this.gameObject.GetComponentsInChildren<Text>();
	}

	public void AddName(string name)
	{
		this.latest_name = name;
		names.Enqueue(name);

		if (names.Count >= max)
		{
			names.Dequeue();
		}

		text_boxes[0].text = "";

		foreach (string its_a_name in names)
		{
			text_boxes[0].text += its_a_name + "\n";
		}
	}

	public void AddInput(string input)
	{
		inputs.Enqueue(input);

		if (inputs.Count >= max)
		{
			inputs.Dequeue();
		}

		text_boxes[1].text = "";

		foreach (string its_a_input in inputs)
		{
			text_boxes[1].text += its_a_input + "\n";
		}
	}

	public void UpdateScrewup()
	{
		this.text_boxes[2].text = this.latest_name;
	}

	public static twitch_output GetInstance()
	{
		return Instance;
	}
}
