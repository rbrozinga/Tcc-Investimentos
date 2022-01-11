using api.Models;
using Bitnvest.Business.Handlers;
using Bitnvest.DataAcess.Context;
using Bitnvest.DataAcess.Repository;
using Bitnvest.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InformacoesController : ControllerBase
    {
        [HttpGet("/{email}")]
        public IActionResult Get(string email)
        {
            try
            {
                var transacaoHandler = new TransacaoHandler();

                using (var db = new DbSqlContext())
                {
                    var _correntistaRepo = new CorrentistaRepository(db);


                    var correntista = _correntistaRepo.SelecionarPeloEmail(email);

                    if (correntista != null)
                    {
                        var transacoes = transacaoHandler.SelecionarTransacoesUsuario(correntista);

                        return Ok(new ContaDTO
                        {
                            Numero = correntista.Conta.Numero,
                            Saldo = correntista.Conta.Saldo,
                            SaldoFormatado = string.Format("{0:C}", correntista.Conta.Saldo),
                            Investimento = transacoes.ValorTotal,
                            InvestimentoFormatado = string.Format("{0:C}", transacoes.ValorTotal),

                        });
                    }
                    return NotFound();
                }
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}