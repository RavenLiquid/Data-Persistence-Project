using Helpers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI HighScore;

        [SerializeField]
        private TMP_InputField playerNameInput;

        private void Start()
        {
            // Get highscore and player name from 
            HighScore.text = $"Best Score : {DataManager.Instance.HighScore?.HighScore ?? 0}";
            playerNameInput.text = DataManager.Instance.PlayerName;
        }

        public void PlayerNameChanged(string newPlayerName)
        {
            DataManager.Instance.PlayerName = newPlayerName;
        }

        public void StartNew()
        {
            // Set player name, anonymous if none is provided
            playerNameInput.text = string.IsNullOrWhiteSpace(DataManager.Instance.PlayerName)
                ? "Anonymous"
                : DataManager.Instance.PlayerName;
            SceneManager.LoadScene(SceneNames.Main);
        }

        public void OpenSettings()
        {
            SceneManager.LoadScene(SceneNames.Settings);
        }

        public void OpenHighScores()
        {
            SceneManager.LoadScene(SceneNames.HighScores);
        }

        /// <summary>
        /// Exit and save data
        /// </summary>
        public void Exit()
        {
            DataManager.Instance.SaveAllData();
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }
    }
}
