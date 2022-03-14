using UnityEngine;
public static class Transforms{
    public static void DestroyChildren(this Transform T, bool destroyImmediately = false){
        foreach(Transform child in T){
            if (destroyImmediately){
                MonoBehaviour.DestroyImmediate(child.gameObject);
            } else {
                MonoBehaviour.Destroy(child.gameObject);
            }
        }
    }
}