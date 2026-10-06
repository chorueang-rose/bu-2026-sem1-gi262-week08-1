using UnityEngine;
using UnityEngine.UI;
using TMPro; // ใช้สำหรับ TextMeshPro

public class SkillNodeUI : MonoBehaviour
{
    // อ้างอิงถึง Component UI
    [Header("UI References")]
    public Button button;
    public Image background;
    public TextMeshProUGUI skillNameText; // หรือ public Text skillNameText;

    // อ้างอิงถึงข้อมูล Skill
    [HideInInspector] public Skill skillData;

    // สถานะสี (กำหนดสีเหล่านี้ใน Inspector)
    [Header("Colors")]
    public Color colorLearned = Color.yellow;
    public Color colorAvailable = Color.green;
    public Color colorLocked = Color.gray;

    public void Initialize(Skill skill)
    {
        this.skillData = skill;
        skillNameText.text = skill.name;

        // ล้าง Listener เก่าออกก่อน เพื่อป้องกันการทำงานซ้ำซ้อนเวลารีเซ็ต UI
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnNodeClicked);

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (skillData == null) return;

        if (skillData.isUnlocked) // ถ้า Skill ถูกเรียนรู้แล้ว (Learned)
        {
            background.color = colorLearned;
            button.interactable = false; // คลิกอีกไม่ได้
        }
        else if (skillData.isAvailable) // ถ้า Skill ปลดล็อคให้เรียนรู้ได้ (Available)
        {
            background.color = colorAvailable;
            button.interactable = true; // คลิกเพื่อเรียนรู้
        }
        else // ถ้า Skill ยังถูกล็อค (Locked)
        {
            background.color = colorLocked;
            button.interactable = false; // คลิกไม่ได้
        }
    }

    private void OnNodeClicked()
    {
        if (skillData != null && skillData.isAvailable && !skillData.isUnlocked)
        {
            // เรียกเมธอด Unlock() ในคลาส Skill
            skillData.Unlock();

            // แจ้งให้ UI ทุกตัวอัปเดตสถานะ
            if (SkillTreeUI.Instance != null)
            {
                SkillTreeUI.Instance.RefreshAllUI();
            }
        }
    }
}
