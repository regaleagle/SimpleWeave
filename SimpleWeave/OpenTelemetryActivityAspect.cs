using System;
using System.Collections.Generic;
using MethodBoundaryAspect.Fody.Attributes;
using System.Diagnostics;
using System.Linq;


namespace SimpleWeave
{
    public class OpenTelemetryActivityAspect : OnMethodBoundaryAspect
    {
        private static ActivitySource source = new ActivitySource("OpenTelemetry.GenericSource", "1.0.0");
        
        private readonly List<(string TagName, string TagValue)> _tags = new List<(string TagName, string TagValue)>();
        public string[] Tags = Array.Empty<string>();

        public OpenTelemetryActivityAspect()
        {
            AttributeTargetMemberAttributes = MulticastAttributes.Public;
            for (int i = 0; i < Tags.Length;)
            {
                if (i + 1 < Tags.Length)
                {
                    _tags.Add((Tags[i], Tags[i+1]));
                }
                i += 2;
            }
        }
        public OpenTelemetryActivityAspect(params string[] tags)
        {
            AttributeTargetMemberAttributes = MulticastAttributes.Public;
            for (int i = 0; i < tags.Length;)
            {
                if (i + 1 < tags.Length)
                {
                    _tags.Add((tags[i], tags[i+1]));
                }
                i += 2;
            }
        }
        
        // public OpenTelemetryActivityAspect(List<string> tags)
        // {}

        public override void OnEntry(MethodExecutionArgs args)
        {
            System.Console.WriteLine("We're here");
            string methodName = args.Method.Name;
            Activity activity = source.StartActivity(methodName);
            foreach (var tag  in _tags)
            {
                activity?.AddTag(tag.TagName, tag.TagValue);
            }
            args.MethodExecutionTag = activity;
            activity?.Start();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Activity activity = args.MethodExecutionTag as Activity;
            activity?.Stop();
        }
    }
}