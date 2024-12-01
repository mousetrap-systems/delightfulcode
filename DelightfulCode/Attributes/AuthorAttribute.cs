namespace DelightfulCode
{
    /// <summary>
    /// Simple mechanism for flagging the author of a class or method,
    /// allows keeping track of contributors, useful in a team environment.
    /// Similar to NUNIT https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/attributes
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    [Author("GPT-4", 2023)]
    [Author("Warren James", 2022 - 2024)]
    [Health(CodeStability.Stable)]
    public sealed class AuthorAttribute : Attribute
    {
        private string name; // The publicly-available name of the contributor (not email address)
        private int[] years; // YYYY format of the year of contributions (if known) as a general guide.
        private CalendarMonth month_last_known_edit = CalendarMonth.Unspecified; // optional precise indicator of the last edit made

        /// <summary>
        /// Indicates the (currently known) health of this portion of code.
        /// You can specify multiple years, and also a range, for each year of contribution.
        /// </summary>
        /// <example>
        /// this: [Author("Warren James", 2018-2021, 2023)]
        /// </example>
        /// <param name="contributor_name">Who added to/was responsible for, this code logic? To protect privacy, do NOT use email addresses.</param>
        /// <param name="years">Optionally indicate the years of contribution (helps identify old code quickly), can use a 'dash' for continuous</param>
        public AuthorAttribute(string contributorDisplayName, params int[] yearsOfContributions)
        {
            this.name = contributorDisplayName;
            this.years = yearsOfContributions;
        }

        /// <summary>
        /// Use this signature if an author only had a very short stint in the code base, or where the extra precision on timing is significant.
        /// Generally only applies to a single year, because doesn't make sense for a range.
        /// </summary>
        [Health(CodeStability.Experimental)]
        public AuthorAttribute(string contributorDisplayName, int yearOfEdit, CalendarMonth whenLastChanged)
        {
            this.name = contributorDisplayName;
            this.years = new int[] { yearOfEdit };
            this.month_last_known_edit = whenLastChanged;
        }

        public string Name
        {
            get { return name; }
        }

        public int[] Years
        {
            get { return years; }
        }

        public CalendarMonth MostRecentEdit
        {
            get { return this.month_last_known_edit; }
        }
    }
}