namespace LibraryApi5.Constraints
{
    public class AllowedGenresConstraint : IRouteConstraint
    {
        private static readonly string[] AllowedGenres =
        {
            "fantasy",
            "mystery",
            "romance",
            "scifi",
            "dystopian",
            "classic",
            "horror",
            "adventure"
        };

        public bool Match(HttpContext? httpContext,
                          IRouter? route,
                          string parameterName,
                          RouteValueDictionary values,
                          RouteDirection routeDirection
        )
        {
            if (!values.TryGetValue(parameterName, out var rawValue))
            {
                return false;
            }
            
            var genre = rawValue?.ToString();

            return !string.IsNullOrEmpty(genre) &&
                    Array.Exists(AllowedGenres, g =>
                        g.Equals(genre, StringComparison.OrdinalIgnoreCase)
                    );
        }
    }
}

