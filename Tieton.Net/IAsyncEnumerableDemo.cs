using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tieton.Net
{
    public class IAsyncEnumerableDemo
    {
        public static async IAsyncEnumerable<string> ReadAllLines(string file)
        {
            using (var fs = File.OpenRead(file))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (true)
                    {
                        string line = await sr.ReadLineAsync();

                        await Task.Delay(100);

                        if (line == null)
                        {
                            break;
                        }

                        yield return line;
                    }
                }
            }
        }
    }
}
