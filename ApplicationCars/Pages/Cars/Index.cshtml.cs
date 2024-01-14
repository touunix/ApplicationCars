using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCars.Data;
using ApplicationCars.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApplicationCars.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationCars.Data.ApplicationCarsContext _context;

        public IndexModel(ApplicationCars.Data.ApplicationCarsContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Models { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CarModel { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> modelQuery = from m in _context.Car
                                            orderby m.Model
                                            select m.Model;

            var cars = from c in _context.Car
                         select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                cars = cars.Where(s => s.Brand.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(CarModel))
            {
                cars = cars.Where(x => x.Model == CarModel);
            }

            Models = new SelectList(await modelQuery.Distinct().ToListAsync());
            Car = await cars.ToListAsync();
        }
    }
}
