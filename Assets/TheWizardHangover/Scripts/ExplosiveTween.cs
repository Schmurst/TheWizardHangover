using System;
using System.Collections;
using System.Collections.Generic;
using SimpleTween;
using UnityEngine;

public class ExplosiveTween : MonoBehaviour, SimpleTween.ITweenEvent
{

    [SerializeField]
    private float m_Radius;
    [SerializeField]
    private float m_Intensity;
    [SerializeField]
    LayerMask activeLayer;
    [SerializeField]
    bool m_destroy = false;
    /// <summary>
    /// To let other animations to finish
    /// </summary>
    [SerializeField]
    float m_destroyTimeout = 0.0f;

    private bool isExecuting = false;

    IEnumerator ExplodeItem()
    {
        isExecuting = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_Radius, activeLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Rigidbody rb = hitColliders[i].gameObject.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
            {
                //Get direction for the force
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                //Get the distance lenght
                float distance = (rb.transform.position - transform.position).sqrMagnitude;
                direction *= m_Intensity/distance;
                //Rotate randomly to give some randomness
                direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-10.0f, 10.0f), Vector3.up) * direction;

                rb.AddForce(direction, ForceMode.Impulse);
            }
        }

        if (m_destroy)
        {
            Destroy(this.gameObject, m_destroyTimeout);
            yield return new WaitForSeconds(m_destroyTimeout);
        }
            

        yield return null;
        isExecuting = false;
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    void ITweenEvent.Play()
    {
        StartCoroutine(ExplodeItem());
    }
}
