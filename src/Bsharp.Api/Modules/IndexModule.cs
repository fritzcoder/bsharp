namespace Bsharp.Api
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get("/", _ => "Tune it to Bsharp");

        }
    }
}