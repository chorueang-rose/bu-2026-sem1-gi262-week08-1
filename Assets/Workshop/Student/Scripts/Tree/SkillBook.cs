using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


    public class SkillBook : MonoBehaviour
    {
        public SkillTree attackSkillTree;

        Skill attack;
        Skill fireStorm;
        Skill fireBall;
        Skill fireBlast;
        Skill fireWave;
        Skill fireExplosion;

        public void Start()
        {
        // build skill tree
        // └── Attack
        //     └── FireStorm
        //         ├── FireBlast
        //         └── FireBall
        //             └── FireWave
        //                 └── FireExplosion

        // 1. set the nextSkills for each skill
        attack = new Skill("Attack");
        attack.isAvailable = true;
        
        fireStorm = new Skill("FireStorm");
        fireBlast = new Skill("fireBlast");
        fireBall = new Skill("fireBall");
        fireWave = new Skill("fireWave");
        fireExplosion = new Skill("fireExplosion");

        fireStorm.nextSkills.Add(fireBall);
        fireStorm.nextSkills.Add(fireBlast);

        fireBall.nextSkills.Add(fireWave);
        fireWave.nextSkills.Add(fireExplosion);

        attack.nextSkills.Add(fireStorm);

        this.attackSkillTree = new SkillTree(attack);
        }

        public void Update()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && keyboard.pKey.wasPressedThisFrame)
            {
                attackSkillTree.rootSkill.PrintSkillTreeHierarchy("");
                //attackSkillTree.rootSkill.PrintSkillTree();
                Debug.Log("====================================");
            } 
        }
    }

