using UnityEditor;
using UnityEngine;


namespace BuilderScenario
{
    public class BuildTests
    {
        [MenuItem("Build/Test/Test 1")]
        public static void Test1()
        {
            var container = new SimpleIoC();
            var executer = new JobExecuter(container);

            container.Register<IJobExecuteService, JobExecuter>(executer);
            container.Register<ILogger, Logger>(new Logger(Debug.unityLogger));

            var jobData = 
@"
Jobs:
  - !dummy
  - !debugLog
    Message: 'Hello from job!'
  - !SetVersion
    Version: '1.5.0'
";

            var rootJob = new JobLoader().Deserialize<RootJob>(jobData);
            var result = executer.Execute(rootJob);

            Debug.Log($"IsSuccess: {result.IsSucces}");
            foreach (var log in result.Logs)
            {
                Debug.Log($"Message: {log}");
            }
        }
    }
}