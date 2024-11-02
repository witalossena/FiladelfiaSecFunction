using Dapper;
using FiladelfiaFunction.Akrun.Models;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Data
{
    public class FiladelfiaDb
    {
        private readonly FiladelfiaSettings _settings;
        public FiladelfiaDb(IOptions<FiladelfiaSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> PostExist(string post_title)
        {
            using var connection = new MySqlConnection(_settings.DatabaseUrl);

            var exists = await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM wp_posts WHERE post_status = 'publish' AND post_title = @post_title", new { post_title });

            if (exists == 0)
            {
                return false;
            }

            return true;

        }

        public void UpdateWpEmissao(DetalhesEmissao emissao)
        {
            using var connection = new MySqlConnection(_settings.DatabaseUrl);         

            string sqlUpdate = @"
                    UPDATE wp_jet_cct_detalhes SET
                        NomeFantasia = @NomeFantasia,
                        DataUltimoPagamento = @DataUltimoPagamento,
                        JurosUltimoPagamento = @JurosUltimoPagamento,
                        AmortUltimoPagamento = @AmortUltimoPagamento,
                        AmexUltimoPagamento = @AmexUltimoPagamento,
                        DataProximoPagamento = @DataProximoPagamento,
                        JurosProximoPagamento = @JurosProximoPagamento,
                        AmortProximoPagamento = @AmortProximoPagamento,
                        AmexProximoPagamento = @AmexProximoPagamento,
                        DurationEmAnos = @DurationEmAnos,
                        Ratings = @Ratings,
                        NaturezaLastro = @NaturezaLastro,
                        CamaraDeLiquidacoes = @CamaraDeLiquidacoes,
                        PrivateDocs = @PrivateDocs,
                        NomeSerie = @NomeSerie,
                        CodigoCETIP = @CodigoCETIP,
                        NumeroEmissao = @NumeroEmissao,
                        TipoEmissao = @TipoEmissao,
                        Emissor = @Emissor,
                        AgenteFiduciario = @AgenteFiduciario,
                        NumeroSerie = @NumeroSerie,
                        ValorGlobalEmissao = @ValorGlobalEmissao,
                        DataEmissao = @DataEmissao,
                        DataVencimento = @DataVencimento,
                        DataLiquidacao = @DataLiquidacao,
                        IndiceCorrecao = @IndiceCorrecao,
                        PercentualIndice = @PercentualIndice,
                        Spread = @Spread,
                        Remuneracao = @Remuneracao,
                        CodigoISIN = @CodigoISIN,
                        PeriodoPagamentoAmort = @PeriodoPagamentoAmort,
                        PeriodoPagamentoJuros = @PeriodoPagamentoJuros,
                        Quantidade = @Quantidade,
                        PUEmissao = @PUEmissao,
                        IsSimulada = @IsSimulada,
                        IsAtivo = @IsAtivo,
                        IsRegimeFiduciario = @IsRegimeFiduciario,
                        TipoDeOferta = @TipoDeOferta,
                        NomeEmissao = @NomeEmissao,
                        Cedentes = @Cedentes,
                        CedentesDocumentos = @CedentesDocumentos,
                        Devedores = @Devedores,
                        TipoSuboordinacao = @TipoSuboordinacao,
                        Escriturador = @Escriturador,
                        CoordenadorLider = @CoordenadorLider,
                        NumeroDeCotas = @NumeroDeCotas,
                        Documents = @Documents,
                        MedicaoIntegralizacaoCotas = @MedicaoIntegralizacaoCotas
                 WHERE SerieId = @SerieId";

            connection.Execute(sqlUpdate, emissao);

        }


    }
}
