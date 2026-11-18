using System.ComponentModel.DataAnnotations;

namespace CitizenTech.Api.Models;

public class PontoOnibus
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; } = "";

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    [StringLength(150)]
    public string Endereco { get; set; } = "";

    public List<ChegadaOnibus> Chegadas { get; set; } = new();
}