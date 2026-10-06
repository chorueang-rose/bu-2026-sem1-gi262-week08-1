using UnityEngine;

namespace Solution
{
    public class Character : Identity
    {
        [Header("Character")]
        public int energy;
        public int maxEnergy;
        public int AttackPoint;

        protected bool isAlive;
        protected bool isFreeze;

        public SpriteRenderer spriteRenderer; // ลาก SpriteRenderer ของตัวละครมาใส่ใน Inspector

        // สีที่เราจะใช้ 3 ระดับ
        [Header("color energy")]
        public Color normalColor = Color.white;    // สีปกติ
        public Color damagedColor1 = Color.yellow; // ได้รับความเสียหายระดับ 1 (เช่น HP เหลือ 66%)
        public Color damagedColor2 = Color.red;    // ได้รับความเสียหายระดับ 2 (เช่น HP เหลือ 33%)

        public override void SetUP()
        {
            isAlive = true;
            isFreeze = false;
            Debug.Log("SetUP " + Name);
            spriteRenderer = GetComponent<SpriteRenderer>();
            energy = maxEnergy;

            UpdateSpriteColorBasedOnHealth(); // เริ่มต้นด้วยการตั้งค่าสีตามพลังชีวิตปัจจุบัน
        }

        protected void GetRemainEnergy()
        {
            Debug.Log(name + " : " + energy);
        }

        public virtual void Move(Vector2 direction)
        {
            if (isFreeze)
            {
                isFreeze = false;
                UpdateSpriteColorBasedOnHealth(); // คืนค่าสีตาม HP เมื่อหายจาก Freeze
                return;
            }

            int toX = (int)(positionX + direction.x);
            int toY = (int)(positionY + direction.y);

            if (HasPlacement(toX, toY))
            {
                bool isCanWalkTo = mapGenerator.GetMapData(toX, toY).Hit();
                if (isCanWalkTo)
                {
                    UpdatePosition(toX, toY);
                }
            }
            else
            {
                UpdatePosition(toX, toY);
                TakeDamage(1);
            }
        }

        public virtual void UpdatePosition(int toX, int toY)
        {
            mapGenerator.mapdata[positionX, positionY] = null;
            positionX = toX;
            positionY = toY;
            transform.position = new Vector3(positionX, positionY, 0);
            mapGenerator.mapdata[positionX, positionY] = this;
        }

        // hasPlacement คืนค่า true ถ้ามีการวางอะไรไว้บน map ที่ตำแหน่ง x,y
        public bool HasPlacement(int x, int y)
        {
            var mapData = mapGenerator.GetMapData(x, y);
            return mapData != null;
        }

        public virtual void TakeDamage(int Damage)
        {
            energy -= Damage;
            Debug.Log(name + " Current Energy : " + energy);
            UpdateSpriteColorBasedOnHealth();
            CheckDead();
        }

        public virtual void TakeDamage(int Damage, bool freeze)
        {
            energy -= Damage;
            isFreeze = freeze;
            Debug.Log(name + " Current Energy : " + energy);

            if (isFreeze && spriteRenderer != null)
            {
                spriteRenderer.color = Color.blue; // แสดงสีฟ้าเมื่อแช่แข็ง
                Debug.Log("you are Freeze");
            }
            else
            {
                UpdateSpriteColorBasedOnHealth();
            }

            CheckDead();
        }

        public void Heal(int healPoint)
        {
            Heal(healPoint, false);
        }

        public void Heal(int healPoint, bool Bonuse)
        {
            energy += healPoint * (Bonuse ? 2 : 1);
            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            Debug.Log("Current Energy : " + energy);
            UpdateSpriteColorBasedOnHealth(); // อัปเดตสี Sprite ใหม่เมื่อได้รับ Heal
        }

        protected virtual void CheckDead()
        {
            if (energy <= 0)
            {
                mapGenerator.mapdata[positionX, positionY] = null;
                Destroy(gameObject);
            }
        }

        private void UpdateSpriteColorBasedOnHealth()
        {
            if (spriteRenderer == null || isFreeze) return; // หาก Freeze อยู่จะไม่เปลี่ยนสีตาม HP

            float healthPercentage = (float)energy / maxEnergy;

            if (healthPercentage > 0.66f) // มากกว่า 66%
            {
                spriteRenderer.color = normalColor;
            }
            else if (healthPercentage > 0.33f) // มากกว่า 33% แต่ไม่เกิน 66%
            {
                spriteRenderer.color = damagedColor1;
            }
            else // 33% หรือน้อยกว่า
            {
                spriteRenderer.color = damagedColor2;
            }
            Debug.Log(name + " Health Percentage: " + (healthPercentage * 100) + "%");
        }
    }
}