using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitizenTech.Api.Models;

public class ChegadaOnibus
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string Linha { get; set; } = "";

    [Required, StringLength(100)]
    public string Destino { get; set; } = "";

    [Required]
    public DateTime PrevisaoChegada { get; set; }

    [Required, StringLength(20)]
    public string Status { get; set; } = "NO_HORARIO"; 

    [Range(1, int.MaxValue, ErrorMessage = "Informe um ponto válido.")]
    public int PontoOnibusId { get; set; }

    [JsonIgnore]
    public PontoOnibus? Ponto { get; set; }
}