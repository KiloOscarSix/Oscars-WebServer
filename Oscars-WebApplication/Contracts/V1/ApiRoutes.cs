namespace Oscars_WebApplication.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = $"{Root}/{Version}";
    
    public static class Posts
    {
        public const string GetAll = $"{Base}/posts";
        public const string Get = $"{Base}/posts/{{postId}}";
        public const string Create = $"{Base}/posts";
    }
    
    public static class LovenseCallback
    {
        public const string GetAll = $"{Base}/lovense/users";
        public const string Get = $"{Base}/lovense/users/{{userId}}";
        public const string Create = $"{Base}/lovense/users";
    }
}