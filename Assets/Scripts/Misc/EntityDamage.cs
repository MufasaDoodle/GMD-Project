using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityDamage : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color normalColor;
    public Color critColor;
    public Color enemyColor;
    public float fadeOutTime = 3f;

    public void Show(int damage, bool isCritical, bool fromEnemy)
    {
        var rect = GetComponent<RectTransform>().rect;
        text.text = $"{damage}";

        if (fromEnemy)
        {
            text.color = enemyColor;
        }
        else if (isCritical)
        {
            text.color = critColor;
        }
        else
        {
            text.color = normalColor;
        }
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(1f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * 2.5f));
            yield return null;
        }

        Destroy(gameObject);
    }
}
