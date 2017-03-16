using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Net.Http;
/*
 * In the Manage NuGet Packages window, select Online option in left pan and search for web-api. This will list all the packages for Web API. Now, look for Microsoft ASP.NET Web API 2.2 Self Host package and click Install.
 *http://www.tutorialsteacher.com/webapi/web-api-hosting
 */
namespace WebApiSelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            /*In the above code, first we created an object of HttpSelfHostConfiguration class by passing uri location. Then, we created an object of HttpSelfHostServer by passing config and HttpMessageHandler object. And then we started listening for incoming request by calling server.OpenAsync() method. This will listen requests asynchronously, so it will return Task object.*/
            //need using System.Web.Http.SelfHost; aft downloading from NuGet & adding ref
            
            var config = new HttpSelfHostConfiguration("http://localhost:1234");

            var server = new HttpSelfHostServer(config, new MyWebAPIMessageHandler());//create a server, pass configuration obj & an API obj
            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Web API Server has started at http://localhost:1234");
            Console.ReadLine();
        }
    }

    class MyWebAPIMessageHandler : HttpMessageHandler //need using System.Net.Http;
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var task = new Task<HttpResponseMessage>(() =>
            {
                var resMsg = new HttpResponseMessage();
                resMsg.Content = new StringContent("Hello World!");
                return resMsg;
            });

            task.Start();
            return task;
        }
    }
}
