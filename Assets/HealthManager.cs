using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public List<GameObject> health;
    public GameObject Death;
    public int index = 2;
 
    public void removePointIfHitWall()
    {
        if(index>=0)
        {
            health[index].SetActive(false);
            index -= 1;
        }
        else if (index==-1)
        {
            //zrób gameover
        }
    }
}
