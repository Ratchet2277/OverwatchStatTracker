using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public class Pagination<T>
    {
        //User friendly, start at 1
        public int Page
        {
            get => Page0 + 1;
            set => Page0 = value - 1;
        }

        //Start at 0, code firnedly
        public int Page0 { get; private set; }

        private readonly int? _pageSize;
        public List<T> List => _loadGames();
        private readonly IEnumerable<T> _query;
        public int NbPages { get; set; }

        public Pagination(IEnumerable<T> query, int page = 1, int? pageSize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Negative or zero are value not allowed here");
            }

            if (pageSize < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Negative or zero value are not allowed here");
            }
            _query = query;
            Page0 = page - 1;
            _pageSize = pageSize;
            if (_pageSize is not null)
            {
                NbPages = (int) Math.Ceiling((float) (_query.Count() / _pageSize));
            }
            else
            {
                NbPages = 1;
            }
            _loadGames();
        }

        private List<T> _loadGames()
        {
            if (_pageSize is null)
            {
                return _query.ToList();
            }
            return _query.Skip((int) (_pageSize * Page0)).Take((int) _pageSize).ToList();
        }

        public Pagination<T> Next(int nb = 1)
        {
            Page0 = Math.Min(Page0 + nb, NbPages - 1);
            return this;
        }

        public Pagination<T> Prev(int nb = 1)
        {
            Page0 = Math.Max(0, Page0 - nb);
            return this;
        }

        public int? NextPage => Page < NbPages ? Page + 1 : null;
        public int? PrevPage => Page > 0 ? Page - 1 : null;
    }
}