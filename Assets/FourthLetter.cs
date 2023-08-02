using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ThirdLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter3;
    public Transform Name;
    PlayerInput controls;
    public PlayerController pc;
   // public GameObject ALetter;
    [SerializeField] RectTransform ScrollContent;
    




   void Awake()
    {   
        pc.isSleeping = true;
        controls = new PlayerInput();
        controls.Pause.ScrollUp.performed += ctx => ScrollUp();  
        controls.Pause.ScrollDown.performed += ctx => ScrollDown();
       // EventSystem.current.SetSelectedGameObject(ALetter);

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
