using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���׸� �̱��� ��ũ��Ʈ
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }
            if (instance)
            {
                return instance;
            }

            return null;
        }
    }
}