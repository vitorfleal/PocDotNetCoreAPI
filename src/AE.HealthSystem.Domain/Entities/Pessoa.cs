namespace AE.HealthSystem.Domain.Entities
{
    public class Pessoa : BaseEntity<Pessoa>
    {
        public Pessoa(string nome)
        {
            Nome = nome;
        }

        public Pessoa() { }

        public string Nome { get; protected set; }
    }
}
