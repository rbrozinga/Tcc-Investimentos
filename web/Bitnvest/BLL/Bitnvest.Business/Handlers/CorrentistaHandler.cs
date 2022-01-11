using System;
using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Model.Models;
using Bitnvest.Model.ModelView;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Bitnvest.Business.Handlers
{
    public class CorrentistaHandler
    {
        private DbSqlContext _db = new DbSqlContext();
        private ContaRepository _contaRepo;
        private CorrentistaRepository _correntistaRepo;
        private readonly IHttpContextAccessor _http;

        public CorrentistaHandler()
        {
            _contaRepo = new ContaRepository(_db);
            _correntistaRepo = new CorrentistaRepository(_db);
        }

        public CorrentistaHandler(IHttpContextAccessor http)
        {
            _contaRepo = new ContaRepository(_db);
            _correntistaRepo = new CorrentistaRepository(_db);
            _http = http;
        }

        public Correntista SelecionarCorrentista(string id)
        {
            var correntista = _correntistaRepo.SelecionarPeloId(int.Parse(id));
            return correntista;
        }

        public void AdicionarSaldo(Correntista cc)
        {
            cc.Conta.Saldo += 100M;
            _contaRepo.Atualizar(cc.Conta);
            _db.SaveChanges();
        }

        public Correntista CriarCorrentista(CadastroDTO dto)
        {
            var correntista = _correntistaRepo.SelecionarPeloEmail(dto.Email);

            if(correntista != null)
            {
                return correntista;
            }

            var cadastro = new Correntista();

            var random = new Random();
            var conta = new Conta
            {
                Numero = random.Next(1000, 9000),
                Saldo = 3500.00M
            };

            cadastro.Nome = dto.Nome.ToUpper().Trim();
            cadastro.Cel = dto.Cel;
            cadastro.CPF = dto.CPF;
            cadastro.Senha = dto.Senha;
            cadastro.Email = dto.Email.ToLower().Trim();
            cadastro.DataNascimento = dto.DataNascimento.Value;
            cadastro.Conta = conta;

            _contaRepo.Inserir(conta);
            _correntistaRepo.Inserir(cadastro);

            _db.SaveChanges();

            return null;

        }

        public async void Deslogar()
        {
            await _http.HttpContext.SignOutAsync();
        }

        public bool Logar(LoginDTO login)
        {
            var usuario = _correntistaRepo.SelecionarPeloLogin(login.Email.ToLower().Trim(), login.Senha);

            if (usuario != null)
            {
                Login(usuario);
                return true;
            }

            return false;
        }

        private async void Login(Correntista cc)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cc.Id.ToString()),
                new Claim(ClaimTypes.Role, "Usuario_Comum")
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddHours(2),
                IsPersistent = true
            };

            await _http.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }
    }
}
