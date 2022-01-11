using Bitnvest.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Bitnvest.DataAcess.Context
{
    public class DbSqlContext : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Correntista> Correntistas { get; set; }
        public DbSet<Moeda> Moedas { get; set; }
        public DbSet<MoedaTransacao> MoedaTransacoes { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }


        //Sistema de Tarifas
        public DbSet<Administracao> Administradores { get; set; }
        public DbSet<Tarifa> Tarifas { get; set; }
        public DbSet<PagamentoTarifas> PagamentosTarifas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("database.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Sql"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PagamentoTarifas>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<PagamentoTarifas>()
                .HasOne(bc => bc.Correntista)
                .WithMany(b => b.Tarifas)
                .HasForeignKey(bc => bc.IdCorrentista);

            modelBuilder.Entity<PagamentoTarifas>()
                .HasOne(bc => bc.Tarifa)
                .WithMany(c => c.Pagamentos)
                .HasForeignKey(bc => bc.IdTarifa);

            modelBuilder.Entity<Correntista>()
                .HasOne(x => x.Conta)
                .WithOne(x => x.Correntista)
                .HasForeignKey<Correntista>(x => x.IdConta);

            base.OnModelCreating(modelBuilder);
        }

    }
}
