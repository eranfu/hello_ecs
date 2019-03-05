using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ForEach
{
    public class RotationSpeedSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            ForEach((ref RotationSpeed rotationSpeed, ref Rotation rotation) =>
            {
                float deltaTime = Time.deltaTime;
                rotation.Value = math.mul(rotation.Value,
                    quaternion.AxisAngle(math.up(), deltaTime * rotationSpeed.radiansPerSecond));
            });
        }
    }
}