
using System;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private Text fruitsCountText;
    [SerializeField] private Text lifeCountText;

    private void Start()
    {
        GameManager.Instance.OnChangeLife += OnChangeLifeHandler;
        GameManager.Instance.OnChangeFruits += OnChangeFruitHandler;
    }

    private void OnChangeLifeHandler(int newLife)
    {
        lifeCountText.text = newLife.ToString();
    }
    
    private void OnChangeFruitHandler(int newFruit)
    {
        fruitsCountText.text = newFruit.ToString();
    }
}