using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    [SerializeField] private int indexSceneLevel;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private void Start()
    {
        var scene = PlayerPrefs.GetInt("LevelScene", 7);

        if (scene < indexSceneLevel)
        {
            GetComponent<Button>().interactable = false;
        }
        GetComponent<Button>().onClick.AddListener(OnChangeSceneLevelHandler);

        var stars = PlayerPrefs.GetInt("LevelStars", 0);

        switch (stars)
        {
            case 0:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;
            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
            default:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
        }
    }

    private void OnChangeSceneLevelHandler()
    {
        SceneManager.LoadScene(indexSceneLevel);
    }
}
