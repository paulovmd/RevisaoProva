using RevisaoProva.Models;

namespace RevisaoProva.Controllers
{
    public class ResultApplication 
    {
        public bool Success { get; set; }

        public String Error { get; set; }

        public String Message { get; set; }

        public List<Aluno> Dados { get; set; }

        public Aluno Aluno { get; set; }
        

    }
}
