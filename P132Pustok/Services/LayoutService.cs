using P132Pustok.DAL;
using P132Pustok.Models;

namespace P132Pustok.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;

        public LayoutService(PustokContext context)
        {
            _context = context;
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}
