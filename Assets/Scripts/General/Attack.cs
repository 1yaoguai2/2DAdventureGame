using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
   //伤害
   public int damage;
   //范围
   public float attackRange;
   //普攻倍率
   public float attackRate;


   private void OnTriggerStay2D(Collider2D other)
   {
      other.GetComponent<Character>()?.TakeDamage(this);
   }
}
