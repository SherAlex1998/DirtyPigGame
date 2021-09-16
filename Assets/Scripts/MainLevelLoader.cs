using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainLevelLoader : MonoBehaviour
{
    public GameObject stone;

    private Vector3 firstStonePosition;
    private Vector3 perspectiveStoneScale;
    private GameObject level;
    private GameObject gameOverLabel;
    private GameObject gameOverLabelWin;
    private GameObject characters;
    private const string levelName = "Stones";
    private const string labelName = "GameOverLabel";
    private const string labelWinName = "GameOverLabelWin";
    private const string charactersName = "Characters";

    public static MainLevelLoader instance = null;

    public int scoreCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        firstStonePosition.Set(-7.23f, 3, 0);
        level = GameObject.Find(levelName);
        gameOverLabel = GameObject.Find(labelName);
        gameOverLabelWin = GameObject.Find(labelWinName);
        gameOverLabel.SetActive(false);
        gameOverLabelWin.SetActive(false);
        characters = GameObject.Find(charactersName);
        perspectiveStoneScale.Set(0.90f, 0.90f, 0.90f);
        SetStonesOnMap();
    }

    void Update()
    {
        
    }

    public void AddScore()
    {
        scoreCounter++;
        if(scoreCounter == 4)
        {
            GameOver(true);
        }
    }

    public void GameOver(bool isWin)
    {
        if(!isWin)
        {
            gameOverLabel.SetActive(true);
            characters.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            gameOverLabelWin.SetActive(true);
            characters.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void SetStonesOnMap()
    {
        Vector3 currentStonePosition = firstStonePosition;
        for (int i = 0; i < 4; i++)
        {
            float deltaX = 2f;
            for (int j = 0; j < 8; j++)
            {
                GameObject newStone = Instantiate(stone, level.transform);
                newStone.transform.position = currentStonePosition;
                newStone.transform.localScale = perspectiveStoneScale; 
                currentStonePosition.x += deltaX;
                deltaX += 0.04f;
            }
            currentStonePosition.y -= 2.0f;
            firstStonePosition.x -= 0.25f;
            currentStonePosition.x = firstStonePosition.x;
            perspectiveStoneScale.Set(
                perspectiveStoneScale.x += 0.04f,
                perspectiveStoneScale.y += 0.04f,
                perspectiveStoneScale.z);
        }
    }



}
