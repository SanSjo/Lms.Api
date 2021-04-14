using Bogus;
using Lms.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(IServiceProvider service)
        {
            using (var context = new LmsApiContext(service.GetRequiredService<DbContextOptions<LmsApiContext>>()))
            {
                if (await context.Course.AnyAsync()) return;

                var fake = new Faker("sv");

                var courses = new List<Course>();

                for (int i = 0; i < 20; i++)
                {
                    var course = new Course
                    {
                        Title = fake.Company.CatchPhrase(),
                        StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2)),
                        Modules = new Module[]
                        {
                            new Module
                            {
                               Title = fake.Company.CompanyName(),
                               StartDate = DateTime.Now.AddDays(fake.Random.Int(-2, 2))
                            }

                        }

                    };

                    courses.Add(course);
                }
                await context.AddRangeAsync(courses);

                await context.SaveChangesAsync();
            }
        }
    }
}
