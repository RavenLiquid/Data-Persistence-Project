using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public string PlayerName;

    public Color PaddleColor = Color.gray;
    
    public Color BallColor = Color.gray;

    public HighScoreEntry HighScore => HighScoreEntries.FirstOrDefault();

    /// <summary>
    /// Give a read only list of high scores. Editing is only via AddHighScore
    /// </summary>
    public IReadOnlyList<HighScoreEntry> HighScoreEntries =>
        highScoreEntries.OrderByDescending(hse => hse.HighScore).ToList().AsReadOnly();

    private List<HighScoreEntry> highScoreEntries = new List<HighScoreEntry>();

    private static string savePath;
    private int maxHighScores = 10;

    private void Awake()
    {
        savePath = $"{Application.persistentDataPath}/savefile.json";

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadAllData();
    }

    /// <summary>
    /// Save all data to file
    /// </summary>
    public void SaveAllData()
    {
        var data = new SaveData
        {
            HighScores =  highScoreEntries,
            PlayerName = PlayerName,
            BallColor = BallColor,
            PaddleColor = PaddleColor
        };

        var json = JsonUtility.ToJson(data);

        File.WriteAllText(savePath, json);
    }

    /// <summary>
    /// Read all data from file
    /// </summary>
    public void LoadAllData()
    {
        if (!File.Exists(savePath)) return;

        var json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<SaveData>(json);
        PlayerName = data.PlayerName;
        highScoreEntries = data.HighScores;
        PaddleColor = data.PaddleColor;
        BallColor = data.BallColor;
    }

    /// <summary>
    /// Add a high score entry to the list, drop the last entry if it is higher and the max is reached
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="score">High score</param>
    public void AddHighScore(int score)
    {
        // Check if there is space on the high score list
        if (highScoreEntries.Count > maxHighScores)
        {
            // Check if the new score is higher than the lowest entry
            if (highScoreEntries.Min(hse => hse.HighScore) < score)
                highScoreEntries.RemoveAt(highScoreEntries.Count - 1);
            else
                // Score is not higher than the lowest entry and the list is full, don't add it
                return;
        }

        highScoreEntries.Add(new HighScoreEntry
        {
            PlayerName = PlayerName,
            HighScore = score
        });
    }

    [Serializable]
    class SaveData
    {
        /// <summary>
        /// High score
        /// </summary>
        public List<HighScoreEntry> HighScores = new List<HighScoreEntry>();

        /// <summary>
        /// Last player name
        /// </summary>
        public string PlayerName;

        /// <summary>
        /// Color of the Paddle
        /// </summary>
        public Color PaddleColor;
        /// <summary>
        /// Color of the Ball
        /// </summary>
        public Color BallColor;
    }
}