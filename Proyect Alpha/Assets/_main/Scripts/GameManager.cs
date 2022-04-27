using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int playerMaxLife;
    private int _playerLife;
    private int _playerFruit;
    
    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _playerLife = playerMaxLife;
    }

    public void RestLife()
    {
        _playerLife--;
        if (_playerLife <= 0)
        {
            SceneManager.LoadScene(0);
            _playerLife = playerMaxLife;
        }
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("SpawnPointPlayer").transform.position;
    }

    public void AddFruit()
    {
        _playerFruit++;
    }

}