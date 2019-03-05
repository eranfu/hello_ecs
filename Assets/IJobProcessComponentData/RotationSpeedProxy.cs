using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace IJobProcessComponentData
{
    public class RotationSpeedProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float degreesPerSecond = 180;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new RotationSpeed {radiansPerSecond = math.radians(degreesPerSecond)});
        }
    }
}