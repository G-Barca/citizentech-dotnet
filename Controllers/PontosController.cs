using CitizenTech.Api.Data;
using CitizenTech.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitizenTech.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PontosController : ControllerBase
{
    private readonly AppDbContext _db;
    public PontosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PontoOnibus>>> Get()
        => await _db.Pontos.AsNoTracking().ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<PontoOnibus>> GetById(int id)
    {
        var ponto = await _db.Pontos.Include(p => p.Chegadas)
                             .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (ponto is null) return NotFound(new { mensagem = "Ponto não encontrado." });
        return ponto;
    }
    
    [HttpGet("{id}/chegadas")]
    public async Task<ActionResult<IEnumerable<ChegadaOnibus>>> GetChegadas(int id)
    {
        if (!await _db.Pontos.AnyAsync(p => p.Id == id))
            return NotFound(new { mensagem = "Ponto não encontrado." });

        return await _db.Chegadas.AsNoTracking()
            .Where(c => c.PontoOnibusId == id)
            .OrderBy(c => c.PrevisaoChegada).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<PontoOnibus>> Post(PontoOnibus ponto)
    {
        _db.Pontos.Add(ponto);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = ponto.Id }, ponto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PontoOnibus dados)
    {
        if (id != dados.Id)
            return BadRequest(new { mensagem = "O id da rota difere do id do corpo." });

        var ponto = await _db.Pontos.FindAsync(id);
        if (ponto is null) return NotFound(new { mensagem = "Ponto não encontrado." });

        ponto.Nome = dados.Nome;
        ponto.Endereco = dados.Endereco;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ponto = await _db.Pontos.FindAsync(id);
        if (ponto is null) return NotFound(new { mensagem = "Ponto não encontrado." });

        _db.Pontos.Remove(ponto);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}