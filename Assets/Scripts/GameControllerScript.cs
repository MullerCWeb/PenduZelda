using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attemptsLeft;
    [SerializeField] private string attemptsLeftLabel;
    private const int MaxErrors = 6;
    private int _attemptsLeftCount = MaxErrors;
    
    [SerializeField] private TextMeshProUGUI hiddenWord;
    private const string WordToFind = "HYLIA";
    private char[] _guessingWord;
    private int _wordLength;
    private const char HidingCharacter = '_';
    
    [SerializeField] private TextMeshProUGUI usedLetters;
    private List<char> _usedLettersList = new List<char>();  
    
    [SerializeField] private TMP_InputField letterInput;
    [SerializeField] private Button submitButton;
    
    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _attemptsLeftCount = MaxErrors;
        attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();

        _wordLength = WordToFind.Length;
        _guessingWord = new string(HidingCharacter, _wordLength).ToCharArray();
        hiddenWord.text = new string(_guessingWord);

        usedLetters.text = "";
        _usedLettersList.Clear();
        
        //TODO handle input form and submit button initialization
    }
}
