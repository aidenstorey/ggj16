using UnityEngine;
using System.Collections;

public class hide_show : MonoBehaviour
{
	public void Hidden(bool hide = true)
	{
		this.gameObject.SetActive(!hide);
	}
}
