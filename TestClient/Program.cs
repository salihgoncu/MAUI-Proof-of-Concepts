using Grpc.Net.Client;

namespace TestClient
{
    internal class Program
    {
        private static GrpcChannel _channel = GrpcChannel.ForAddress("https://localhost:6000");

        static void Main(string[] args)
        {
            Console.WriteLine("Please press any key to call...");
            Console.ReadKey();
            var client = new WebApplication3.Greeter.GreeterClient(_channel);

            var response = client.SayHello(new WebApplication3.HelloRequest() { Name = "hello" });
            Console.WriteLine(response.Message); 
        }
    }
}