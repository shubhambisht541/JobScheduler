using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace JobScheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobSchedulerController : ControllerBase
    {
        private readonly ISchedulerFactory _schedulerFactory;

        private readonly ILogger<JobSchedulerController> _logger;

        public JobSchedulerController(ILogger<JobSchedulerController> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        [HttpPost("RescheduleJob")]
        public async Task<IActionResult> RescheduleJob([FromQuery] DateTime selectedTime, [FromQuery] string JobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey(JobName);
            var triggerKey = new TriggerKey(JobName);


            if (!await scheduler.CheckExists(jobKey))
            {
                return NotFound("Job does not exist.");
            }

            var currentlyExecutingJobs = await scheduler.GetCurrentlyExecutingJobs();

            if (currentlyExecutingJobs.Any(job => job.JobDetail.Key.Equals(jobKey)))
            {
                return Conflict("Job is currently running and cannot be rescheduled.");
            }

            string cronExpression = $"0 {selectedTime.Minute} {selectedTime.Hour} * * ?";

            var trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithCronSchedule(cronExpression)
                .WithIdentity(triggerKey)
                .Build();

            await scheduler.RescheduleJob(triggerKey, trigger);

            return Ok($"Job scheduled to run daily at {selectedTime.ToShortTimeString()}.");
        }

        [HttpPost("UnscheduleJob")]
        public async Task<IActionResult> UnscheduleJob([FromQuery] string JobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey(JobName);
            var triggerKey = new TriggerKey(JobName);

            if (!await scheduler.CheckExists(jobKey))
            {
                return NotFound("Job does not exist.");
            }

            var trigger = TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithIdentity(triggerKey)
                .Build();

            await scheduler.UnscheduleJob(triggerKey);

            return Ok($"Job unscheduled to run.");
        }

        [HttpPost("StopJob")]
        public async Task<IActionResult> StopJob([FromQuery] string JobName)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobKey = new JobKey(JobName);

            if (!await scheduler.CheckExists(jobKey))
            {
                return NotFound("Job does not exist.");
            }

            await scheduler.Interrupt(jobKey);

            return Ok("Job stopped.");
        }
    }
}
