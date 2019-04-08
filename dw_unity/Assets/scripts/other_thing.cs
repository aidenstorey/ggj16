using UnityEngine;
using System.Collections;

public class other_thing : MonoBehaviour
{
	public void Hidden(bool hide = true)
	{
		this.gameObject.SetActive(hide);
	}
}
