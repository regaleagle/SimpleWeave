using System;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody.Attributes;
using SimpleWeave;

namespace SimpleWeaveIntermediaryLib
{
    [OpenTelemetryActivityAspect("jama", "kmakma", "kmakam", AttributeTargetMemberAttributes = MulticastAttributes.Public)]
//[OpenTelemetryActivityAspect(Tags = new []{"jama", "kmakma", "kmakam"}, AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    public class IntermediaryTestMethods
    {
        public async Task AsyncMethod()
        {
            await Task.Delay(1000);
            Console.WriteLine("Async method completed.");
        }

        public void SyncMethod()
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Sync method completed.");
        }}
}