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

        public void InsertOrUpdateWpEmissao(Emissao emissao)
        {
            using var connection = new MySqlConnection(_settings.DatabaseUrl);

            var exists = connection.ExecuteScalar<int>("SELECT COUNT(1) FROM wp_emissao WHERE SerieId = @SerieId", new { emissao.Meta.SerieId });

            if (exists > 0)
            {
                string sqlUpdate = @"
                    UPDATE wp_emissao SET
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

                connection.Execute(sqlUpdate, emissao.Meta);
            }
            else
            {
                connection.Open();

                using var transaction = connection.BeginTransaction();
                try
                {
                    // Primeiro, insira na tabela wp_posts
                    string sqlInsertPosts = @"
                            INSERT INTO wp_posts 
                                (post_author, post_date, post_date_gmt, post_content, post_title, post_excerpt, post_status, comment_status, 
                                 ping_status, post_password, post_name, to_ping, pinged, post_modified, post_modified_gmt, post_content_filtered, 
                                 post_parent, guid, menu_order, post_type, post_mime_type, comment_count)
                            VALUES 
                                (@PostAuthor, @PostDate, @PostDateGmt, @PostContent, @PostTitle, @PostExcerpt, @PostStatus, @CommentStatus, 
                                 @PingStatus, @PostPassword, @PostName, @ToPing, @Pinged, @PostModified, @PostModifiedGmt, @PostContentFiltered;";

                    // Inserindo na tabela wp_posts e recuperando o ID gerado
                    var postId = connection.ExecuteScalar<int>(sqlInsertPosts, new
                    {
                        PostAuthor = 3,
                        PostDate = DateTime.Now,
                        PostDateGmt = DateTime.UtcNow,
                        PostContent = "Post content",
                        PostTitle = emissao.Title,
                        PostExcerpt = string.Empty,
                        PostStatus = "publish",
                        CommentStatus = "closed",
                        PingStatus = "closed",
                        PostPassword = string.Empty,
                        PostName = emissao.Title.Replace(" ", "-").ToLower(), // Cria um slug de NomeFantasia
                        ToPing = string.Empty,
                        Pinged = string.Empty,
                        PostModified = DateTime.Now,
                        PostModifiedGmt = DateTime.UtcNow,
                        PostContentFiltered = string.Empty,
                        PostParent = 0,
                        Guid = "https://filadelfiasecuritizadora.com.br/emissoes/cri-reis-95anos/" + emissao.Title,
                        MenuOrder = 0,
                        PostType = "emissoes",
                        PostMimeType = string.Empty,
                        CommentCount = 0
                    }, transaction);

                    // Agora insira na tabela wp_emissoes usando o postId da wp_posts
                    string sqlInsertEmissoes = @"INSERT INTO wp_emissao (
                                 DataUltimoPagamento, JurosUltimoPagamento, AmortUltimoPagamento, AmexUltimoPagamento, DataProximoPagamento,
                                 JurosProximoPagamento, AmortProximoPagamento, AmexProximoPagamento, DurationEmAnos, Ratings, NaturezaLastro,
                                 CamaraDeLiquidacoes, PrivateDocs, NomeSerie, CodigoCETIP, NumeroEmissao, TipoEmissao, Emissor, AgenteFiduciario,
                                 NumeroSerie, ValorGlobalEmissao, DataEmissao, DataVencimento, DataLiquidacao, IndiceCorrecao, PercentualIndice,
                                 Spread, Remuneracao, CodigoISIN, PeriodoPagamentoAmort, PeriodoPagamentoJuros, Quantidade, PUEmissao, SerieId,
                                 NomeFantasia, IsSimulada, IsAtivo, IsRegimeFiduciario, TipoDeOferta, NomeEmissao, Cedentes, CedentesDocumentos,
                                 Devedores, TipoSuboordinacao, Escriturador, CoordenadorLider, NumeroDeCotas, Documents, MedicaoIntegralizacaoCotas
                             )
                             VALUES (
                                 @DataUltimoPagamento, @JurosUltimoPagamento, @AmortUltimoPagamento, @AmexUltimoPagamento, @DataProximoPagamento,
                                 @JurosProximoPagamento, @AmortProximoPagamento, @AmexProximoPagamento, @DurationEmAnos, @Ratings, @NaturezaLastro,
                                 @CamaraDeLiquidacoes, @PrivateDocs, @NomeSerie, @CodigoCETIP, @NumeroEmissao, @TipoEmissao, @Emissor, @AgenteFiduciario,
                                 @NumeroSerie, @ValorGlobalEmissao, @DataEmissao, @DataVencimento, @DataLiquidacao, @IndiceCorrecao, @PercentualIndice,
                                 @Spread, @Remuneracao, @CodigoISIN, @PeriodoPagamentoAmort, @PeriodoPagamentoJuros, @Quantidade, @PUEmissao, @SerieId,
                                 @NomeFantasia, @IsSimulada, @IsAtivo, @IsRegimeFiduciario, @TipoDeOferta, @NomeEmissao, @Cedentes, @CedentesDocumentos,
                                 @Devedores, @TipoSuboordinacao, @Escriturador, @CoordenadorLider, @NumeroDeCotas, @Documents, @MedicaoIntegralizacaoCotas
                             );";

                    // Inserindo na tabela wp_emissoes
                    connection.Execute(sqlInsertEmissoes, new
                    {
                        emissao.Meta.DataUltimoPagamento,
                        emissao.Meta.JurosUltimoPagamento,
                        emissao.Meta.AmortUltimoPagamento,
                        emissao.Meta.AmexUltimoPagamento,
                        emissao.Meta.DataProximoPagamento,
                        emissao.Meta.JurosProximoPagamento,
                        emissao.Meta.AmortProximoPagamento,
                        emissao.Meta.AmexProximoPagamento,
                        emissao.Meta.DurationEmAnos,
                        emissao.Meta.Ratings,
                        emissao.Meta.NaturezaLastro,
                        emissao.Meta.CamaraDeLiquidacoes,
                        emissao.Meta.PrivateDocs,
                        emissao.Meta.NomeSerie,
                        emissao.Meta.CodigoCETIP,
                        emissao.Meta.NumeroEmissao,
                        emissao.Meta.TipoEmissao,
                        emissao.Meta.Emissor,
                        emissao.Meta.AgenteFiduciario,
                        emissao.Meta.NumeroSerie,
                        emissao.Meta.ValorGlobalEmissao,
                        emissao.Meta.DataEmissao,
                        emissao.Meta.DataVencimento,
                        emissao.Meta.DataLiquidacao,
                        emissao.Meta.IndiceCorrecao,
                        emissao.Meta.PercentualIndice,
                        emissao.Meta.Spread,
                        emissao.Meta.Remuneracao,
                        emissao.Meta.CodigoISIN,
                        emissao.Meta.PeriodoPagamentoAmort,
                        emissao.Meta.PeriodoPagamentoJuros,
                        emissao.Meta.Quantidade,
                        emissao.Meta.PUEmissao,
                        emissao.Meta.SerieId,
                        emissao.Meta.NomeFantasia,
                        emissao.Meta.IsSimulada,
                        emissao.Meta.IsAtivo,
                        emissao.Meta.IsRegimeFiduciario,
                        emissao.Meta.TipoDeOferta,
                        emissao.Meta.NomeEmissao,
                        emissao.Meta.Cedentes,
                        emissao.Meta.CedentesDocumentos,
                        emissao.Meta.Devedores,
                        emissao.Meta.TipoSuboordinacao,
                        emissao.Meta.Escriturador,
                        emissao.Meta.CoordenadorLider,
                        emissao.Meta.NumeroDeCotas,
                        emissao.Meta.Documents,
                        emissao.Meta.MedicaoIntegralizacaoCotas
                    }, transaction);

                    // Se tudo correr bem, confirmar a transação
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Se ocorrer um erro, reverter a transação
                    transaction.Rollback();
                    // Aqui você pode registrar o erro ou lançá-lo novamente

                    connection.Close();
                    Console.WriteLine(ex.Message);
                }




            }
        }


        public void InsertOrUpdateDesagio(Desagio desagio)
        {

            using var connection = new MySqlConnection(_settings.DatabaseUrl);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string upsertQuery = @"
                            INSERT INTO wp_jet_cct_desagio (
                                _ID, cct_status, cct_author_id, cct_created, cct_modified, jurosesperado, jurosextra, 
                                jurospago, vnaj, seriesid, puresidual, `data`) 
                             VALUES (
                                 @Id, @CctStatus, @CctAuthorId, @CctCreated, @CctModified, @JurosEsperado, @JurosExtra, 
                                 @JurosPago, @VNAJ, @SeriesId, @PUResidual, @Data
                             )
                             ON DUPLICATE KEY UPDATE 
                                 cct_status = @CctStatus,
                                 cct_author_id = @CctAuthorId,
                                 cct_created = @CctCreated,
                                 cct_modified = @CctModified,
                                 jurosesperado = @JurosEsperado,
                                 jurosextra = @JurosExtra,
                                 jurospago = @JurosPago,
                                 vnaj = @VNAJ,
                                 seriesid = @SeriesId,
                                 puresidual = @PUResidual,
                                 `data` = @Data;";

                var parameters = new
                {
                    desagio.CctStatus,
                    desagio.CctAuthorId,
                    desagio.CctCreated,
                    desagio.CctModified,
                    desagio.JurosEsperado,
                    desagio.JurosExtra,
                    desagio.JurosPago,
                    desagio.VNAJ,
                    desagio.SeriesId,
                    desagio.PUResidual,
                    desagio.Data
                };

                int rowsAffected = connection.Execute(upsertQuery, parameters, transaction);


                transaction.Commit();

                Console.WriteLine("Upsert successful! Rows affected: " + rowsAffected);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error occurred: " + ex.Message);
            }



        }

    }
}
