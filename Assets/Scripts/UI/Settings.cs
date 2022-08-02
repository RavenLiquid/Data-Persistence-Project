using System;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private ColorPicker paddleColorPicker;
        [SerializeField]
        private ColorPicker ballColorPicker;

        private void Start()
        {
            paddleColorPicker.Init();
            ballColorPicker.Init();

            paddleColorPicker.onColorChanged += NewPaddleColorSelected;
            ballColorPicker.onColorChanged += NewBallColorSelected;

            paddleColorPicker.SelectColor(DataManager.Instance.PaddleColor);
            ballColorPicker.SelectColor(DataManager.Instance.BallColor);
        }

        public void NewPaddleColorSelected(Color color)
        {
            DataManager.Instance.PaddleColor= color;
        }
        
        public void NewBallColorSelected(Color color)
        {
            DataManager.Instance.BallColor = color;
        }

        public void Back()
        {
            SceneManager.LoadScene(SceneNames.StartMenu);
        }
    }
}
