namespace Breath.Models
{
    public class Cor
    {
        public string Name { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int  B { get; set; }

        public Cor()
        {
            
        }

        public Cor(string name, int r, int g, int b)
        {
            Name = name;
            R = r;
            G = g;
            B = b;
        }
    }
}