// See https://aka.ms/new-console-template for more information

//using MethodBoundaryAspect.Fody.Attributes;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SimpleWeaveIntermediaryLib;

//using SimpleWeave;


class Program
{

    static async Task Main(string[] args)
    {
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SimpleWeave"))
            .AddSource("OpenTelemetry.GenericSource")
            .AddConsoleExporter()
            .Build();
        var guy = new IntermediaryTestMethods();
        await guy.AsyncMethod();
        guy.SyncMethod();
        Console.ReadKey();
    }
}


//[OpenTelemetryActivityAspect("jama", "kmakma", "kmakam", AttributeTargetMemberAttributes = MulticastAttributes.Public)]
public class TestMethods
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