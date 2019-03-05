using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace IJobChunk
{
    public class RotationSpeedSystem : JobComponentSystem
    {
        private ComponentGroup _group;

        protected override void OnCreateManager()
        {
            _group = GetComponentGroup(typeof(Rotation), ComponentType.ReadOnly<RotationSpeed>());
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            ArchetypeChunkComponentType<Rotation> rotationType = GetArchetypeChunkComponentType<Rotation>();
            ArchetypeChunkComponentType<RotationSpeed> rotationSpeedType =
                GetArchetypeChunkComponentType<RotationSpeed>(true);
            var job = new RotationSpeedJob
            {
                DeltaTime = Time.deltaTime,
                RotationType = rotationType,
                RotationSpeedType = rotationSpeedType
            };
            return job.Schedule(_group, inputDeps);
        }

        [BurstCompile]
        private struct RotationSpeedJob : Unity.Entities.IJobChunk
        {
            public float DeltaTime;
            public ArchetypeChunkComponentType<Rotation> RotationType;
            [ReadOnly] public ArchetypeChunkComponentType<RotationSpeed> RotationSpeedType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                NativeArray<Rotation> chunkRotations = chunk.GetNativeArray(RotationType);
                NativeArray<RotationSpeed> chunkRotationSpeeds = chunk.GetNativeArray(RotationSpeedType);
                for (var i = 0; i < chunk.Count; i++)
                {
                    Rotation rotation = chunkRotations[i];
                    RotationSpeed rotationSpeed = chunkRotationSpeeds[i];
                    chunkRotations[i] = new Rotation
                    {
                        Value = math.mul(rotation.Value,
                            quaternion.AxisAngle(math.up(), DeltaTime * rotationSpeed.radiansPerSecond))
                    };
                }
            }
        }
    }
}