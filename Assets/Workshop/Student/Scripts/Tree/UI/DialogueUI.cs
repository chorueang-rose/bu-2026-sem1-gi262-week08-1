using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcText;
    public Transform choiceContainer;
    public Button choiceButtonPrefab;
    public GameObject closeButtonDialogue;
    private DialogueSequen InteractNpcSequen;

    private List<Button> activeButtons = new List<Button>();

    public void Setup(DialogueSequen sequen)
    {
        // 1. Set Dialogue Sequen
        InteractNpcSequen = sequen;
        DialogueNode currentNode = InteractNpcSequen.tree.root;
        sequen.currentNode = currentNode;

        ShowDialogue(currentNode);

        // Show UI
        dialoguePanel.SetActive(true);
        gameObject.SetActive(true);
        closeButtonDialogue.SetActive(false);
    }

    public void ShowDialogue(DialogueNode node)
    {
        InteractNpcSequen.currentNode = node;

        npcText.text = node.text;

        ClearChoices();

        var choices = new List<string>(node.nexts.Keys);
        for (int i = 0; i < choices.Count; i++)
        {
            string choiceText = choices[i];
            CreateChoiceButton(choiceText, i);
        }
    }

    private void CreateChoiceButton(string text, int index)
    {
        Button btn = Instantiate(choiceButtonPrefab, choiceContainer);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = text;
        btn.onClick.AddListener(() => OnChoiceSelected(index));
        activeButtons.Add(btn);
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
        InteractNpcSequen.SelectChoice(index);
    }

    public void ShowCloseButtonDialog()
    {
        closeButtonDialogue.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        ClearChoices();
    }
}