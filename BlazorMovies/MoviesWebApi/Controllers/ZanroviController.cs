﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApi.Commands.Zanrovi;
using MoviesWebApi.Models;
using MoviesWebApi.Queries.VratiSveZanrove;
using MoviesWebApi.Queries.VratiZanrPoId;
using MoviesWebApi.Shared;
using MoviesWebApi.ViewModels;
using System.Threading;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZanroviController : ApiController
    {

        public ZanroviController(ISender sender)
            :base(sender)
        {
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZanrDto>>> GetZanrovi(CancellationToken cancelationToken)
        {
            var query = new VratiSveZanroveQuery();
            Result<List<ZanrJedanResponse>> res = await _sender.Send(query, cancelationToken);
            return Ok(res.Value);
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanrDto>> GetZanr(int id, CancellationToken cancelationToken)
        {
            var query = new VratiZanrPoIdQuery(id);
            Result<ZanrResponse> res = await _sender.Send(query, cancelationToken);
            if (!res.IsSucess)
                return NotFound(res.Error);
            return Ok(res.Value);
        }

        // PUT: api/Zanrovi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZanr(int id, ZanrDto zanrDto, CancellationToken cancelationToken)
        {
            if (id != zanrDto.ZanrId)
            {
                return BadRequest();
            }
            var command = new PostaviZanrCommand(id, zanrDto.ZanrId, zanrDto.Naziv);
            var res = await _sender.Send(command, cancelationToken);
            if (res.IsFaliure)
                return BadRequest(res.Error);
            return NoContent();
        }

        // POST: api/Zanrovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(ZanrDto zanrDto, CancellationToken cancelationToken)
        {
            var command = new DodajZanrCommand(zanrDto.ZanrId, zanrDto.Naziv);
            var res = await _sender.Send(command, cancelationToken);
            if(res.IsFaliure)
                return BadRequest(res.Error);
            return CreatedAtAction("GetZanr", new { id = zanrDto.ZanrId }, zanrDto);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZanr(int id, CancellationToken cancelationToken)
        {
            var command = new ObrisiZanrCommand(id);
            var res = await _sender.Send(command, cancelationToken);
            if (res.IsFaliure)
                return BadRequest(res.Error);
            return NoContent();
        }

    }
}
