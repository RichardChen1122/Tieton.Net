using System.Data.Common;

namespace Tieton.Net
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

                       
            Console.ReadKey();
        }


        public static async Task WouldNotTimeout()
        {
            var lines = IAsyncEnumerableDemo.ReadAllLines(@"D:\feature.json");

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(1));

            await foreach (var line in lines.WithCancellation(cts.Token))
            {
                Console.WriteLine(line);
            }
        }


        public static async Task WouldTimeoutInTheMiddelOfIteration()
        {
            var lines = IAsyncEnumerableDemo.ReadAllLines(@"D:\feature.json");

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(1));

            var t = lines.SelectAwaitWithCancellation(async (s, c) =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(5), c);
                //await Task.Delay(TimeSpan.FromMilliseconds(100), c);
                return ValueTask.FromResult(s);
            });

            await foreach (var line in t.WithCancellation(cts.Token))
            {
                Console.WriteLine(line);
            }
        }
    }
}