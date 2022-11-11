﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Data;
using MoviesWebApi.Models;
using MoviesWebApi.ViewModels;

namespace MoviesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZanroviController : ControllerBase
    {
        private readonly MoviesWebApiContext _context;
        private readonly IMapper _mapper;

        public ZanroviController(MoviesWebApiContext context, IMapper mapper)
        {
            _context = context ?? throw new InvalidEnumArgumentException(nameof(context));
            _mapper = mapper ?? throw new InvalidEnumArgumentException(nameof(mapper));
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZanrDto>>> GetZanrovi()
        {
            List<Zanr> zanrovi = await _context.Zanr.ToListAsync();
            List<ZanrDto> ret = _mapper.Map<List<ZanrDto>>(zanrovi);
            return Ok(ret);
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanrDto>> GetZanr(int id)
        {
            var zanr = await _context.Zanr.Where(z=> z.ZanrId == id).SingleOrDefaultAsync();
            if (zanr == null)
            {
                return NotFound();
            }
            return _mapper.Map<ZanrDto>(zanr);
        }

        // PUT: api/Zanrovi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZanr(int id, ZanrDto zanrDto)
        {
            if (id != zanrDto.ZanrId)
            {
                return BadRequest();
            }
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
                return NotFound();
            _mapper.Map<ZanrDto, Zanr>(zanrDto, zanr); 
            _context.Entry(zanr).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Zanrovi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(ZanrDto zanrDto)
        {
            Zanr zanr = _mapper.Map<Zanr>(zanrDto);
            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetZanrovi", new { id = zanr.ZanrId }, zanrDto);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }
            _context.Zanr.Remove(zanr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.ZanrId == id);
        }
    }
}
