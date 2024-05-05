namespace BlazoriseApp.Models.DTO;

public readonly record struct FailureReportDTO
{
    public string FailureType { get; init; }
    public string Location { get; init; }
    public int InteractionTargetId { get; init; }
    public string ActionOnTarget { get; init; }
    public DateTime ReceiveTime { get; init; }

    public static FailureReportDTO Parse(string data)
    {
        var report = new FailureReportDTO();
        var parameters = data.Split('&');
        foreach (var parameter in parameters)
        {
            var keyValue = parameter.Split('=');
            if (keyValue.Length == 2)
            {
                var key = keyValue[0];
                var value = keyValue[1];

                switch (key)
                {
                    case "FailureType":
                        report = report with { FailureType = value };
                        break;
                    case "Location":
                        report = report with { Location = value };
                        break;
                    case "InteractionTargetId":
                        if (int.TryParse(value, out var interactionTargetId))
                            report = report with { InteractionTargetId = interactionTargetId };
                        break;
                    case "ActionOnTarget":
                        report = report with { ActionOnTarget = value };
                        break;
                    default:
                        Console.WriteLine($"Key: {key} is not recognized.");
                        break;
                }
            }
        }

        report = report with { ReceiveTime = DateTime.Now };
        return report;
    }

    public override string ToString()
        => $"Failure Report:\n" +
           $"  Failure Type: {FailureType}\n" +
           $"  Location: {Location}\n" +
           $"  Interaction Target Id: {InteractionTargetId}\n" +
           $"  Action on Target: \"{ActionOnTarget}\"\n" +
           $"  Receive Time: {ReceiveTime}";
}