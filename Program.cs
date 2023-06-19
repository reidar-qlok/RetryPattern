namespace RetryPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int maxRetries = 3;
            int retryDelayMilliseconds = 1000;

            bool success = RetryOperation(maxRetries, retryDelayMilliseconds, PerformOperation);

            if (success)
            {
                Console.WriteLine("Operation completed successfully.");
            }
            else
            {
                Console.WriteLine("Operation failed after maximum retries.");
            }
        }

        public static bool RetryOperation(int maxRetries, int retryDelayMilliseconds, Action operation)
        {
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    operation(); // Perform the actual operation
                    return true; // Operation succeeded, no need to retry
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine($"Retry attempt {retryCount + 1}/{maxRetries}");

                    // Delay before retrying
                    Thread.Sleep(retryDelayMilliseconds);

                    retryCount++;
                }
            }

            return false; // Operation failed after maximum retries
        }

        public static void PerformOperation()
        {
            // Simulate a potentially failing operation
            Random random = new Random();
            int result = random.Next(0, 5);

            if (result == 0)
            {
                Console.WriteLine("Operation failed.");
                throw new Exception("Operation failed.");
            }
            else
            {
                Console.WriteLine("Operation succeeded.");
            }
        }
    }
}