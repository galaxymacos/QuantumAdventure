using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class FloatingDamageActivator: MonoBehaviourPun
{
    private HealthComponent _healthComponent;
    [SerializeField] private Transform damagePopupDisplayTransform;
    
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.onTakeDamage += ShowFloatingText;
    }

    private void ShowFloatingText(float damage)
    {
        if (photonView.IsMine)
        {
            if (damage < 1)
            {
                Debug.LogWarning($"{damage} is lower than 1, ignoring");
                return;
            }
            DamagePopup.Create(damagePopupDisplayTransform.position, (int)damage);
        }
        
    }

    private void OnDestroy()
    {
        _healthComponent.onTakeDamage -= ShowFloatingText;
    }
}