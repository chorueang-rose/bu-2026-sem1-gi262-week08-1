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
        // 0. สร้าง instance ของ Skill แต่ละตัว
        attack = new Skill("Attack");
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

        // 1. set the nextSkills for each skill
        attack = new Skill("Attack");
        attack.isAvailable = true;
        
        fireStorm = new Skill("FireStorm");
        fireBlast = new Skill("fireBlast");
        fireBall = new Skill("fireBall");
        fireWave = new Skill("fireWave");
        fireExplosion = new Skill("fireExplosion");

        fireWave = new Skill("fireWave");
        fireExplosion = new Skill("fireExplosion");

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

        // [5] Attack -> FireStorm (ตั้งค่าสกิลแรก Attack ให้เป็น Available เพื่อเปิดใช้งานเริ่มต้น)
        attack.isAvailable = true;

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

    public void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.pKey.wasPressedThisFrame)
        {
            attackSkillTree.rootSkill.PrintSkillTreeHierarchy("");
            // attackSkillTree.rootSkill.PrintSkillTree();
            Debug.Log("====================================");
        }
    }
}
