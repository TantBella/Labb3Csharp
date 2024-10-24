using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Labb3
{
    internal class GymSearch
    {
        public IEnumerable<GymSessions.GymSession> SearchSessions(IEnumerable<GymSessions.GymSession> sessions, string searchInput)
        {
            var result = sessions.AsQueryable();
            var searchTerms = searchInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var term in searchTerms)
            {
                if (TimeSpan.TryParse(term, out TimeSpan parsedTime))
                {
                    result = result.Where(p => p.TimeOfDay == parsedTime);
                }
                else
                {
                    result = result.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                                p.Category.Name.Contains(term, StringComparison.OrdinalIgnoreCase));
                }
            }
            return result;
        }

    }
}
