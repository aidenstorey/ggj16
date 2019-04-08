using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public static float DetectionRange = 1000.0f; // Set this elsewhere

    bool IsTracking = false;

    void Start()
    {
        GameManager.GetGameManager().Enemies.Add( this );
    }

    public void GenerateMove()
    {
        MovementShared.EDirection Move = MovementShared.EDirection.DIRECTION_INVALID;
        GameObject Player = GameObject.FindGameObjectWithTag( "Player" );

        if( IsTracking )
        {
            float xdif = 0, ydif = 0;

            xdif = Player.transform.position.x - this.transform.position.x;
            ydif = Player.transform.position.y - this.transform.position.y;

            if( Vector3.Distance( this.transform.position, Player.GetComponent<MovementShared>().GetTargetLocation() ) < 1.1f * MovementShared.TileSize )
            {
                Move = MovementShared.EDirection.DIRECTION_INVALID;

                BattleManager.GetInstance().StartBattle( this.gameObject );
            }
            else
            {
                if( Mathf.Abs( xdif ) > Mathf.Abs( ydif ) )
                {
                    Move = xdif < 0 ? MovementShared.EDirection.DIRECTION_LEFT : MovementShared.EDirection.DIRECTION_RIGHT;
                }
                else
                {
                    Move = ydif < 0 ? MovementShared.EDirection.DIRECTION_DOWN : MovementShared.EDirection.DIRECTION_UP;
                }
            }
        }
        else
        {
            if( Vector3.Distance( this.transform.position, Player.transform.position ) < DetectionRange )
            {
                IsTracking = true;
            }
            
            Move = ( MovementShared.EDirection )Random.Range( 1, ( int )MovementShared.EDirection.DIRECTION_MAX );
        }

        this.gameObject.GetComponent<MovementShared>().HandleInput( Move );

        if( Vector3.Distance( this.gameObject.GetComponent<MovementShared>().GetTargetLocation(), Player.GetComponent<MovementShared>().bBlocked ? Player.GetComponent<MovementShared>().transform.position : Player.GetComponent<MovementShared>().GetTargetLocation() ) < 1.1f * MovementShared.TileSize )
        {
            BattleManager.GetInstance().StartBattle( this.gameObject );
        }
    }

    void Attack()
    {
        if( IsAttackReady() )
        {
            // TODO: Stop player movement
            // TODO: Start battle
        }
    }

    bool IsAttackReady()
    {
        return ( Vector3.Distance( this.transform.position, GameObject.FindGameObjectWithTag( "Player" ).transform.position ) < 1.1f * MovementShared.TileSize );
    }

    void OnDestroy()
    {
        GameManager.GetGameManager().Enemies.Remove( this );
    }
}
