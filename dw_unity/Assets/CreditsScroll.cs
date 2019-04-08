using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsScroll : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float endPos; //max y

    private RectTransform rt;
    private Vector3 curPos;

    void Start ()
    {
        rt = GetComponent<RectTransform>();
    }

	void Update () {
        curPos = rt.anchoredPosition;

        if(curPos.y <= endPos)
        {
            Vector3 newPos = new Vector3(curPos.x, curPos.y + speed, curPos.z);
            rt.anchoredPosition = newPos;
        }
        else
        {
            //Put code to restart game here
        }

        
	
	}
}
