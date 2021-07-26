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
            get => _page + 1;
            set => _page = value - 1;
        }

        //Start at 0;
        public int Page0 => _page;
        private int _page;
        private readonly int _pageSize;
        public List<T> List => _loadGames();
        private readonly IEnumerable<T> _query;
        public int NbPages { get; set; }

        public Pagination(IEnumerable<T> query, int page = 1, int pageSize = 10)
        {
            _query = query;
            _page = page - 1;
            _pageSize = pageSize;
            NbPages = (int) Math.Ceiling((float) _query.Count() / _pageSize);
            _loadGames();
        }

        private List<T> _loadGames()
        {
            return _query.Skip(_pageSize * _page).Take(_pageSize).ToList();
        }

        public Pagination<T> Next(int nb = 1)
        {
            _page = Math.Min(_page + nb, NbPages - 1);
            return this;
        }

        public Pagination<T> Prev(int nb = 1)
        {
            _page = Math.Max(0, _page - nb);
            return this;
        }

        public int? NextPage => Page < NbPages ? Page + 1 : null;
        public int? PrevPage => Page > 0 ? Page - 1 : null;
    }
}