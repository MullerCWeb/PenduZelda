using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hiddenWord;
    [SerializeField] private TextMeshProUGUI attemptsLeft;
    [SerializeField] private TextMeshProUGUI usedLetters;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField letterInput;
    [SerializeField] private string attemptsLeftLabel;

    private const int MaxErrors = 6;
    private const string WordToFind = "HYLIA";
    private int _wordLength;
    private char[] _guessingWord; // Hidden word with guessed letters (array)
    private const char HidingCharacter = '_';
    private List<char> _usedLettersList = new List<char>();
    private int _attemptsLeftCount; // Number of attempts left, decreasing with each mistake
    
    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        _wordLength = WordToFind.Length;

        _guessingWord = new string(HidingCharacter, _wordLength).ToCharArray();

        hiddenWord.text = new string(_guessingWord); // Write the word with underscores

        usedLetters.text = "";
        _usedLettersList.Clear();

        _attemptsLeftCount = MaxErrors;
        attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();
        
        //TODO Handle input field and button reactivation
        
    }
}
