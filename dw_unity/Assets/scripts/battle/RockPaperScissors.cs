using UnityEngine;
using System.Collections;

public static class RockPaperScissors
{

    public enum RPSChoice
    {
        RPS_ID_INVALID  = 0,
        RPS_ID_ROCK     = 1,
        RPS_ID_PAPER    = 2,
        RPS_ID_SCISSORS = 3,
        RPS_ID_MAX
    }

    public static int PlayRPS( RPSChoice _PlayerOneChoice, RPSChoice _PlayerTwoChoice )
    {
        switch( _PlayerOneChoice - _PlayerTwoChoice )
        {
        case -2: // ROCK vs SCISSORS
        case  1: // PAPER vs ROCK || SCISSORS vs PAPER
            return 1;

        case -1: // ROCK vs PAPER || PAPER vs SCISSORS
        case  2: // SCISSORS vs ROCK
            return 2;
            
        default: // ROCK vs ROCK || PAPER vs PAPER || SCISSOR vs SCISSORS || Error
            return 0;
        }
    }
}
