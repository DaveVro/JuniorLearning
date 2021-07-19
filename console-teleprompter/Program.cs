using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TeleprompterConsole
{
    class Program
    {
        /// <summary>
        /// Entry point to the application, reads out text from a sample file with the option to increase or decrease the read speed
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var lines = ReadFrom("sampleQuotes.txt");

            foreach (var line in lines)
            {
                Console.WriteLine(line);
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var pause = Task.Delay(200);
                    // Synchronously waiting on a task is an anti-pattern. This will get fixed in later steps
                    pause.Wait();
                }
            }

            RunTeleprompter().Wait();
        }

        /// <summary>
        /// Task to initialize the ShowTeleprompter and GetInput methods, this reads out the sample file and adjusts the scroll speed
        /// </summary>
        /// <returns></returns>
        private static async Task RunTeleprompter()
        {
            var config = new TelePrompterConfig();
            var displayTask = ShowTeleprompter(config);

            var speedTask = GetInput(config);
            await Task.WhenAny(displayTask, speedTask);
        }

        /// <summary>
        /// Passes the file name of the text doc to be read to the ReadFrom method and writes the results to a console output
        /// </summary>
        /// <param name="config">Teleprompter config to control the speed of the file being read as well as task completion</param>
        /// <returns></returns>
        private static async Task ShowTeleprompter(TelePrompterConfig config)
        {
            var words = ReadFrom("sampleQuotes.txt");
            foreach (var word in words)
            {
                Console.WriteLine(word);
                if (!string.IsNullOrWhiteSpace(word))
                {
                    await Task.Delay(config.DelayInMilliseconds);
                }
            }

            config.SetDone();
        }

        /// <summary>
        /// Reads the text in the supplied file
        /// </summary>
        /// <param name="file">File name of the text doc to be read</param>
        /// <returns></returns>
        static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var words = line.Split(' ');
                    var lineLength = 0;
                    foreach (var word in words)
                    {
                        yield return word + " ";
                        lineLength += word.Length + 1;
                        if (lineLength > 70)
                        {
                            yield return Environment.NewLine;
                            lineLength = 0;
                        }
                    }
                    yield return Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// Reads keyboard inputs to control the speed of text playback and early completion
        /// </summary>
        /// <param name="config">Teleprompter config to control the speed of the file being read as well as task completion</param>
        /// <returns></returns>
        private static async Task GetInput(TelePrompterConfig config)
        {
            Action work = () =>
            {
                do
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == '>')
                    {
                        config.UpdateDelay(-10);
                    }
                    else if (key.KeyChar == '<')
                    {
                        config.UpdateDelay(10);
                    }
                    else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                    {
                        config.SetDone();
                    }
                } while (!config.Done);
            };
            await Task.Run(work);
        }
    }
}
