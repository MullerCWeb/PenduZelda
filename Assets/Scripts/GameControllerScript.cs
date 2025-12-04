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

        usedLetters.text = "";
        _usedLettersList.Clear();
        
        //TODO Handle input & field reactivation
    }
}
