using UnityEngine;
using System.Collections;

public class DefaultEnemy : MonoBehaviour
{
    RockPaperScissors.RPSChoice EnemyChoice;

    public Sprite QuestionMark;
    public Sprite Rock;
    public Sprite Paper;
    public Sprite Scissors;

    public RockPaperScissors.RPSChoice GenerateRPSMove()
    {
        EnemyChoice = ( RockPaperScissors.RPSChoice )Random.Range( ( int )RockPaperScissors.RPSChoice.RPS_ID_ROCK, ( int )RockPaperScissors.RPSChoice.RPS_ID_MAX );
        return EnemyChoice;
    }

    public void DisplayMove()
    {
        this.transform.GetChild( 0 ).gameObject.GetComponent<Animator>().enabled = false;
        this.transform.GetChild( 0 ).localScale = new Vector3( 1.0f, 1.0f, 1.0f );

        switch( EnemyChoice )
        {
        case RockPaperScissors.RPSChoice.RPS_ID_ROCK: // TODO: Display Rock
            this.transform.GetChild( 0 ).gameObject.GetComponent<SpriteRenderer>().sprite = Rock;
            break;

        case RockPaperScissors.RPSChoice.RPS_ID_PAPER: // TODO: Display Paper
            this.transform.GetChild( 0 ).gameObject.GetComponent<SpriteRenderer>().sprite = Paper;
            break;

        case RockPaperScissors.RPSChoice.RPS_ID_SCISSORS: // TODO: Display Scissors  
            this.transform.GetChild( 0 ).gameObject.GetComponent<SpriteRenderer>().sprite = Scissors;
            break;

        default: // TODO: Display question mark?
            break;
        }
    }

    public void HideMove()
    {
        this.transform.GetChild( 0 ).GetComponent<Animator>().enabled = true;
        this.transform.GetChild( 0 ).gameObject.GetComponent<SpriteRenderer>().sprite = QuestionMark;
    }
}