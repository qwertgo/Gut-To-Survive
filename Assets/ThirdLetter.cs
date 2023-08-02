using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class FourthLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public string Letter4;
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
        
        string Letter4 = "A";
        Debug.Log(Letter4);
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
        

    }

     public void B()
    {
        string Letter4 = "B";
        Debug.Log(Letter4);
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    }

     public void C()
    {
        string Letter4 = "C";
        Debug.Log(Letter4);
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;

    }

     public void D()
    {
        string Letter4 = "D";
        Debug.Log(Letter4);
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;

    }

     public void E()
    {
        string Letter4 = "E";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    }

    public void F()
    {
        string Letter4 = "F";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void G()
    {
        string Letter4 = "G";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void H()
    {
        string Letter4 = "H";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void I()
    {
        string Letter4 = "I";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    public void J()
    {
        string Letter4 = "J";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void K()
    {
        string Letter4 = "K";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void L()
    {
        string Letter4 = "L";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void M()
    {
        string Letter4 = "M";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void N()
    {
        string Letter4 = "N";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void O()
    {
        string Letter4 = "O";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void P()
    {
        string Letter4 = "P";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    }
    
    public void Q()
    {
        string Letter4 = "Q";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void R()
    {
        string Letter4 = "R";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void S()
    {
        string Letter4 = "S";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void T()
    {
        string Letter4 = "T";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void U()
    {
        string Letter4 = "U";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void V()
    {
        string Letter4 = "V";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void W()
    {
        string Letter4 = "W";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void X()
    {
        string Letter4 = "X";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void Y()
    {
        string Letter4 = "Y";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
    } 
    
    public void Z()
    {
        string Letter4 = "Z";
        Name.Find("FourthLetterText").GetComponent<TextMeshProUGUI>().text =Letter4;
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
