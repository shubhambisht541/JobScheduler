using Quartz;

namespace JobScheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class FileProcessingJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("Job execution started at: " + DateTime.Now);
                await Task.Delay(TimeSpan.FromMinutes(5));
                Console.WriteLine("Job execution completed at: " + DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
