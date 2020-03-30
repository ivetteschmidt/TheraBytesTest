using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ResetSkills);
    }

   // Reset skill tree
   void ResetSkills() {
        Buttons.reset = true; 
    }
}
