using UnityEngine;
using System.Collections;

public class MovementShared : MonoBehaviour {

    public static float TileSize = 0.64f;
    public static float MoveSpeed = 0.05f;

	public Vector3 Target;
    bool IsMoving;
    float PrevDist = 100000.0f;
    public bool bBlocked = false;
    Vector3 PrevPos;

    Animator animCharacter;
    Animator animShadow;

    public enum EDirection
    {
        DIRECTION_INVALID = 0,
        DIRECTION_UP      = 1,
        DIRECTION_DOWN    = 2,
        DIRECTION_RIGHT   = 3,
        DIRECTION_LEFT    = 4,
        DIRECTION_MAX
    }

    void Start()
    {
        Target = this.transform.position;
        animCharacter = this.GetComponent<Animator>();
    }
   
    public void HandleInput( EDirection _InputDirection )
    {
        if( !IsMoving )
        {
            IsMoving = true;

            Animator childAnim = GetShadowChild();
            
            switch( _InputDirection )
            {
            case EDirection.DIRECTION_UP:
                Target += Vector3.up * TileSize;
                animCharacter.SetTrigger( "walk_up" );
                childAnim.SetTrigger( "walk_up" );
                break;

            case EDirection.DIRECTION_DOWN:
                Target += Vector3.up * -TileSize;
                animCharacter.SetTrigger( "walk_down" );
                childAnim.SetTrigger( "walk_down" );
                break;

            case EDirection.DIRECTION_RIGHT:
                Target += Vector3.right * TileSize;
                animCharacter.SetTrigger( "walk_right" );
                childAnim.SetTrigger("walk_right");
                break;

            case EDirection.DIRECTION_LEFT:
                Target += Vector3.right * -TileSize;
                animCharacter.SetTrigger( "walk_left" );
                childAnim.SetTrigger("walk_left");
                break;

            default:
                break;
            }

            if( !level_generation.GetInstance().IsTraversible( Target.x, Target.y ) )
            {
                bBlocked = true;
                PrevPos = this.transform.position;

                switch( _InputDirection )
                {
                case EDirection.DIRECTION_UP:
                    Target -= Vector3.up * TileSize * 0.5f;
                    animCharacter.SetTrigger( "walk_up" );
                        childAnim.SetTrigger("walk_up");
                        break;

                case EDirection.DIRECTION_DOWN:
                    Target -= Vector3.up * -TileSize * 0.5f;
                    animCharacter.SetTrigger( "walk_down" );
                        childAnim.SetTrigger("walk_down");
                        break;

                case EDirection.DIRECTION_RIGHT:
                    Target -= Vector3.right * TileSize * 0.5f;
                    animCharacter.SetTrigger( "walk_right" );
                        childAnim.SetTrigger("walk_right");
                        break;

                case EDirection.DIRECTION_LEFT:
                    Target -= Vector3.right * -TileSize * 0.5f;
                    animCharacter.SetTrigger( "walk_left" );
                        childAnim.SetTrigger("walk_left");
                        break;

                default:
                    break;
                }
            }
        }
    }

    void Update()
    {
        if( IsMoving )
        {
            // TODO: Check what you're moving into
            this.transform.position += MoveSpeed * ( Target - this.transform.position ).normalized;

            if( Vector3.Distance( this.transform.position, Target ) < PrevDist )
            {
                PrevDist = Vector3.Distance( this.transform.position, Target );
            }
            else
            {
                PrevDist = 100000.0f;
                this.transform.position = Target;

                if( bBlocked )
                {
                    bBlocked = false;
                    Target = PrevPos;
                }
            }

            if( this.transform.position == Target )
            {
                IsMoving = false;
            }
        }
    }

    public Vector3 GetTargetLocation()
    {
        return Target;
    }

    private Animator GetShadowChild ()
    {
        Animator shadow = transform.FindChild("Shadow").GetComponent<Animator>();
        return shadow;
    }
}
