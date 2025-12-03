using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attemptsLeft;
    [SerializeField] private TextMeshProUGUI hiddenWord;
    [SerializeField] private TextMeshProUGUI usedLetters;
    [SerializeField] private TMP_InputField letterInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private string attemptsLeftLabel;

    private int _attemptsLeftCount;
    private const int MaxErrors = 6;
    private const string WordToFind = "HYLIA";
    private int _wordLength;
    private const char HidingCharacter = '_';
    private List<char> _usedLettersList = new List<char>();
    
    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _attemptsLeftCount = MaxErrors;
        attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();

        _wordLength = WordToFind.Length;
        hiddenWord.text = new string(HidingCharacter, _wordLength);

        usedLetters.text = "";
        _usedLettersList.Clear();
        
        //TODO Handle inputs to activate them
    }
}
