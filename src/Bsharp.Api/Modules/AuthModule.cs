namespace Bsharp.Api
{
    using Nancy;

    public class AuthModule : NancyModule
    {
        public AuthModule()
        {
            Get("/Battle", parameters => {
                return Response.AsJson("true");
            });
        }
    }
}