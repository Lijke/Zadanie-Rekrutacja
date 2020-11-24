using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public int enemyHit=0;
    public int playerHit=0;
   
    public void addEnemyHit(int point)
    {
        enemyHit += point;
    }
    public void addPlayerHit(int point)
    {
        playerHit += 1;
    }
    public void returnScore()
    {
        if(playerHit>0)
        {
            //defeat;
        }
        else
        {
            //congrats next level;
        }
    }

}
