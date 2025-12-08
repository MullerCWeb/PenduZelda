using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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
    private char[] _guessingWord;
    
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
        
        EmptyLetterInput();
        submitButton.interactable = true;
    }

    public void CheckLetterFromInput()
    {
        string input = letterInput.text.Trim().ToUpper();

        if (!ValidateInput(input))
        {
            // La lettre fournie n'est pas valide
            return; // on sort de la méthode CheckLetterFromInput
        }

        char letter = input[0];
        
        _usedLettersList.Add(letter);
        usedLetters.text = string.Join(", ", _usedLettersList);

        bool letterIsFound = false;
        for (int i = 0; i < _wordLength; i++)
        {
            if (letter == WordToFind[i])
            {
                Debug.Log("La lettre " + letter + " est dans le mot à trouver");
                _guessingWord[i] = letter;
                letterIsFound = true;
            }
        }
        hiddenWord.text = new string(_guessingWord);

        if (!letterIsFound)
        {
            // La lettre n'est pas dans le mot
            _attemptsLeftCount--;
            attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();
        }
        
        EmptyLetterInput();

        CheckGameOver();
    }

    private void EmptyLetterInput()
    {
        letterInput.text = "";
        letterInput.interactable = true;
        letterInput.ActivateInputField();
    }

    private void CheckGameOver()
    {
        if (_attemptsLeftCount <= 0) // Le joueur a perdu
        {
            hiddenWord.text = WordToFind;
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();
            attemptsLeft.color = Color.red;
        }
        else if (Array.IndexOf(_guessingWord, HidingCharacter) == -1) // Le joueur a gagné
        {
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();
            attemptsLeft.color = Color.green;
        }
    }

    private bool ValidateInput(string inputString)
    {
        if (inputString.Length != 1 || string.IsNullOrEmpty(inputString))
        {
            Debug.LogWarning("Chaîne invalide : veuillez n'entrer qu'une seule lettre");
            return false;
        }

        if (!char.IsLetter(inputString[0]))
        {
            Debug.LogWarning("Chaîne invalide : veuillez entrer une lettre alphabétique");
            return false;
        }

        if (_usedLettersList.Contains(inputString[0]))
        {
            Debug.LogWarning("Lettre a déjà été utilisée");
            return false;
        }
        
        Debug.Log("Chaîne valide");
        return true;
    }
}
