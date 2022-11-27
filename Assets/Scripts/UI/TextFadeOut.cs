using System.Collections;
using TMPro;
using UnityEngine;

public class TextFadeOut : MonoBehaviour
{
    [SerializeField] private float FadingSpeed = .2f;
    TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        StartCoroutine(fadeOut());
    }

    private IEnumerator fadeOut()
    {
        Color c = tmp.color;
        while (c.a >= 0)
        {
            c.a -= FadingSpeed * Time.deltaTime;
            tmp.color = c;
            yield return null;
        }
    }
}
