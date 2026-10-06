using Solution;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class NPC : Identity
{
    public DialogueUI dialogueUI;
    public DialogueSequen sequen;
    public bool canTalk = true;

    private void Awake()
    {
        // ดึง DialogueSequen ที่ติดอยู่บน GameObject เดียวกันมาใช้อัตโนมัติ
        if (sequen == null)
        {
            sequen = GetComponent<DialogueSequen>();
        }
    }

    public override bool Hit()
    {
        // ตรวจสอบว่าสามารถคุยได้หรือไม่
        if (canTalk)
        {
            if (dialogueUI != null && sequen != null)
            {
                dialogueUI.Setup(sequen);
            }
            else
            {
                Debug.LogWarning("DialogueUI หรือ DialogueSequen ยังไม่ได้ตั้งค่าใน Inspector!");
            }
            return false;
        }
        else
        {
            Debug.Log("I not need to talk to you");
            return false;
        }
    }
}