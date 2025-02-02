using Dapper;
using FiladelfiaFunction.Filadelfia.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace FiladelfiaFunction.Data
{
  public class FiladelfiaDb(IOptions<FiladelfiaSettings> settings)
  {
    private readonly FiladelfiaSettings _settings = settings.Value;

    public async Task<bool> PostExist(string post_title)
    {
      if (string.IsNullOrEmpty(_settings.DatabaseUrl))
      {
        throw new ArgumentNullException(nameof(_settings.DatabaseUrl), "Database connection string cannot be null or empty.");
      }

      if (string.IsNullOrEmpty(post_title))
      {
        throw new ArgumentNullException(nameof(post_title), "Post title cannot be null or empty.");
      }

      try
      {
        using var connection = new MySqlConnection(_settings.DatabaseUrl);
        await connection.OpenAsync().ConfigureAwait(false);

        string sql = @"
            SELECT COUNT(1) 
            FROM wp_posts 
            WHERE post_status = 'publish' 
            AND post_title = @post_title";

        var exists = await connection.ExecuteScalarAsync<bool>(sql, new { post_title }).ConfigureAwait(false);

        return exists;
      }
      catch (MySqlException ex)
      {
        Console.Error.WriteLine($"Database error: {ex.Message}");
        throw; 
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        throw;
      }
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
                        MedicaoIntegralizacaoCotas = @MedicaoIntegralizacaoCotas
                 WHERE SerieId = @SerieId";

      connection.Execute(sqlUpdate, emissao);

    }

    public void DeleteDesagioJetCct(string seriesid)
    {
      using var connection = new MySqlConnection(_settings.DatabaseUrl);

      string sql = "DELETE FROM wp_jet_cct_desagio WHERE seriesid = @seriesid";

      connection.Execute(sql, new { seriesid });

    }

    public async Task DeleteDocumentos()
    {
      if (string.IsNullOrEmpty(_settings.DatabaseUrl))
      {
        throw new ArgumentNullException(nameof(_settings.DatabaseUrl), "Database connection string cannot be null or empty.");
      }

      try
      {
        using var connection = new MySqlConnection(_settings.DatabaseUrl);
        await connection.OpenAsync().ConfigureAwait(false);

        string sql = "DELETE FROM wp_jet_cct_detalhesdocumentos";

        int affectedRows = await connection.ExecuteAsync(sql).ConfigureAwait(false);

        Console.WriteLine($"Deleted {affectedRows} rows from wp_jet_cct_detalhesdocumentos.");
      }
      catch (MySqlException ex)
      {
        Console.Error.WriteLine($"Database error: {ex.Message}");
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        throw;
      }
    }

  }
}
