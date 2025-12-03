using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hiddenWord; 
    [SerializeField] private TMP_InputField letterInput;
    [SerializeField] private TextMeshProUGUI usedLetters;
    [SerializeField] private TextMeshProUGUI attemptsLeft;
    [SerializeField] private Button submitButton;
    [SerializeField] private Image hangmanImage;
    [SerializeField] private Sprite[] hangmanStates;
    
    [SerializeField] private string attemptsLeftLabel = "Tentatives restantes : ";

    private const int MaxErrors = 6;
    
    private const string WordToFind = "HYLIA";
    private int _wordLength; // Used multiple times in this script
    private const char HidingCharacter = '_';
    private char[] _guessingWord; // Hidden word with guessed letters
    private readonly List<char> _usedLettersList = new List<char>(); // List of all used letters
    private int _attemptsLeftCount; // Number of attempts left, decreasing with each mistake
    
    private void Start()
    {
        InitializeGame();
    }

    // Initializing in Start to make it easier to replay
    private void InitializeGame()
    {
        _wordLength = WordToFind.Length;
        
        // Filling the dashedWord with as many dashes as letters in the word to guess
        _guessingWord = new string(HidingCharacter, _wordLength).ToCharArray();
        
        // Updating the dashed word text
        hiddenWord.text = new string(_guessingWord);
        
        // Clearing all potential letters listed in usedLetters
        _usedLettersList.Clear(); 
        usedLetters.text = "";
        
        // Setting back the number of attempts left to the max allowed
        _attemptsLeftCount = MaxErrors;
        attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();
        
        // Emptying the letter input and putting the focus on it
        EmptyLetterInput();
        
        SwitchSprite();
    }

    private void EmptyLetterInput()
    {
        letterInput.text = "";
        letterInput.interactable = true;
        letterInput.ActivateInputField();
    }

    // Validating letter input
    public void CheckLetterFromInput()
    {
        // Save the player input to a local variable
        string input = letterInput.text.Trim().ToUpper();
        
        // Emptying the letter input and putting the focus on it
        EmptyLetterInput();
        
        // Testing the input
        if (!ValidateInput(input))
        {
            return;
        }
        
        //Clearing the console
        Debug.ClearDeveloperConsole();
        
        // Converting to a char instead of a string
        char letter = input[0];
        
        // Adding the letter to the list of used letters
        _usedLettersList.Add(letter);
        usedLetters.text = string.Join(", ", _usedLettersList);
        
        // Checking if the letter is in the word to guess
        bool letterIsFound = false;
        for (int i = 0; i < _wordLength; i++)
        {
            if (WordToFind[i] == letter) // Considering WordToFind is written in all caps
            {
                _guessingWord[i] = letter;
                letterIsFound = true;
            }
        }
        
        if (letterIsFound)
        {
            hiddenWord.text = new string(_guessingWord);
        }
        else
        {
            // If not in the word
            _attemptsLeftCount--;
            attemptsLeft.text = attemptsLeftLabel + _attemptsLeftCount.ToString();
        }
        
        SwitchSprite();
        
        CheckGameOver();
    }

    private void SwitchSprite()
    {
        hangmanImage.sprite = hangmanStates[MaxErrors - _attemptsLeftCount];
    }

    private bool ValidateInput(string inputString)
	{
		if (string.IsNullOrEmpty(inputString) || inputString.Length != 1)
		{
		    Debug.LogWarning("Entrée invalide : veuillez n'entrer qu'une seule lettre.");
		    return false;
		}
		
		if (!char.IsLetter(inputString[0]))
		{
		    Debug.LogWarning("Entrée invalide : veuillez entrer une lettre alphabétique.");
		    return false;
		}
		
		if (_usedLettersList.Contains(inputString[0]))
		{
		    Debug.LogWarning("Lettre déjà utilisée.");
		    return false;
		}
		return true;
	}


    private void CheckGameOver()
    {
        if (_attemptsLeftCount <= 0) // The player lost the game
        {
            hiddenWord.text = WordToFind;
            letterInput.interactable = false;
            letterInput.DeactivateInputField();
            submitButton.interactable = false;
            attemptsLeft.color = Color.red;
        } else if (System.Array.IndexOf(_guessingWord, HidingCharacter) == -1) // The player won the game
        {
            letterInput.interactable = false;
            letterInput.DeactivateInputField();
			submitButton.interactable = false;
            attemptsLeft.color = Color.green;
        }
    }
}
