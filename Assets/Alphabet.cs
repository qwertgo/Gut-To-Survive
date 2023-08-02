using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Alphabet : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter1;
    public Transform FirstLetter;
    PlayerInput controls;
    public PlayerController pc;
    [SerializeField] RectTransform ScrollContent;
    
    Alphabet alphabet;




   void Awake()
    {   
        pc.isSleeping = true;
        controls = new PlayerInput();
        controls.Pause.ScrollUp.performed += ctx => ScrollUp();  
        controls.Pause.ScrollDown.performed += ctx => ScrollDown();
        controls.Pause.SelectRight.performed += ctx => SelectRight();
    }

    public void A()
    {   
        
        string Letter1 = "A";
        Debug.Log(Letter1);
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
        

    }

     public void B()
    {
        string Letter1 = "B";
        Debug.Log(Letter1);
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    }

     public void C()
    {
        string Letter1 = "C";
        Debug.Log(Letter1);
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;

    }

     public void D()
    {
        string Letter1 = "D";
        Debug.Log(Letter1);
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;

    }

     public void E()
    {
        string Letter1 = "E";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    }

    public void F()
    {
        string Letter1 = "F";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void G()
    {
        string Letter1 = "G";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void H()
    {
        string Letter1 = "H";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void I()
    {
        string Letter1 = "I";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    public void J()
    {
        string Letter1 = "J";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void K()
    {
        string Letter1 = "K";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void L()
    {
        string Letter1 = "L";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void M()
    {
        string Letter1 = "M";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void N()
    {
        string Letter1 = "N";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void O()
    {
        string Letter1 = "O";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void P()
    {
        string Letter1 = "P";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    }
    
    public void Q()
    {
        string Letter1 = "Q";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void R()
    {
        string Letter1 = "R";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void S()
    {
        string Letter1 = "S";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void T()
    {
        string Letter1 = "T";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void U()
    {
        string Letter1 = "U";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void V()
    {
        string Letter1 = "V";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void W()
    {
        string Letter1 = "W";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void X()
    {
        string Letter1 = "X";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void Y()
    {
        string Letter1 = "Y";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void Z()
    {
        string Letter1 = "Z";
        FirstLetter.Find("FirstLetter").GetComponent<TextMeshProUGUI>().text =Letter1;
    }


void ScrollUp()
    {
    Debug.Log("Input");
    ScrollContent.transform.position = new Vector2(ScrollContent.transform.position.x,ScrollContent.transform.position.y + ScrollContent.rect.height  *-0.00038462f);

    }

     void ScrollDown()
    {
    Debug.Log("Input");
    ScrollContent.transform.position = new Vector2(ScrollContent.transform.position.x,ScrollContent.transform.position.y + ScrollContent.rect.height  *0.0003846f);
    }

    void SelectRight() 
    { 
      alphabet.enabled = false;
    }


     void OnEnable()
     {
       controls.Pause.Enable();
     }

      void OnDisable()
     {
       controls.Pause.Disable();
     }
}
