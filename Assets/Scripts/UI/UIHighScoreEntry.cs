using TMPro;
using UnityEngine;

namespace UI
{
    public class UIHighScoreEntry : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI Index;
        [SerializeField]
        public TextMeshProUGUI PlayerName;
        [SerializeField]
        public TextMeshProUGUI Score;

        public void SetValues(int index, string playerName, int score)
        {
            Index.text = index.ToString();
            PlayerName.text = playerName;
            Score.text = score.ToString();
        }

        public void SetValues(int index, HighScoreEntry highScoreEntry)
        {
            SetValues(index, highScoreEntry.PlayerName, highScoreEntry.HighScore);
        }
    }
}
