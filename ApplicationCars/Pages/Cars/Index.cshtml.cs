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
using Microsoft.AspNetCore.Authorization;

namespace ApplicationCars.Pages.Cars
{
    [Authorize(Policy = "IsAdmin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationCars.Data.ApplicationCarsContext _context;

        public IndexModel(ApplicationCars.Data.ApplicationCarsContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Car> Car { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Models { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CarModel { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Use LINQ to get the list of distinct car models for dropdown
            IQueryable<string> modelQuery = from m in _context.Car
                                            orderby m.Model
                                            select m.Model;

            // Get the list of cars
            var cars = from c in _context.Car
                       select c;

            // Apply search filter
            if (!string.IsNullOrEmpty(SearchString))
            {
                cars = cars.Where(s => s.Brand.Contains(SearchString));
            }

            // Apply model filter
            if (!string.IsNullOrEmpty(CarModel))
            {
                cars = cars.Where(x => x.Model == CarModel);
            }

            // Apply sorting based on the sortOrder parameter
            ViewData["BrandSort"] = string.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";
            ViewData["ModelSort"] = sortOrder == "model" ? "model_desc" : "model";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["ProductionYearSort"] = sortOrder == "productionyear" ? "productionyear_desc" : "productionyear";

            // Apply sorting based on the sortOrder parameter
            switch (sortOrder)
            {
                case "brand_desc":
                    cars = cars.OrderByDescending(s => s.Brand);
                    break;
                case "model":
                    cars = cars.OrderBy(s => s.Model);
                    break;
                case "model_desc":
                    cars = cars.OrderByDescending(s => s.Model);
                    break;
                case "price":
                    cars = cars.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    cars = cars.OrderByDescending(s => s.Price);
                    break;
                case "productionyear":
                    cars = cars.OrderBy(s => s.ProductionYear);
                    break;
                case "productionyear_desc":
                    cars = cars.OrderByDescending(s => s.ProductionYear);
                    break;
                default:
                    cars = cars.OrderBy(s => s.Brand);
                    break;
            }

            // Set the SelectList for the dropdown
            Models = new SelectList(await modelQuery.Distinct().ToListAsync());

            // Set the Car property with the sorted and filtered list
            Car = await cars.ToListAsync();
        }

    }
}
