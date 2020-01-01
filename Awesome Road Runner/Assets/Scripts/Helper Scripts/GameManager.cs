using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public int starScore, scoreCount, selectedIndex;
    [HideInInspector] public bool[] heroes;
    [HideInInspector] public bool playSound = true;

    private GameData gameData;

    private string dataPath = "GameData.dat";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

        InitializeGameData();
    }

    void InitializeGameData()
    {
        LoadGameData();

        if (gameData == null)
        {
            // we are running our game for the first time
            // setup initial values
            starScore = 0;
            scoreCount = 0;
            selectedIndex = 0;

            heroes = new bool[9];
            heroes[0] = true;
            for (int i = 1; i < heroes.Length; i++)
                heroes[i] = false;

            gameData = new GameData();
            gameData.StarScore = starScore;
            gameData.ScoreCount = scoreCount;
            gameData.Heroes = heroes;
            gameData.SelectedIndex = selectedIndex;

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + dataPath);

            if (gameData != null)
            {
                gameData.Heroes = heroes;
                gameData.StarScore = starScore;
                gameData.ScoreCount = scoreCount;
                gameData.SelectedIndex = selectedIndex;

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    private void LoadGameData()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + dataPath, FileMode.Open);

            gameData = (GameData) bf.Deserialize(file);

            if (gameData != null)
            {
                starScore = gameData.StarScore;
                scoreCount = gameData.ScoreCount;
                heroes = gameData.Heroes;
                selectedIndex = gameData.SelectedIndex;
            }
        } catch (Exception e)
        {

        } finally
        {
            if (file != null)
                file.Close();
        }
    }
}
