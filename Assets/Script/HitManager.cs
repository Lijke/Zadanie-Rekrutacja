using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public int enemyHit=0;
    public int playerHit=0;
    public GameObject uiEndLevel;
    public GameObject player;
    private Camera m_camera;
    public List<Transform> nextStagePlayerTransform;
    public List<Transform> nextstageCameraTransform;
    private int currentStage = 0;
    public GameObject nextLevelHint;
    public GameObject deadHint;
    public List<float> countOfEnemy;
    public List<GameObject> levels;
    private void Awake()
    {
        player = GameObject.Find("Character");
        m_camera = Camera.main;
    }
    public void addEnemyHit(int point)
    {
        enemyHit += point;
    }
    public void addPlayerHit(int point)
    {
        playerHit += point;
    }
    public void returnScore()
    {
        if(playerHit==1)
        {
            StartCoroutine(Restart());
        }
        Debug.Log(enemyHit);
        Debug.Log(countOfEnemy[currentStage]);
        if(enemyHit==countOfEnemy[currentStage])
        {
            StartCoroutine(ShowMessage());
        }
        
    }
    public IEnumerator ShowMessage()
    {
        uiEndLevel.SetActive(true);
        yield return new WaitForSeconds(2);
        uiEndLevel.SetActive(false);
        enemyHit = 0;
        currentStage += 1;
        levels[currentStage - 1].SetActive(false);
        if(currentStage==3)
        {
            ShowMessageThenGoToNextLevel();
        }
        else
        {
            levels[currentStage].SetActive(true);
            player.transform.position = nextStagePlayerTransform[currentStage].transform.position;
            m_camera.transform.position = nextstageCameraTransform[currentStage].transform.position;
        }       
    }
    public IEnumerator Restart()
    {
        deadHint.SetActive(true);
        currentStage = -1;
        playerHit = 0;
        enemyHit = 0;
        yield return null;
    }
    public void ShowMessageThenGoToNextLevel()
    {
        nextLevelHint.SetActive(true);
    }

}
