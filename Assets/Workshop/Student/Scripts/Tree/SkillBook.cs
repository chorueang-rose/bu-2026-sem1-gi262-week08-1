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
        // 0. สร้าง instance ของ Skill และตั้งค่าเริ่มต้น
        attack = new Skill("Attack");
        attack.isAvailable = true;

        fireStorm = new Skill("FireStorm");
        fireBlast = new Skill("FireBlast");
        fireBall = new Skill("FireBall");
        fireWave = new Skill("FireWave");
        fireExplosion = new Skill("FireExplosion");

        // build skill tree
        // └── Attack
        //     └── FireStorm
        //         ├── FireBlast
        //         └── FireBall
        //             └── FireWave
        //                 └── FireExplosion

        // [0] Attack -> FireStorm
        attack.nextSkills.Add(fireStorm);

        // [1] FireStorm -> FireBlast
        fireStorm.nextSkills.Add(fireBlast);

        // [2] FireStorm -> FireBall
        fireStorm.nextSkills.Add(fireBall);

        // [3] FireBall -> FireWave
        fireBall.nextSkills.Add(fireWave);

        // [4] FireWave -> FireExplosion
        fireWave.nextSkills.Add(fireExplosion);

        // สร้าง SkillTree
        this.attackSkillTree = new SkillTree(attack);
    }

    public void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.pKey.wasPressedThisFrame)
        {
            if (attackSkillTree != null && attackSkillTree.rootSkill != null)
            {
                attackSkillTree.rootSkill.PrintSkillTreeHierarchy("");
                Debug.Log("====================================");
            }
        }
    }
}
