using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequen : MonoBehaviour
{
    public DialogueTree tree;
    public DialogueNode currentNode;
    public DialogueUI dialogueUI;

    public void Start()
    {
        // 1. สร้างและตั้งค่า Dialogue Tree / call LoadConversation() to set up the dialogue tree
        LoadConversations();

        // ดึง DialogueUI จาก NPC Component (กรณีที่ยังไม่ได้ลากใส่ใน Inspector)
        if (dialogueUI == null)
        {
            NPC npc = GetComponent<NPC>();
            if (npc != null)
            {
                dialogueUI = npc.dialogueUI;
            }
        }

        // 2. กำหนด Current Node ให้เป็น Root ของ Tree
        if (tree != null)
        {
            currentNode = tree.root;
            // currentNode.Print(); // ปลดคอมเมนต์บรรทัดนี้ถ้าใน DialogueNode มีเมธอด Print()
        }
    }

    private void LoadConversations()
    {
        // 3. Create the dialogue nodes
        DialogueNode greeting = new DialogueNode("Ah, traveler! What brings you to this old place?");
        DialogueNode askForQuest = new DialogueNode("I have a task for you. There’s a beast in the woods. Can you take care of it?");
        DialogueNode questDenied = new DialogueNode("You're not ready for this yet. Come back when you're stronger.");
        DialogueNode directionsVillage = new DialogueNode("Follow the road south, and you’ll reach the village.");
        DialogueNode directionsForest = new DialogueNode("Head west, into the forest. But beware, it's dangerous.");
        DialogueNode goodbye = new DialogueNode("Safe travels, adventurer.");
        DialogueNode noIdea = new DialogueNode("I'm afraid I can't help you with that.");

        // Build the tree, adding custom responses
        // [1] add greeting's next node: askForQuest
        greeting.AddNext(askForQuest, "Can you give me a quest?");


        // [2] add greeting's next node: directionsVillage
        greeting.AddNext(directionsVillage, "Where is the village?");

        // [3] add greeting's next node: directionsForest
        greeting.AddNext(directionsForest, "How do I get to the forest?");

        // [4] add greeting's next node: goodbye
        greeting.AddNext(goodbye, "Goodbye.");

        // [5] add askForQuest's next node: questDenied
        askForQuest.AddNext(questDenied, "I’m ready for anything!");

        // [6] add askForQuest's next node: goodbye
        askForQuest.AddNext(goodbye, "Maybe later.");
        // 5. Set up the root of the dialogue tree
        tree = new DialogueTree(greeting);
    }

    // เมธอดสำหรับรับการเลือกจากปุ่ม UI
    public void SelectChoice(int index)
    {
        if (currentNode == null || currentNode.nexts == null) return;

        var choiceTextKeys = new List<string>(currentNode.nexts.Keys);

        if (index >= 0 && index < choiceTextKeys.Count)
        {
            string choiceKey = choiceTextKeys[index];

            // 1. เลื่อนไปยัง Dialogue Node ถัดไป
            currentNode = currentNode.nexts[choiceKey];

            // 2. ตรวจสอบว่ามีตัวเลือกถัดไปหรือไม่
            if (dialogueUI != null)
            {
                if (currentNode.nexts.Count > 0)
                {
                    dialogueUI.ShowDialogue(currentNode); // แสดง Node ถัดไป
                }
                else
                {
                    // ถ้าไม่มีตัวเลือกถัดไป ถือว่าจบบทสนทนา
                    dialogueUI.ShowDialogue(currentNode);   // แสดงข้อความสุดท้าย
                    dialogueUI.ShowCloseButtonDialog();     // แสดงปุ่มปิด
                }
            }
        }
    }
}