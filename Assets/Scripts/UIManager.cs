using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _livesImages;

    [SerializeField]
    private Image _livesImageDisplay;

    [SerializeField]
    private GameObject _titleScreenImage;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private int _score;


    public void updateLives(int currentLives)
    {
        _livesImageDisplay.sprite = _livesImages[currentLives];
    }

    public void printScore()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void updateScore()
    {
        _score++;
        printScore();
    }

    public void showTitleScreen()
    {
        _titleScreenImage.SetActive(true);
        
    }
    public void hideTitleScreen()
    {
        _titleScreenImage.SetActive(false);
    }

    public void resetUI()
    {
        _score = 0;
    }
}
