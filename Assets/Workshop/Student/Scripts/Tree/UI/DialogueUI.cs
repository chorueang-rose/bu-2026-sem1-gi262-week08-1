using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcText;
    public Transform choiceContainer;
    public Button choiceButtonPrefab; // �ҡ Prefab ����������͡�����
    public GameObject closeButtonDialogue;
    private DialogueSequen InteractNpcSequen;

    // �纻������١���ҧ��� ���͹�价����/��͹������ѧ
    private List<Button> activeButtons = new List<Button>();

    public void Setup(DialogueSequen sequen)
    {
        //1. Set Dialogue Sequen
        InteractNpcSequen = sequen;
        DialogueNode currentNode = InteractNpcSequen.tree.root;
        ShowDialogue(currentNode);
        InteractNpcSequen = sequen;
        DialogueNode currentNode = InteractNpcSequen.tree.root;
        sequen.currentNode = currentNode;
        ShowDialogue(currentNode);
        //Show UI
        dialoguePanel.SetActive(true);
        gameObject.SetActive(true);
        closeButtonDialogue.SetActive(false);
    }

    public void ShowDialogue(DialogueNode node)
    {
        // 2. set ����� �˹��Ѩ�غѹ
        InteractNpcSequen.currentNode = node;

        // 3. �ʴ���ͤ����ͧ NPC
        npcText.text = node.text;
        // 4. ��ҧ����������͡���
        ClearChoices();

        // 5. ���ҧ����������͡������ nexts
        var choices = new List<string>(node.nexts.Keys);
        for (int i = 0; i < choices.Count; i++)
        {
            string choiceText = choices[i];
            CreateChoiceButton(choiceText, i);
        {
            string choiceText = choices[i];
            CreateChoiceButton(choiceText, i);
        }
    }

    private void CreateChoiceButton(string text, int index)
    {
        Button newButton = Instantiate(choiceButtonPrefab, choiceContainer);

        // ��駤�Ң�ͤ���������
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // ���� Listener ����͡�����
        // �� Lambda Expression ������ index ��Ѻ���� DialogueManager
        newButton.onClick.AddListener(() => OnChoiceSelected(index));

        activeButtons.Add(newButton);
    }

    private void ClearChoices()
    {
        foreach (Button button in activeButtons)
        {
            Destroy(button.gameObject);
        }
        activeButtons.Clear();
    }

    private void OnChoiceSelected(int index)
    {
        // �� index �ͧ������͡�����������͡��Ѻ���� DialogueManager �Ѵ���
        InteractNpcSequen.SelectChoice(index);
    }

    public void ShowCloseButtonDialog()
    {
        closeButtonDialogue.gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        ClearChoices();
    }
}