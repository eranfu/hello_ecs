using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ForEach
{
    public class RotationSpeedProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float degreePerSecond;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new RotationSpeed() {radiansPerSecond = math.radians(degreePerSecond)});
        }
    }
}