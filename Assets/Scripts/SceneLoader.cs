using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int targetScene = 1;
    [SerializeField] private float minWaitTime = 1.5f;
    [SerializeField] private float maxWaitTime = 2f;

    void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
    }
}
