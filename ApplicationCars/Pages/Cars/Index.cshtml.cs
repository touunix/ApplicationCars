﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCars.Data;
using ApplicationCars.Models;

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

        public async Task OnGetAsync()
        {
            if (_context.Car != null)
            {
                Car = await _context.Car.ToListAsync();
            }
        }
    }
}
