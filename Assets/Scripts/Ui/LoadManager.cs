using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public string sceneName = "Main_Scene";
    public Slider slider; // ���⿡ �����̴� ������Ʈ �Ҵ�
    private AsyncOperation operation;

    void Start()
    {
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.1f);

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(operation.progress, 1f, timer);
                if (slider.value >= operation.progress)
                    timer = 0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, timer);
                if (slider.value >= 0.99f)
                    operation.allowSceneActivation = true;
            }
        }

    }
}
