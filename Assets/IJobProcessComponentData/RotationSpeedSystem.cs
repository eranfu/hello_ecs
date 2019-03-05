using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace IJobProcessComponentData
{
    public class RotationSpeedSystem : JobComponentSystem
    {
        [BurstCompile]
        private struct RotationSpeedJob : IJobProcessComponentData<Rotation, RotationSpeed>
        {
            public float DeltaTime;

            public void Execute(ref Rotation rotation, [ReadOnly] ref RotationSpeed rotationSpeed)
            {
                rotation.Value = math.mul(rotation.Value,
                    quaternion.AxisAngle(math.up(), DeltaTime * rotationSpeed.radiansPerSecond));
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new RotationSpeedJob
            {
                DeltaTime = Time.deltaTime
            };
            return job.Schedule(this, inputDeps);
        }
    }
}