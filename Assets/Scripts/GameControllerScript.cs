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
        
        EmptyLetterInput();
        submitButton.interactable = true;

    }

    void EmptyLetterInput()
    {
        letterInput.text = "";
        letterInput.interactable = true;
        letterInput.ActivateInputField();
    }

    public void CheckLetterFromInput()
    {
        string input = letterInput.text.Trim().ToUpper();

        EmptyLetterInput();

        bool inputIsValid = ValidateInput(input);
        if (!inputIsValid)
        {
            // On stoppe la vérification parce que la lettre est invalide
            return;
        }
        
        // Quand on est ici, input est forcément valide
        char letter = input[0];

        _usedLettersList.Add(letter);
        usedLetters.text = string.Join(", ", _usedLettersList);

        bool letterIsFound = false;
        for (int i = 0; i < _wordLength; i++)
        {
            if (letter == WordToFind[i])
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
            // Donne la solution au joueur
            hiddenWord.text = WordToFind;
            
            //Désactive le champ de texte + le bouton
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();
            
            // Change le texte tentatives restantes en rouge
            attemptsLeft.color = Color.red;
        }
        else if (System.Array.IndexOf(_guessingWord, HidingCharacter) == -1) // le joueur a gagné
        {
            //Désactive le champ de texte + le bouton
            letterInput.interactable = false;
            submitButton.interactable = false;
            letterInput.DeactivateInputField();
            
            // Change le texte tentatives restantes en rouge
            attemptsLeft.color = Color.green;
        }
    }

    bool ValidateInput(string inputString)
    {
        if (inputString.Length != 1 || string.IsNullOrEmpty(inputString))
        {
            Debug.LogWarning("Entrée invalide : veuillez n'entrer qu'une seule lettre");
            return false;
        }

        if (!char.IsLetter(inputString[0]))
        {
            Debug.LogWarning("Entrée invalide : veuillez entrer une lettre alphabétique");
            return false;
        }

        if (_usedLettersList.Contains(inputString[0]))
        {
            Debug.LogWarning("La lettre a déjà été utilisée");
            return false;
        }

        Debug.Log("La chaîne entrée par l'utilisateur est valide !");
        return true;
    }
}
