using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

    public bool traversable;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTraversible(bool _traversable = true)
    {
        this.traversable = _traversable;
    }

    public bool IsTraversible()
    {
        return (this.traversable);
    }
}
