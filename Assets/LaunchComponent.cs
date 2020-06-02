using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LaunchComponent : MonoBehaviour
{
    private Vector3 _impact;
    private CharacterController _characterController;
    [SerializeField] private float mass = 3.0f;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }


    public void Launch(Vector3 direction, float force)
    {
        direction.Normalize();
        if (direction.y < 0)
        {
            direction.y = -direction.y;
        }

        _impact += direction * force / mass;
    }
    
    
 
    private void Update(){
        // apply the impact force:
        if (_impact.magnitude > 0.2)
        {
            _characterController.Move(_impact * Time.deltaTime);
        }
        // consumes the impact energy each cycle:
        _impact = Vector3.Lerp(_impact, Vector3.zero, 5*Time.deltaTime);
    }


}