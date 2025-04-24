using Augments.Reflect;
using Common;
using Xunit;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace ShapeShifterDoc;

public class DocToFile
{
    [Fact]
    public void Create()
    {
        var typeattributes =
            FromAssembly.Containing<ShapeShifter.ShapeShifter>()
                .WhereHasAttribute<LegacyDocAttribute>()
                .SelectMany(t => t.GetCustomAttributes(typeof(LegacyDocAttribute), false));

        var methodattributes =
            FromAssembly.Containing<ShapeShifter.ShapeShifter>()
                .MethodsWithAttribute<LegacyDocAttribute>()
                .SelectMany(t => t.GetCustomAttributes(typeof(LegacyDocAttribute), false));

        var additionalAttributes =
            new List<LegacyDocAttribute>
                { new() { Order = "1.2", Caption = "Modifiers" }
                , new() { Order = "1.3", Caption = "Challenges", Content = "These can be done by any agent, needs a Shard to be present." }
                , new() { Order = "1.4", Caption = "Unique Trait" } };

        var attributes =
            typeattributes
                .Union(methodattributes)
                .Union(additionalAttributes)
                .Cast<LegacyDocAttribute>()
                .OrderBy(a => a.Order);


        var sb = new StringBuilder();
        foreach (var attr in attributes)
        {
            if (!string.IsNullOrWhiteSpace(attr.Caption))
            {
                var headingLevel = attr.Order.Split(".").Length;
                var headingMarker = new string('#', headingLevel);
                sb.Append($"{headingMarker} {attr.Caption}");
                sb.AppendLine();
                sb.AppendLine();
            }
            if (!string.IsNullOrWhiteSpace(attr.Content))
            {
                sb.AppendLine(attr.Content);
                sb.AppendLine();
            }

            using (var writer = new StreamWriter("../../../../Doc/ShapeShifter.md", false))
                writer.Write(sb.ToString());
        }
    }
}