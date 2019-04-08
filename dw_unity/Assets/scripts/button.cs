using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class button : MonoBehaviour {

    private bool isSelected;

    [SerializeField]
    public string stateChange;

	// Use this for initialization
	void Start ()
    {
        isSelected = false;
	}

    public void setSelected(bool selection = true)
    {
        isSelected = selection;
        Image image = gameObject.GetComponent<Image>(); 
        Color poop = image.color;
        poop.a = isSelected ? 1.0f : 0.7f;
        image.color = poop;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
