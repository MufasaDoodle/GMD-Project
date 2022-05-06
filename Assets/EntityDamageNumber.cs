using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageNumber : MonoBehaviour
{
    public GameObject prefab;

    public void InstantiateEntityDamage(int damageAmount, bool isCritical, bool fromEnemy, Vector3 position)
    {
        /*
        var newPos = Camera.main.WorldToViewportPoint(position);
        newPos.z = 0f;
        */

        var go = Instantiate(prefab, position, transform.rotation);
        go.GetComponent<RectTransform>().SetParent(this.transform, false);
        go.GetComponent<EntityDamage>().Show(damageAmount, isCritical, fromEnemy);
    }
}
