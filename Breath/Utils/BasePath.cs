namespace Breath.Utils
{
    public class BasePath
    {
        public static string Assets(string name) => $"Assets/{name}";
        public static string Images(string name) => $"Assets/Images/{name}";
        public static string Shaders(bool vtx,string name) => $"Shaders/"+ (vtx ? "Vertex": "Frags")+$"/{name}";
        public static string Sound(string name) => $"Assets/Sounds/{name}";
    }
}