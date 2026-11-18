using CitizenTech.Api.Data;
using CitizenTech.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitizenTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChegadasController : ControllerBase
{
    private readonly AppDbContext _db;
    public ChegadasController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChegadaOnibus>>> Get()
        => await _db.Chegadas.AsNoTracking().ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<ChegadaOnibus>> GetById(int id)
    {
        var c = await _db.Chegadas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (c is null) return NotFound(new { mensagem = "Chegada não encontrada." });
        return c;
    }

    [HttpPost]
    public async Task<ActionResult<ChegadaOnibus>> Post(ChegadaOnibus chegada)
    {
        if (!await _db.Pontos.AnyAsync(p => p.Id == chegada.PontoOnibusId))
            return BadRequest(new { mensagem = "Ponto de ônibus inexistente." });

        _db.Chegadas.Add(chegada);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = chegada.Id }, chegada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ChegadaOnibus dados)
    {
        if (id != dados.Id)
            return BadRequest(new { mensagem = "O id da rota é diferente do id do corpo." });

        var c = await _db.Chegadas.FindAsync(id);
        if (c is null) return NotFound(new { mensagem = "Chegada não encontrada." });

        if (!await _db.Pontos.AnyAsync(p => p.Id == dados.PontoOnibusId))
            return BadRequest(new { mensagem = "Ponto de ônibus inexistente." });

        c.Linha = dados.Linha;
        c.Destino = dados.Destino;
        c.PrevisaoChegada = dados.PrevisaoChegada;
        c.Status = dados.Status;
        c.PontoOnibusId = dados.PontoOnibusId;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var c = await _db.Chegadas.FindAsync(id);
        if (c is null) return NotFound(new { mensagem = "Chegada não encontrada." });

        _db.Chegadas.Remove(c);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}