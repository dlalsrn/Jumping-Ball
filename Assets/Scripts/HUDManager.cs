using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI stageItemText;

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

    public void UpdateStageItemText(int score, int totalScore)
    {
        stageItemText.SetText($"{score} / {totalScore}");
    }
}
