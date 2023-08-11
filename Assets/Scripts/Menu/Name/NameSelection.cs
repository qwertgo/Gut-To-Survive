using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NameSelection : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] float letterHeight;
    [SerializeField] float scrollSpeed;

    [Header("References")]
    [SerializeField] TextMeshProUGUI firstLetter;
    [SerializeField] TextMeshProUGUI secondLetter;
    [SerializeField] TextMeshProUGUI thirdLetter;
    [SerializeField] TextMeshProUGUI fourthLetter;

    [Space(20)]

    [SerializeField] RectTransform firstLetterBox;
    [SerializeField] RectTransform secondLetterBox;
    [SerializeField] RectTransform thirdLetterBox;
    [SerializeField] RectTransform fourthLetterBox;

    [Space(20)]

    [SerializeField] RectTransform currentLetterSelectionIcon;
    [SerializeField] Button saveButton;

    RectTransform selectedLetterBox;
    PlayerInput input;
    EventSystem ev;

    string playerName;
    int scrollDirection = 0;
    int positionOffset = 568; //idk Recttransform not working need to subtract sometimes

    char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

    bool isScrolling;

    private void Awake()
    {
        input = new PlayerInput();
        ev = EventSystem.current;

        
    }

    public void ActivateNameSelection()
    {
        selectedLetterBox = firstLetterBox;
        positionOffset = Mathf.RoundToInt(selectedLetterBox.position.y);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += OnMovement;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Jump.started += SelectLetter;
        input.Player.Back.started += DeleteLastLetter;

        
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovement;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        scrollDirection = -Mathf.RoundToInt(context.ReadValue<Vector2>().y);
    }

    void OnMovementCancelled(InputAction.CallbackContext context)
    {
        scrollDirection = 0;
    }

    void SelectLetter(InputAction.CallbackContext context)
    {
        
        int letterIndex = Mathf.RoundToInt((selectedLetterBox.position.y - positionOffset) / letterHeight);

        char letter = letters[letterIndex];
        playerName += letter;

        switch (playerName.Length -1)
        {
            case 0:
                firstLetter.text = ""+letter;
                selectedLetterBox = secondLetterBox;
                currentLetterSelectionIcon.position += new Vector3(250, 0, 0);
                break;
            case 1:
                secondLetter.text = "" + letter;
                selectedLetterBox = thirdLetterBox;
                currentLetterSelectionIcon.position += new Vector3(250, 0, 0);
                break;
            case 2:
                thirdLetter.text = "" + letter;
                selectedLetterBox = fourthLetterBox;
                currentLetterSelectionIcon.position += new Vector3(250, 0, 0);
                break;
            case 3:
                fourthLetter.text = "" + letter;
                currentLetterSelectionIcon.gameObject.SetActive(false);
                PlayerController.playerName = playerName;

                StopAllCoroutines();
                input.Player.Movement.performed -= OnMovement;
                input.Player.Movement.canceled -= OnMovement;
                input.Player.Jump.started -= SelectLetter;

                StartCoroutine(SelectButton());
                break;
        }
    }

    void DeleteLastLetter(InputAction.CallbackContext context)
    {
        Debug.Log(playerName.Length);
        switch (playerName.Length)
        {
            case 1:
                firstLetter.text = "";
                selectedLetterBox = firstLetterBox;
                currentLetterSelectionIcon.position -= new Vector3(250, 0, 0);
                break;
            case 2:
                secondLetter.text = "";
                selectedLetterBox = secondLetterBox;
                currentLetterSelectionIcon.position -= new Vector3(250, 0, 0);
                break;
            case 3:
                thirdLetter.text = "";
                selectedLetterBox = thirdLetterBox;
                currentLetterSelectionIcon.position -= new Vector3(250, 0, 0);
                break;
            case 4:
                fourthLetter.text = "";
                selectedLetterBox = fourthLetterBox;
                currentLetterSelectionIcon.gameObject.SetActive(true);
                ev.SetSelectedGameObject(null);

                input.Player.Movement.performed += OnMovement;
                input.Player.Movement.canceled += OnMovement;
                input.Player.Jump.started += SelectLetter;
                break;

        }

        playerName = playerName.Remove(playerName.Length - 1);
    }

    private void Update()
    {
        if(!isScrolling && scrollDirection != 0)
        {
            StartCoroutine(ScrollLetters());
        }
    }

    IEnumerator ScrollLetters()
    {
        isScrolling = true;
        while(scrollDirection != 0)
        {
            //Debug.Log(selectedLetterBox.position.y);
            int height = Mathf.RoundToInt(selectedLetterBox.position.y + scrollDirection * letterHeight);
            height = Mathf.Min(1040 + positionOffset, height);
            selectedLetterBox.position = new Vector3(selectedLetterBox.position.x, height, selectedLetterBox.position.z);

            yield return new WaitForSeconds(1 / scrollSpeed);
        }
        isScrolling = false;
    }

    IEnumerator SelectButton()
    {
        yield return null;
        saveButton.Select();
    }
}
