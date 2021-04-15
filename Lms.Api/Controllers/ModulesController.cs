using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly LmsApiContext _context;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ModulesController(LmsApiContext context, IUnitOfWork uow, IMapper mapper)
        {
            _context = context;
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            //return (ActionResult)await uow.ModuleRepository.GetAllModules();

            var model = await uow.ModuleRepository.GetAllModules();
            var dto = mapper.Map<IEnumerable<ModuleDto>>(model);
            return Ok(dto);
        }

        // GET: api/Modules/5
        [HttpGet("{title}")]
        public async Task<ActionResult<ModuleDto>> GetModule(string title)
        {
            var @module = await uow.ModuleRepository.GetModule(title);

            if (@module == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<ModuleDto>(@module);

            return Ok(dto);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{title}")]
        public async Task<IActionResult> PutModule(string title, ModuleDto @module)
        {
            var model = await uow.ModuleRepository.GetModule(title);

            if (model is null)
            {
                return BadRequest();
            }

            mapper.Map(module, model);

            //var dto = mapper.Map<Module>(@module);
            //_context.Entry(dto).State = EntityState.Modified;
            
            if(await uow.ModuleRepository.SaveAsync())
            {
                return Ok(mapper.Map<Module>(model));
            }
            else
            {
                return StatusCode(500);
            }

            //try
            //{
            //    await uow.CompleteAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ModuleExists(id))
            //    {
            //        return StatusCode(500);
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleDto @module)
        {
            var dto = mapper.Map<Module>(@module);
            await uow.ModuleRepository.AddAsync(dto);
            await uow.CompleteAsync();       

            return CreatedAtAction("GetModule", new { id = dto.Id }, dto);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteModule(string title)
        {
            var @module = await uow.ModuleRepository.GetModule(title);
            if (@module == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<Module>(@module);
            uow.ModuleRepository.Remove(dto);
            
            await uow.CompleteAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
