// Transform 扩展方法

using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// 在子物体中查找指定标签的第一个物体
    /// </summary>
    public static GameObject FindChildWithTag(this Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }

            GameObject result = FindChildWithTag(child, tag);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取所有带有指定标签的子物体
    /// </summary>
    public static void GetChildrenWithTag(this Transform parent, string tag, List<GameObject> results)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                results.Add(child.gameObject);
            }
            GetChildrenWithTag(child, tag, results);
        }
    }
}