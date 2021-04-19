using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CoursesController( IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourse(string mainCategory, bool includeModules = false)
        {
            //return (ActionResult)await uow.CourseRepository.GetAllCourses();
            var courses = await uow.CourseRepository.GetAllCourses(includeModules);
            var filter = await uow.CourseRepository.GetAllCourses(mainCategory);
            var coursesDto = mapper.Map<IEnumerable<CourseDto>>(courses);

            mapper.Map(coursesDto, filter);
            return Ok(coursesDto);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await uow.CourseRepository.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            }
            var courseDto = mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto dto)
        {
            var course = await uow.CourseRepository.GetCourse(id);
            
            if (course is null)
            {
                return BadRequest();
            }

            mapper.Map(dto, course);

            //var dto = mapper.Map<Course>(course);
            //_context.Entry(dto).State = EntityState.Modified;

            if(await uow.CourseRepository.SaveAsync())
            {
                return Ok(mapper.Map<Course>(course));
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
            //    if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseDto>> PostCourse(CourseDto course)
        {

            var courseDto = mapper.Map<Course>(course);
            await uow.CourseRepository.AddAsync(courseDto);
            await uow.CompleteAsync();

           

            return CreatedAtAction("GetCourse", new { id = courseDto.Id }, courseDto);
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {

            var course = await uow.CourseRepository.GetCourse(courseId);

            if(course is null)
            {
                return NotFound();
            }

            var model = mapper.Map<CourseDto>(course);

            patchDocument.ApplyTo(model, ModelState);

            if (!TryValidateModel(model))
            {
                return BadRequest(ModelState);
            }

            mapper.Map(model, course);

            if(await uow.CourseRepository.SaveAsync())
            {
                return Ok(mapper.Map<CourseDto>(model));
            }
            else
            {
                return StatusCode(500);
            }

        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await uow.CourseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            mapper.Map(course, id);
    
            uow.CourseRepository.Remove(course);
            await uow.CompleteAsync();

            return NoContent();
        }


    }
}
