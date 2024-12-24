using UnityEngine;

public class InitPlayerPosition : MonoBehaviour
{
    /// <summary>
    /// 场景加载后，初始化角色位置
    /// </summary>
    private void OnEnable()
    {
        var player = GameObject.FindWithTag("Player");
        if (player is not null)
            player.transform.position = transform.position;
    }
}