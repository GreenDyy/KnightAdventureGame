using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static public EffectManager instance;
    public List<GameObject> effects;
    private void Awake()
    {
        instance = this;
        LoadEffects();
    }

    protected virtual void LoadEffects()
    {
        effects = new List<GameObject>();
        foreach (Transform child in transform)
        {
            effects.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    public virtual void SpawnVFX(string effectName, Vector3 pos, Quaternion rotation)
    {
        GameObject effect = GetOjbFromName(effectName);
        GameObject newEffect = Instantiate(effect, pos, rotation);
        newEffect.SetActive(true);
    }

    public void DestroyEffect(GameObject effect)
    {
        Destroy(effect);
    }

    protected virtual GameObject GetOjbFromName(string effectName)
    {
        foreach (GameObject child in effects)
        {
            if (child.name == effectName)
                return child;
        }
        return null;
    }
}
