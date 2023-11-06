using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade_Script : MonoBehaviour
{
    public Image image;
    public Image image2;

    public GameObject im_0;
    public GameObject im_1;
    public GameObject im_2;
    public GameObject im_3;
    public GameObject im_4;
    public GameObject im_5;

    public Text _text;

    public int i = 0;
    public bool NEXT = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIN());
        StartCoroutine(textboxon());
        if (i == 0)
        {
            im_0.SetActive(true);
            _text.text = "������ �������� ������ �ø������� �ŵ���";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NEXT == true)
        {
            StartCoroutine(FadeOff());
        }

    }

    IEnumerator FadeIN()
    {
        float fadeCount = 1f;
        while (fadeCount <= 1.0f)
        {
            fadeCount -= 0.025f;
            yield return new WaitForSeconds(0.001f);
            image.color = new Color(0, 0, 0, fadeCount);
        }

    }

    IEnumerator FadeOff()
    {
        float fadeCount = 0f;
        while (fadeCount <= 1f)
        {
            fadeCount += 0.025f;
            yield return new WaitForSeconds(0.001f);
            image2.color = new Color(0, 0, 0, fadeCount);
        }

        if (fadeCount > 0.8f)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {

        }

    }

    IEnumerator textboxon()
    {
        yield return new WaitForSeconds(2f);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void next()
    {
        ++i;

        if (i == 1)
        {
            im_0.SetActive(false);
            im_1.SetActive(true);
            _text.text = "�������� �޷����� ���踦 â���ϴµ�";
        }
        else if (i == 2)
        {
            im_1.SetActive(false);
            im_2.SetActive(true);
            _text.text = "�װ����� ���밡 �Ǿ� �ο��� �̸� ��ü�ڶ�� �ҷ�����";
        }
        else if (i == 3)
        {
            im_2.SetActive(false);
            im_3.SetActive(true);
            _text.text = "��ü�ڴ� �ŵ鿡�Լ� �ɷ��� �ο��޾� ���� �ο���";
        }
        else if (i == 4)
        {
            im_1.SetActive(false);
            im_2.SetActive(false);
            im_3.SetActive(false);

            im_4.SetActive(true);
            _text.text = "�ŵ��� �װ��� ���� �������� �޷���";
        }
        else if (i == 5)
        {
            im_4.SetActive(false);
            im_5.SetActive(true);
            _text.text = "Ȥ�ڴ� �װ��� �ŵ��� ������ �ҷ���";
        }
        else if (i == 6)
        {
            NEXT = true;
        }
    }
}

