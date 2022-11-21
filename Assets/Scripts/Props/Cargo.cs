using UnityEngine;
using DLIFR.Physics;

namespace DLIFR.Props
{
    public class Cargo : Grabbable, IWeightBody
    {
        public const float VOID_HEIGHT = -15;

        public float fuelValue = 0;
        public int sellValue = 0;

        private Rigidbody _rigidbody;

        private void OnEnable() 
        {
            _rigidbody = GetComponent<Rigidbody>();

            WeightSimulator.AddBody(this);
        }

        private void OnDisable() 
        {
            WeightSimulator.RemoveBody(this);
        }

        public float GetWeight()
        {
            return 1;
        }

        public Vector3 GetPosition()
        {
            return transform.localPosition;
        }

        private void FixedUpdate() 
        {
            if(transform.position.y <= VOID_HEIGHT)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other) 
        {   
            Area area = other.GetComponentInParent<Area>();

            if(area != null)
            {
                area.cargoes.Add(this);
                area.onChange?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other) 
        {      
            Area area = other.GetComponentInParent<Area>();

            if(area != null)
            {
                area.cargoes.Remove(this);
                area.onChange?.Invoke();
            }
        }
    }
}