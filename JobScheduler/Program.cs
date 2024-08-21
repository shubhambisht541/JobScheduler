using JobScheduler.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{

    var cronExpression = builder.Configuration["Quartz:FileProcessingJob:CronExpression"] ?? string.Empty;

    q.AddJob<FileProcessingJob>(job => job.WithIdentity(nameof(FileProcessingJob)));

    q.AddTrigger(trigger => trigger
        .ForJob(nameof(FileProcessingJob))
        .WithIdentity(nameof(FileProcessingJob))
        .WithCronSchedule(cronExpression)
        //.WithSimpleSchedule(
        //        schedule => schedule.WithIntervalInSeconds(10).RepeatForever())
        );
});

builder.Services.AddQuartzHostedService(service => service.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
