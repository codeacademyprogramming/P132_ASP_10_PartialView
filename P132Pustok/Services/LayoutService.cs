﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P132Pustok.DAL;
using P132Pustok.Models;
using P132Pustok.ViewModels;
using System.Security.Claims;

namespace P132Pustok.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public LayoutService(PustokContext context,IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public BasketViewModel GetBasket()
        {
            BasketViewModel basket = new BasketViewModel();

            if (_httpAccessor.HttpContext.User.Identity.IsAuthenticated && _httpAccessor.HttpContext.User.IsInRole("Member"))
            {
                string userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


                var model = _context.BasketItems.Include(x => x.Book).ThenInclude(x => x.BookImages).Where(x=>x.AppUserId== userId).ToList();


                foreach (var item in model)
                {
                    BasketItemViewModel itemVM = new BasketItemViewModel
                    {
                        Book = item.Book,
                        Count = item.Count,
                        Id = item.Id
                    };

                    basket.Items.Add(itemVM);
                    basket.TotalPrice += item.Count * (item.Book.SalePrice * (100 - item.Book.DiscountPercent) / 100);
                }
            }
            else
            {
                var basketStr = _httpAccessor.HttpContext.Request.Cookies["basket"];

                List<BasketItemCookieViewModel> basketCookieItems = new List<BasketItemCookieViewModel>();

                if (basketStr != null)
                {
                    basketCookieItems = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);
                }
                foreach (var item in basketCookieItems)
                {
                    Book book = _context.Books.Include(x => x.BookImages).FirstOrDefault(x => x.Id == item.BookId);

                    BasketItemViewModel itemVM = new BasketItemViewModel
                    {
                        Book = book,
                        Count = item.Count,
                        Id = 0
                    };

                    basket.Items.Add(itemVM);
                    basket.TotalPrice += item.Count * (itemVM.Book.SalePrice * (100 - itemVM.Book.DiscountPercent) / 100);
                }
            }

           

            return basket;
        }
    }
}
