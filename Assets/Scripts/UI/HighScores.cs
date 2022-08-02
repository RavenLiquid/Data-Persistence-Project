using System;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class HighScores : MonoBehaviour
    {
        [SerializeField]
        private UIHighScoreEntry highScoreEntryPrefab;
        [SerializeField] 
        private Transform HighScoreParent;

        /// <summary>
        /// Offset between entries
        /// </summary>
        private float yOffset = -50f;

        private void Start()
        {
            var highscores = DataManager.Instance.HighScoreEntries;

            // Create a highscore entry for each high score and position
            for (var index = 0; highscores.Count > index; index++)
            {
                var entry = highscores[index];
                var entryObject = Instantiate(highScoreEntryPrefab, transform);
                entryObject.SetValues(index, entry);

                entryObject.transform.SetParent(HighScoreParent);
                entryObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yOffset * index, 0);
            }
        }

        public void Back()
        {
            SceneManager.LoadScene(SceneNames.StartMenu);
        }
    }
}
