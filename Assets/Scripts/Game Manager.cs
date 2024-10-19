using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [HideInInspector] public bool isSelected;
    [HideInInspector] public GameObject selectedObj;

    public GameObject[] tubes;
    public bool[] levelTubesCompleted;

    bool isGameCompleted;

    [SerializeField] GameObject GameCompleteCanvas;

    private void Start()
    {
        isGameCompleted = false;
        isSelected = false;
    }

    private void OnDestroy()
    {
        Destroy(instance);
    }

    public void ResetSelected()
    {
        for(int i = 0; i < tubes.Length; i++)
        {
            tubes[i].gameObject.transform.GetChild(3).gameObject.transform.GetComponent<Tube>().isSelectedThis = false;
        }
    }

    public void CheckGameComplete()
    {
        for(int i = 0;i < levelTubesCompleted.Length; i++)
        {
            if (levelTubesCompleted[i] == false)
            {
                isGameCompleted = false;
            }
            else if(levelTubesCompleted[i] == true)
            {
                isGameCompleted = true;
            }
        }

        if(isGameCompleted)
        {
            StartCoroutine(WaitAndComplete());
            Debug.Log("Game Completed");
        }
    }

    IEnumerator WaitAndComplete()
    {
        isGameCompleted = true;
        yield return new WaitForSeconds(1f);
        GameCompleteCanvas.SetActive(true);
    }

    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
