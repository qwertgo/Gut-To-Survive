using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

public class FirstLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter1;
    public Transform Name;
    PlayerInput controls;
    public PlayerController pc;
    public GameObject ALetter;
    [SerializeField] RectTransform ScrollContent;
    




   void Awake()
    {   
        pc.isSleeping = true;
        controls = new PlayerInput();
        controls.Pause.ScrollUp.performed += ctx => ScrollUp();  
        controls.Pause.ScrollDown.performed += ctx => ScrollDown();
        EventSystem.current.SetSelectedGameObject(ALetter);

    }

    public void A()
    {   
        
        string Letter1 = "A";
        Debug.Log(Letter1);
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
        

    }

     public void B()
    {
        string Letter1 = "B";
        Debug.Log(Letter1);
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    }

     public void C()
    {
        string Letter1 = "C";
        Debug.Log(Letter1);
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;

    }

     public void D()
    {
        string Letter1 = "D";
        Debug.Log(Letter1);
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;

    }

     public void E()
    {
        string Letter1 = "E";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    }

    public void F()
    {
        string Letter1 = "F";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void G()
    {
        string Letter1 = "G";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void H()
    {
        string Letter1 = "H";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void I()
    {
        string Letter1 = "I";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    public void J()
    {
        string Letter1 = "J";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void K()
    {
        string Letter1 = "K";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void L()
    {
        string Letter1 = "L";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void M()
    {
        string Letter1 = "M";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void N()
    {
        string Letter1 = "N";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void O()
    {
        string Letter1 = "O";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void P()
    {
        string Letter1 = "P";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    }
    
    public void Q()
    {
        string Letter1 = "Q";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void R()
    {
        string Letter1 = "R";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void S()
    {
        string Letter1 = "S";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void T()
    {
        string Letter1 = "T";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void U()
    {
        string Letter1 = "U";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void V()
    {
        string Letter1 = "V";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void W()
    {
        string Letter1 = "W";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void X()
    {
        string Letter1 = "X";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void Y()
    {
        string Letter1 = "Y";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
    } 
    
    public void Z()
    {
        string Letter1 = "Z";
        Name.Find("FirstLetterText").GetComponent<TextMeshProUGUI>().text =Letter1;
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

}
