using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace SpawnFromEntity
{
    public class SpawnerSystem : JobComponentSystem
    {
        private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreateManager()
        {
            base.OnCreateManager();
            _commandBufferSystem = World.GetOrCreateManager<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            JobHandle job = new SpawnJob
            {
                commandBuffer = _commandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDeps);
            _commandBufferSystem.AddJobHandleForProducer(job);
            return job;
        }

        [BurstCompile]
        private struct SpawnJob : IJobProcessComponentDataWithEntity<Spawner, LocalToWorld>
        {
            public EntityCommandBuffer commandBuffer;

            public void Execute(Entity entity, int index,
                [ReadOnly] ref Spawner spawner, [ReadOnly] ref LocalToWorld localToWorld)
            {
                for (var y = 0; y < spawner.countY; y++)
                {
                    for (var x = 0; x < spawner.countX; x++)
                    {
                        Entity instance = commandBuffer.Instantiate(spawner.prefab);
                        float3 position = math.transform(localToWorld.Value,
                            new float3(x * 1.3f, noise.cnoise(new float2(x, y) * 0.21f) * 2, y * 1.3f));
                        commandBuffer.SetComponent(instance, new Translation {Value = position});
                    }
                }

                commandBuffer.DestroyEntity(entity);
            }
        }
    }
}