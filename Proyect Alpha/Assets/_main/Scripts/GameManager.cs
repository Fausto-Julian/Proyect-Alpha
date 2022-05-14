using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int playerMaxLife;
    [SerializeField] private int maxFruit;
    private int _playerLife;
    private int _playerFruit;
    
    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
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

    public void WinPlayer()
    {
        var star = 0;
        if (_playerFruit >= maxFruit)
        {
            star = 3;
        }
        else if (_playerFruit >= maxFruit / 2)
        {
            star = 2;
        }
        else if (_playerFruit > 0)
        {
            star = 1;
        }
        else if (_playerFruit <= 0)
        {
            star = 0;
        }

        PlayerPrefs.SetInt("LevelScene", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt($"LevelStars{SceneManager.GetActiveScene().buildIndex}", star);
        SceneManager.LoadScene(0);
    }

}