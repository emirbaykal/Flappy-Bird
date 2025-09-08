using System;
using MainFolder.Scripts.Zenject.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace MainFolder.Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Elements")]
        /*
         * In game panels
         */
        [Header("Panels")]
        [SerializeField]
        private GameObject gameOverScreen;

        [SerializeField]
        private GameObject scoreboard;
        
        
        /*
         * These variables are used for score counter control.
         */
        [Header("Scoreboard")]
        
        [SerializeField]
        private GameObject[] numberPrefabs;
        
        [SerializeField]
        private SpriteAtlas numbersAtlas;
        
        [SerializeField]
        private string resetScoreSpriteName;
        
        [Header("Game Over Panel")]
        [SerializeField]
        private TMP_Text scoreText;
        
        //Zenject variable
        private GameManager _gameManager;
        private SignalBus _bus;

        [Inject]
        public void Construct(GameManager gameManager, SignalBus bus)
        {
            _gameManager = gameManager;
            _bus = bus;
        }

        private void OnEnable()
        {
            _bus.Subscribe<GameOver>(OpenGameOverPanel);
            _bus.Subscribe<RestartGame>(ResetScoreboard);
            _bus.Subscribe<ColumnSuccess>(UpdateScore);
        }

        private void OnDisable()
        {
            _bus.Unsubscribe<ColumnSuccess>(UpdateScore);
            _bus.Unsubscribe<GameOver>(OpenGameOverPanel);
            _bus.Unsubscribe<RestartGame>(ResetScoreboard);
        }

        public void UpdateScore()
        {
            int currentScore = _gameManager.AddScore(); 
            string scoreString = currentScore.ToString();
        
            for (int i = 0; i <= currentScore.ToString().Length - 1; i++)
            {
                numberPrefabs[i].gameObject.SetActive(true);
                //This was done because it is necessary to call them in the form 1_0 due to the name error in the assets that were ready to use.
                numberPrefabs[i].GetComponent<Image>().sprite = numbersAtlas.GetSprite(int.Parse(scoreString[i].ToString()) + "_0");
            }
        }

        public void ResetScoreboard()
        {
            numberPrefabs[0].GetComponent<Image>().sprite = numbersAtlas.GetSprite(resetScoreSpriteName);
            for (int i = 1; i < numberPrefabs.Length; i++)
            {
                numberPrefabs[i].gameObject.SetActive(false);
            }
            gameOverScreen.SetActive(false);
            scoreboard.SetActive(true);
        }

        public void OpenGameOverPanel()
        {
            scoreboard.SetActive(false);
            gameOverScreen.SetActive(true);
            scoreText.text += _gameManager.score;
        }
    }
}
