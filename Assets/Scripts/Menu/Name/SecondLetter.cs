using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class SecondLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter2;
    public Transform Name;
    PlayerInput controls;
    public PlayerController pc;
    PlayerInput inputs;

    [SerializeField] float controllerScrollSpeed;
    [SerializeField] RectTransform ScrollContentLetter2;
  

    
    Vector2 scrollDirection;
    




   void Awake()
    {   
        pc.isSleeping = true;
        inputs = new PlayerInput();

        ScrollContentLetter2 = ScrollContentLetter2.GetComponent<RectTransform>();


    }

    void Update()
    {
        ScrollContentLetter2.position += new Vector3(0, Time.deltaTime *  controllerScrollSpeed *scrollDirection.y, 0);

    }

    
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Movement.performed += OnMovement;
        inputs.Player.Movement.canceled += OnMovementCancelled;
    }

    private void OnDisable()
    {
        inputs.Disable();
        inputs.Player.Movement.performed -= OnMovement;
        inputs.Player.Movement.canceled -= OnMovementCancelled;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        scrollDirection = -context.ReadValue<Vector2>();
    }

    void OnMovementCancelled(InputAction.CallbackContext c)
    {
        scrollDirection = Vector2.zero;
    }

   
    

    public void A()
    {   
        
        string Letter2 = "A";
        Debug.Log(Letter2);
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
        

    }

     public void B()
    {
        string Letter2 = "B";
        Debug.Log(Letter2);
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    }

     public void C()
    {
        string Letter2 = "C";
        Debug.Log(Letter2);
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;

    }

     public void D()
    {
        string Letter2 = "D";
        Debug.Log(Letter2);
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;

    }

     public void E()
    {
        string Letter2 = "E";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    }

    public void F()
    {
        string Letter2 = "F";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void G()
    {
        string Letter2 = "G";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void H()
    {
        string Letter2 = "H";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void I()
    {
        string Letter2 = "I";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    public void J()
    {
        string Letter2 = "J";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void K()
    {
        string Letter2 = "K";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void L()
    {
        string Letter2 = "L";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void M()
    {
        string Letter2 = "M";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void N()
    {
        string Letter2 = "N";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void O()
    {
        string Letter2 = "O";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void P()
    {
        string Letter2 = "P";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    }
    
    public void Q()
    {
        string Letter2 = "Q";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void R()
    {
        string Letter2 = "R";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void S()
    {
        string Letter2 = "S";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void T()
    {
        string Letter2 = "T";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void U()
    {
        string Letter2 = "U";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void V()
    {
        string Letter2 = "V";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void W()
    {
        string Letter2 = "W";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void X()
    {
        string Letter2 = "X";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void Y()
    {
        string Letter2 = "Y";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    } 
    
    public void Z()
    {
        string Letter2 = "Z";
        Name.Find("SecondLetterText").GetComponent<TextMeshProUGUI>().text =Letter2;
    }



}
