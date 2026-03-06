using ControlSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlSystem.Infra.Data.Mappings;

/* Mapping da entidade User para a tabela "User" no banco de dados.
 Responsável por definir chave primária, propriedades obrigatórias, tamanho de campos e índices.*/
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Define o nome da tabela
        builder.ToTable("User");

        // define a chave primária
        builder.HasKey(x => x.Id);

        // configuração da propriedade Name
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        // configuração da propriedade Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        // configuração da propriedade BirthDate
        builder.Property(x => x.BirthDate)
            .IsRequired();

        // cria índice único para evitar e-mails duplicados
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}