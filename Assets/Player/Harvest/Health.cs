using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    [SerializeField] private float iFramesLength;
    private bool hasIframes = false;
    IEnumerator iFrametimer;

    [SerializeField] private UnityEvent damaged = new();
    [SerializeField] private UnityEvent healed = new();
    [SerializeField] private UnityEvent died = new();
    [SerializeField] private UnityEvent startedIFrames = new();
    [SerializeField] private UnityEvent stoppedIFrames = new();
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float amt, bool useIFrames = true)
    {
        if(health <= 0) return;
        if (useIFrames && hasIframes) return;

        if (health - amt > 0)
        {
            iFrametimer = IFrameTimer();
            StartCoroutine(iFrametimer);
            health -= amt;
            damaged.Invoke();
        }
        else
        {
            health = 0;
            died.Invoke();
        }
    }

    public void Heal(float amt)
    {
        health += amt;
        if (health > maxHealth) health = maxHealth;
        healed.Invoke();
    }

    IEnumerator IFrameTimer()
    {
        hasIframes = true;
        startedIFrames.Invoke();
        yield return new WaitForSeconds(iFramesLength);
        hasIframes = false;
        stoppedIFrames.Invoke();
    }

    public void StopIFrameTimer()
    {
        StopCoroutine(iFrametimer);
        hasIframes = false;
    }

    #region Getters
    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetIFrames()
    {
        return iFramesLength;
    }

    public bool HasIframes
    {
        get => hasIframes;
    }

    #endregion
}