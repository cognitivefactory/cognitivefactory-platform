﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CognitiveFactory.Platform.API.Data;
using CognitiveFactory.Platform.API.Models;

namespace CognitiveFactory.Platform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionsController : ControllerBase
    {
        private readonly CognitiveFactoryPlatformAPIContext _context;

        public SolutionsController(CognitiveFactoryPlatformAPIContext context)
        {
            _context = context;
        }

        // GET: api/Solutions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solution>>> GetSolution()
        {
            return await _context.Solutions.ToListAsync();
        }

        // GET: api/Solutions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solution>> GetSolution(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);

            if (solution == null)
            {
                return NotFound();
            }

            return solution;
        }

        // PUT: api/Solutions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolution(int id, Solution solution)
        {
            if (id != solution.Id)
            {
                return BadRequest();
            }

            _context.Entry(solution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolutionExists(id))
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

        // POST: api/Solutions
        [HttpPost]
        public async Task<ActionResult<Solution>> PostSolution(Solution solution)
        {
            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolution", new { id = solution.Id }, solution);
        }

        // DELETE: api/Solutions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Solution>> DeleteSolution(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();

            return solution;
        }

        private bool SolutionExists(int id)
        {
            return _context.Solutions.Any(e => e.Id == id);
        }
    }
}