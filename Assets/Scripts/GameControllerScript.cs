using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attemptsLeft;
    private const int MaxErrors = 6;
    private int _attemptsLeftCount = MaxErrors;
    [SerializeField] private string attemptsLeftLabel = "Tentatives restantes : ";
    
    [SerializeField] private TextMeshProUGUI hiddenWord;
    private const string WordToFind = "HYLIA";
    private int _wordLength;
    private const char HidingCharacter = '_';
    private char[] _guessingWord;
    
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
        hiddenWord.text = new string(HidingCharacter, _wordLength);
        _guessingWord = hiddenWord.text.ToCharArray();

        usedLetters.text = "";
        _usedLettersList.Clear();
        
        EmptyLetterInput();
        submitButton.interactable = true;
    }

    public void CheckLetterFromInput()
    {
        string input = letterInput.text.Trim().ToUpper();
        EmptyLetterInput();
        
        bool isInputValid = ValidateInput(input);
        if (!isInputValid)
        {
            return;
        }
        
        // Si on exécute les lignes ici, c'est que la lettre de l'utilisateur est valide
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

    private void CheckGameOver()
    {
        if (_attemptsLeftCount <= 0) // Si le joueur a perdu
        {
            hiddenWord.text = WordToFind;
            letterInput.interactable = false;
            letterInput.DeactivateInputField();
            submitButton.interactable = false;
            attemptsLeft.color = Color.red;
        }
        else if (System.Array.IndexOf(_guessingWord, HidingCharacter) == -1) // Si le joueur a gagné
        {
            letterInput.interactable = false;
            letterInput.DeactivateInputField();
            submitButton.interactable = false;
            attemptsLeft.color = Color.green;
        }
    }

    private void EmptyLetterInput()
    {
        letterInput.text = "";
        letterInput.interactable = true;
        letterInput.ActivateInputField();
    }

    private bool ValidateInput(string inputString)
    {
        if (inputString.Length != 1 || String.IsNullOrEmpty(inputString))
        {
            Debug.LogWarning("Entrée est invalide : veuillez n'entrer qu'une seule lettre");
            return false;
        }

        if (!Char.IsLetter(inputString[0]))
        {
            Debug.LogWarning("Entrée invalide : veuillez entrer une lettre alphabétique");
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
