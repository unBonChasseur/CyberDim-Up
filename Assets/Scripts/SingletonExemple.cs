using UnityEngine;

/// <summary>
/// A singleton inheriting from MonoBehaviour.
/// </summary>
/// <typeparam name="T">Type of the class inheriting from MonoBehaviourSingleton.</typeparam>
public class Singleton : MonoBehaviour
{
    /// <summary>
    /// Instance of the singleton.
    /// </summary>
    private static Singleton m_Instance;

    /// <summary>
    /// Public access to the instance.
    /// </summary>
    public static Singleton Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<Singleton>();
            if (m_Instance == null)
                Debug.LogError("Singleton<" + typeof(Singleton) + "> instance has been not found.");
            return m_Instance;
        }
    }

    /// <summary>
    /// Awake is called by Unity for initialization.
    /// </summary>
    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as Singleton;
        }
        else if (m_Instance != this)
            DestroySelf();
    }

    /// <summary>
    /// OnValidate is called by Unity when a changes is done with this script within the Unity Inspector.
    /// </summary>
    protected void OnValidate()
    {
        if (m_Instance == null)
            m_Instance = this as Singleton;
        else if (m_Instance != this)
        {
            Debug.LogError("Singleton<" + this.GetType() + "> already has an instance on scene. Component will be destroyed.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall -= DestroySelf;
            UnityEditor.EditorApplication.delayCall += DestroySelf;
#endif
        }
    }

    /// <summary>
    /// Destroys the instance.
    /// </summary>
    private void DestroySelf()
    {
        if (Application.isPlaying)
            Destroy(this);
        else
            DestroyImmediate(this);
    }
}