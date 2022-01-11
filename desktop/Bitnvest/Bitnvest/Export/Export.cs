using Bitnvest.Model.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Bitnvest.Export
{
    public static class Export
    {
        public static void ExportToExcel(List<Transacao> transacoes, string excelFilePath = null)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    if (transacoes.Count == 0)
                        throw new Exception("ExportToExcel: Sem dados para Exportação!\n");

                    // load excel, and create a new workbook
                    var worksheet = workbook.Worksheets.Add("Transacoes");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = "Numero Conta";
                    worksheet.Cell(currentRow, 2).Value = "Nome do Cliente";
                    worksheet.Cell(currentRow, 3).Value = "CPF";
                    worksheet.Cell(currentRow, 4).Value = "Valor";
                    worksheet.Cell(currentRow, 5).Value = "Tipo Transacao";
                    worksheet.Cell(currentRow, 6).Value = "Data Transacao";


                    foreach (var transacao in transacoes)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = transacao.Conta.Numero;
                        worksheet.Cell(currentRow, 2).Value = transacao.Conta.Correntista.Nome;
                        worksheet.Cell(currentRow, 3).Value = transacao.Conta.Correntista.CPF;
                        worksheet.Cell(currentRow, 4).Value = transacao.Valor;
                        worksheet.Cell(currentRow, 5).Value = transacao.TipoTransacao.ToString();
                        worksheet.Cell(currentRow, 6).Value = transacao.DataTransacao.Date;

                    }


                    // check file path
                    if (!string.IsNullOrEmpty(excelFilePath))
                    {
                        try
                        {
                            workbook.SaveAs(excelFilePath);
                            workbook.Dispose();
                            MessageBox.Show("Excel salvo com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("ExportToExcel: Exportação falhou.\n"
                                                + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}

