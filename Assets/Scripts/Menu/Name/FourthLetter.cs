using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class ThirdLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter3;
    public Transform Name;
    PlayerInput controls;
    public PlayerController pc;
   // public GameObject ALetter;
 PlayerInput inputs;

    [SerializeField] float controllerScrollSpeed;
    [SerializeField] RectTransform ScrollContentLetter4;
  

    
    Vector2 scrollDirection;
    




   void Awake()
    {   
        pc.isSleeping = true;
        inputs = new PlayerInput();

       
        ScrollContentLetter4 = ScrollContentLetter4.GetComponent<RectTransform>();


    }

    void Update()
    {
        ScrollContentLetter4.position += new Vector3(0, Time.deltaTime *  controllerScrollSpeed*scrollDirection.y, 0);

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
        
        string Letter3 = "A";
        Debug.Log(Letter3);
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
        

    }

     public void B()
    {
        string Letter3 = "B";
        Debug.Log(Letter3);
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    }

     public void C()
    {
        string Letter3 = "C";
        Debug.Log(Letter3);
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;

    }

     public void D()
    {
        string Letter3 = "D";
        Debug.Log(Letter3);
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;

    }

     public void E()
    {
        string Letter3 = "E";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    }

    public void F()
    {
        string Letter3 = "F";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void G()
    {
        string Letter3 = "G";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void H()
    {
        string Letter3 = "H";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void I()
    {
        string Letter3 = "I";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    public void J()
    {
        string Letter3 = "J";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void K()
    {
        string Letter3 = "K";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void L()
    {
        string Letter3 = "L";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void M()
    {
        string Letter3 = "M";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void N()
    {
        string Letter3 = "N";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void O()
    {
        string Letter3 = "O";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void P()
    {
        string Letter3 = "P";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    }
    
    public void Q()
    {
        string Letter3 = "Q";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void R()
    {
        string Letter3 = "R";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void S()
    {
        string Letter3 = "S";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void T()
    {
        string Letter3 = "T";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void U()
    {
        string Letter3 = "U";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void V()
    {
        string Letter3 = "V";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void W()
    {
        string Letter3 = "W";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void X()
    {
        string Letter3 = "X";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void Y()
    {
        string Letter3 = "Y";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    } 
    
    public void Z()
    {
        string Letter3 = "Z";
        Name.Find("ThirdLetterText").GetComponent<TextMeshProUGUI>().text =Letter3;
    }




}
