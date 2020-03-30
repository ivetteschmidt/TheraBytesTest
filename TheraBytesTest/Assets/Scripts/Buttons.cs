using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display skills
/// </summary>
public class Buttons : MonoBehaviour {

    public static bool reset;

    [SerializeField] private Skills skill;
    [SerializeField] private PlayerState player;

    //UI
    [SerializeField] private Text buttonText;
    [SerializeField] private Text buttonInfo;
    [SerializeField] private Material buttonIcon;



    // ------------------ private methods ---------------------


    // Setup
    private void Awake() {
        //Debug.Log("Awake");      
        setUI();
        this.GetComponent<Button>().onClick.AddListener(Skill);
    }


    private void Update() {
        //Debug.Log("updating " + skill.skillName);
        updateUI(); // update UI every frame  // XXX there is probably a more efficient way to handle this, but idk

        //handle reset
        if (reset) { 
            player.skills.Clear();
            updateUI();
            reset = false;
        }
    }


    /// Is activated on click
    /// either adds skill or activates skill
    private void Skill() {
        //Debug.Log("Skill");
        if (skill.AddSkill(player)) { //Adding skill
            //Debug.Log("Added " + skill.skillName);
            //Do this when adding skill (currently the same as default usage) 
            UseSkill();
        } else {
            //do this everytime you click on skill
            UseSkill();
        }
    }


    // Use skill
    private void UseSkill() {
        //Debug.Log("Using " + skill.skillName);

        buttonIcon.mainTexture = skill.icon.texture;
        this.GetComponentInChildren<ParticleSystem>().Play();
    }
    


    // Set names in UI
    private void setUI() {
        //Debug.Log("setUI");

        if (buttonText) {
            buttonText.text = skill.skillName;
            updateUI();
        }
    }


    // Update skill tree 
    private void updateUI() {
        //Debug.Log("UpdateUI");

        skill.Check(player);
        //Debug.Log(skill.skillName + skill.isLocked + skill.canBeUnlocked);
        if (skill.canBeUnlocked) {
            GetComponent<Button>().interactable = true;
            if (skill.isLocked) {
                buttonInfo.text = "Click to unlock";
                this.GetComponent<Image>().color = new Color(1f, 0.8f, 0f, 0.8f); // yellow; 
            } else {
                buttonInfo.text = "Click to use skill";
                this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.95f); // white;
            }
        } else {
            GetComponent<Button>().interactable = false;
            buttonInfo.text = "Unlock previous skills";
            this.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 0.6f); // transparent gray;
        }
    }

  
}
