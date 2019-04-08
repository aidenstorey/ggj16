using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public BossBattle.RitualItem type;
    
    // Update is called once per frame
    void Update()
    {
        if( GameObject.FindGameObjectWithTag( "Player" ).transform.position.x == this.transform.position.x &&
            GameObject.FindGameObjectWithTag( "Player" ).transform.position.y == this.transform.position.y )
        {
            GameManager.GetGameManager().PlayerInventory.Add( type );

            DestroyObject( this.gameObject );
        }
    }
}
