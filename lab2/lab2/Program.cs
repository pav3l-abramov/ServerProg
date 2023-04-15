using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal class Program
    {
        static Boolean keepGoing = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Start main");
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:11111/");
            listener.Start();


            var listenTask = ProcessAsync(listener);
            while (keepGoing)
            {
                Console.WriteLine("Still keepGoing in main(while)");
                var cmd = Console.ReadLine();
                if (cmd.Equals("q", StringComparison.OrdinalIgnoreCase))
                {
                    keepGoing = false;
                }
            }

            bool y = listenTask.Wait(2000);
            Console.WriteLine(y);
            Console.WriteLine($"Main end {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task ProcessAsync(HttpListener listener)
        {
            Console.WriteLine($"ProcessAsync start {Thread.CurrentThread.ManagedThreadId}");
            while (keepGoing)
            {
                Console.WriteLine($"Still keepGoing in ProcessAsync of(while) {Thread.CurrentThread.ManagedThreadId}");
                HttpListenerContext context = await listener.GetContextAsync();

                if (keepGoing) Perform(context);
                else return;
            }
        }

        static async Task Perform(HttpListenerContext context)
        {
            Console.WriteLine($"Perform - enter in {Thread.CurrentThread.ManagedThreadId}");
            string filePath = context.Request.Url.GetComponents(UriComponents.Path, UriFormat.UriEscaped);
            Console.WriteLine(context.Request.Url);
            Console.WriteLine(context.Request.RemoteEndPoint);
            if (!File.Exists(filePath))
            {
                HttpListenerResponse response = context.Response;
                response.StatusCode = 404;
                Console.WriteLine(response.StatusCode);
                response.OutputStream.Close();
            }
            else
            {
                HttpListenerResponse response = context.Response;
                System.IO.Stream input = File.OpenRead(filePath);
                byte[] buffer = new byte[1024];
                Console.WriteLine($"Right before await readsync in {Thread.CurrentThread.ManagedThreadId}");
                int bytesRead = await input.ReadAsync(buffer, 0, buffer.Length);
                Console.WriteLine($"Right after await readsync in {Thread.CurrentThread.ManagedThreadId}");
                while (bytesRead > 0)
                {
                    Console.WriteLine($"Right before await WriteAsync(while) in {Thread.CurrentThread.ManagedThreadId}");
                    await response.OutputStream.WriteAsync(buffer, 0, bytesRead);
                    Console.WriteLine($"Right after await WriteAsync(while) in {Thread.CurrentThread.ManagedThreadId}");

                    Console.WriteLine($"Right before await readasync(while) in {Thread.CurrentThread.ManagedThreadId}");
                    bytesRead = await input.ReadAsync(buffer, 0, buffer.Length);
                    Console.WriteLine($"Right after await readasync(while) in {Thread.CurrentThread.ManagedThreadId}");
                }
                response.StatusCode = 200;
                Console.WriteLine(response.StatusCode);
                input.Close();
                response.OutputStream.Close();
                Console.WriteLine($"Done for {Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }
}
