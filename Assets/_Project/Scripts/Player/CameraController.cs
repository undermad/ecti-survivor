using KBCore.Refs;
using UnityEngine;

namespace Explorer._Project.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Anywhere] private Transform target;
    
        [Header("Settings")]
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
        [SerializeField] private float smoothSpeed = 0.125f;



        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        
            transform.LookAt(target);
        }
    }
}
