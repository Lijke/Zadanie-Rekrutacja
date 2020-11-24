using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public int enemyHit=0;
    public int playerHit=0;
    public GameObject uiEndLevel;
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
        if(enemyHit>0)
        {
            uiEndLevel.SetActive(true);
        }
        else
        {
            //congrats next level;
        }
    }

}
