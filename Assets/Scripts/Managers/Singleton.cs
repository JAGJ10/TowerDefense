﻿using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    //Returns the instance of this singleton.
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null) {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }

            return instance;
        }
    }

    public virtual void Awake() {
        if (instance == null) {
            instance = this as T;
        }

        if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}