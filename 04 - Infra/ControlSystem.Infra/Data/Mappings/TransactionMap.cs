using ControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlSystem.Infra.Data.Mappings;


// Mapping da entidade Transaction para a tabela "Transaction".
// Define propriedades da transação e seus relacionamentos entre usuário e categoria
public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    // Configura o mapeamento da entidade Transaction.
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        // Define o nome da tabela
        builder.ToTable("Transaction");

        // dEfine a chave primária
        builder.HasKey(x => x.Id);

        // configuração da descrição da transação
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(255);

        // configuração do valor da transação
        builder.Property(x => x.Value)
               .HasPrecision(18, 2)
               .IsRequired();

        // tipo da transação (Income ou Expense)
        builder.Property(x => x.TransactionType)
            .IsRequired();

        // relacionamento com Category
        builder.HasOne(x => x.Category)
               .WithMany(c => c.Transactions)
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        // relacionamento com User
        builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId);
    }
}