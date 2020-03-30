using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "SO_Skill")]
public class Skills : ScriptableObject
{
    // required
    public string skillName;
    [SerializeField] internal bool isLocked;
    [SerializeField] internal bool canBeUnlocked;

    // other stuff
    [SerializeField] private ScriptableObject[] requiredSkills;  
    [SerializeField] public Sprite icon;



    // ------------------------ public methods ------------------------------------------


    // Check if skill is locked or can be unlocked
    public void Check(PlayerState player) {
        isLocked = !IsPlayerSkill(player);
        canBeUnlocked = CheckRequirements(player); 
    }


    /// Learn skill 
    /// if player doesn't already know it &
    /// if player fullfills all requirements
    public bool AddSkill(PlayerState player) {
        //Debug.Log("Adding Skill"); 
        if (IsPlayerSkill(player)) {
            //Debug.Log("Skill could not be added. - You already have this skill.");
            return false;
        }

        if (CheckRequirements(player)) {
            List<Skills>.Enumerator skills = player.skills.GetEnumerator();
            player.skills.Add(this);
            //Debug.Log("Requirements have been met. - Adding skill.");
            return true;
        } else {
            //Debug.Log("Skill could not be added. - You don't have the required skills.");
            return false;
        }
    }




    // ------------------------ private methods ----------------------------------------


    // Check if player has this skill 
    private bool IsPlayerSkill(PlayerState player) {
        //Debug.Log("Check if player skill"); 

        List<Skills>.Enumerator skills = player.skills.GetEnumerator();

        while (skills.MoveNext()) {
            var currentSkill = skills.Current;
            if(currentSkill.name == this.name) {
                //player has this skill
                //Debug.Log(skillName + " is a player skill");
                return true;
            } 
        }

        //player does NOT have this skill
        //Debug.Log(skillName + " is NOT a player skill");       
        return false; 
    }


    // Check if player meets skill requirements
    private bool CheckRequirements(PlayerState player) {
        //Debug.Log("CheckRequirements");

        //if no skills are required, no check is required. player meets the requirement
        if (requiredSkills.Length == 0) {   
            return true;
        }

        int checkedSkills = 0; //counts player skills that match the requirement

        for (int i = 0; i < requiredSkills.Length; i++) {

            // needs to be inside the for-loop so that skill acquirement order doesn't break the tree
            List<Skills>.Enumerator skills = player.skills.GetEnumerator(); 

            while (skills.MoveNext()) {
                var currentSkill = skills.Current;
                //Debug.Log(currentSkill.name + " " + requiredSkills[i].name + checkedSkills);
                if (currentSkill.name == requiredSkills[i].name) {
                    checkedSkills += 1;
                    //Debug.Log("inside: "+ currentSkill.name + " " + requiredSkills[i].name + checkedSkills);
                    break;
                }
            }
        }

        /// if amount of player skills matching the required skills equals the amount of 
        /// required skills the player meets the requirements
        if (checkedSkills == requiredSkills.Length) {     
            //Debug.Log(skillName + " can be unlocked");
            return true;
        } else {
            //Debug.Log(skillName + " can NOT be unlocked");
            return false; 
        }
    }

}
