using System;
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
        
        EmptyLetterInput();
    }

    public void CheckLetterFromInput()
    {
        string input = letterInput.text.ToUpper().Trim();
        EmptyLetterInput();
        
        bool letterIsValid = ValidateInput(input);
        if (!letterIsValid)
        {
            return;
        }
        
        // Si on atteint cette ligne de code, c'est que la lettre est valide
        // C'est ici que l'on fait toute la suite de notre algorithme
        char letter = input[0];
        _usedLettersList.Add(letter);
        usedLetters.text = string.Join(", ", _usedLettersList);

        bool letterIsFound = false;
        for (int i = 0; i < _wordLength; i++)
        {
            if (WordToFind[i] == letter)
            {
                _guessingWord[i] = letter;
                letterIsFound = true;
            }
        }

        hiddenWord.text = new string(_guessingWord);

        if (!letterIsFound)
        {
            _attemptsLeftCount--;
            attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();
        }

        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (_attemptsLeftCount <= 0) // Le joueur a perdu
        {
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();

            hiddenWord.text = WordToFind;

            attemptsLeft.color = Color.red;
        } else if (System.Array.IndexOf(_guessingWord, HidingCharacter) == -1) // le joueur a gagné
        {
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();

            attemptsLeft.color = Color.green;
        }
    }

    void EmptyLetterInput()
    {
        letterInput.text = "";
        letterInput.interactable = true;
        submitButton.interactable = true;
        letterInput.ActivateInputField();
    }

    bool ValidateInput(string inputString)
    {
        if (inputString.Length != 1 || String.IsNullOrEmpty(inputString))
        {
            Debug.LogWarning("Entrée invalide : veuillez écrire une seule lettre");
            return false;
        }

        if (!Char.IsLetter(inputString[0]))
        {
            Debug.LogWarning("Entrée invalide : veuillez entrer une lettre alphabéticale");
            return false;
        }
        
        if (_usedLettersList.Contains(inputString[0]))
        {
            Debug.LogWarning("La lettre a déjà été utilisée");
            return false;
        }

        return true;
    }
}
