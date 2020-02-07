using System;
using global::System.Web.Http.SelfHost;
using global::System.Web.Http;
using global::Newtonsoft.Json.Serialization;

namespace SelfHostedWebApi
{
    static class Module1
    {
        public static void Main()
        {
            var config = new HttpSelfHostConfiguration("http://127.0.0.1:2002");

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            // hash out the formatters below, if you want it returned as XML or whatever the request format was made by
            // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New System.Net.Http.Headers.MediaTypeHeaderValue("text/html"))
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            using (var server = new HttpSelfHostServer(config))
            {
                try
                {
                    server.OpenAsync().Wait();
                    Console.WriteLine("Listening at http://127.0.0.1:2002/api/products");
                }
                catch (AggregateException aggEx)
                {
                    Console.WriteLine("Error opening server. Error: " + aggEx.Message);
                    Console.WriteLine();
                    Console.WriteLine("Try running Visual Studio as Administrator by right-clicking on the ");
                    Console.WriteLine("Visual Studio icon, and selecting \"Run as Administrator\"");
                    Console.WriteLine();
                    Console.WriteLine("Another work around is to open a command prompt as administrator and type:");
                    Console.WriteLine(@"netsh http add urlacl url=http://+:2002/ user=machine\username");
                    Console.WriteLine(@"replacing machine\username with your machine name, and user name.");
                }

                Console.WriteLine();
                Console.WriteLine("Close Window to stop listening and exit.");
                Console.ReadLine();
            }
        }
    }
}
