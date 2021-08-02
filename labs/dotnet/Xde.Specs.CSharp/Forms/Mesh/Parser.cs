namespace Xde.Forms.Mesh
{
    public class Parser
    {
        public bool Token(string token) => true;

        public bool Next(string token) => true;

        public static implicit operator Parser(string value)
        {
            return new Parser();
        }
    }
}
