using ControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlSystem.Infra.Data.Mappings;
// Mapping da entidade Category para a tabela "Category".
// Define as propriedades e restrições da categoria de transações.
public class CategoryMap : IEntityTypeConfiguration<Category>
{

    // Configura o mapeamento da entidade Category.
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Define o nome da tabela
        builder.ToTable("Category");

        // Define a chave primária
        builder.HasKey(x => x.Id);

        // Configuração da descrição da categoria
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(255);

        // Tipo da categoria (Income ou Expense)
        builder.Property(x => x.PurposeType)
            .IsRequired();
    }
}