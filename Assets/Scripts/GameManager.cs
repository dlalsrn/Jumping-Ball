using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int TotalScore { get; private set; }
    
    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
        TotalScore = GameObject.FindGameObjectsWithTag("Item").Length; // Item의 총 개수
        HUDManager.Instance.UpdateStageItemText(Score, TotalScore);
    }

    public void AddScore()
    {
        Score++;
        HUDManager.Instance.UpdateStageItemText(Score, TotalScore);
    }
}
