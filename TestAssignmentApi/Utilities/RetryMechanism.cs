using Polly;
using Polly.Retry;

namespace TestAssignmentApi.Utilities;

/// <summary>
/// This class helps with the retry mechanism for retrying api calls
/// </summary>
public class RetryMechanism
{
    public static ResiliencePipeline InitiateResiliencePipeline()
    {
        return new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions() { MaxRetryAttempts = 7 }) // Add retry using the default options
            .AddTimeout(TimeSpan.FromSeconds(4))
            .Build();
    }
}
