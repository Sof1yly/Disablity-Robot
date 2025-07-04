using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interacbleMask;

    private readonly Collider[] _colider = new Collider[3];
    [SerializeField] private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colider, _interacbleMask);

        if (_numFound > 0)
        {
            var interacable = _colider[0].GetComponent<IInteractable>();

            if (interacable != null && Input.GetKeyDown(KeyCode.E)) 
            {
                interacable.Interact(this);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
