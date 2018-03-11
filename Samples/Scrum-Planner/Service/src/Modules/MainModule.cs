namespace Service.Modules
{
    public class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            Get("/", action: async (args, ct) =>
            {
                return "Scrum-Planner API-Service";
            });
        }
    }
}
