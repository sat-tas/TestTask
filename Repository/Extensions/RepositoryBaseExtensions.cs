using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Extensions
{
    public static class RepositoryBaseExtensions
    {
        public static IQueryable<Department> Search(this IQueryable<Department> departments, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return departments;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return departments.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Employee> Search(this IQueryable<Employee> departments, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return departments;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return departments.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}
