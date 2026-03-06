using ControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlSystem.Infra.Data.Context
{
    // Contexto principal da aplicação, responsável pela comunicação com o banco de dados
    public class ApplicationContext : DbContext
    {
        // O construtor recebe as configurações do banco 
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        // Tabela de usuários
        public DbSet<User> Users => Set<User>();

        // Tabela de transações 
        public DbSet<Transaction> Transactions => Set<Transaction>();

        // Tabela de categorias
        public DbSet<Category> Categories => Set<Category>();

        // método utilizado para procurar as classes de mapeamento como a usermap
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}