namespace DelightfulCode
{
    /// <summary>
    /// Use this attribute to indicate 'last known state' of the code so that others know how to approach it (without needing to read the code inside).
    /// NOTE: If the code does not fall into any of the following categories (i.e. no assessment has been made) then do not use, to reduce noise.
    /// IF MULTIPLE CLASSIFICATIONS EXIST, use additional attribute decoration.
    /// </summary>
    [Author("Warren James", 2017 - 2024)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Health(CodeStability.Stable)]
    public sealed class HealthAttribute : Attribute
    {
        private CodeStability semi_permanent_assessment = CodeStability.Unknown;

        /// <summary>
        /// Indicates the (currently known) health of this portion of code.
        /// </summary>
        public HealthAttribute(CodeStability current_assessment)
        {
            semi_permanent_assessment = current_assessment;
        }

        /// <summary>
        /// This is here so that it is possible to iterate/collect the flags via code. It's a work-around for a Microsoft known issue.
        /// I'm including it by default because the most useful thing about this attribute would be that we could create reports on the codebase map at some later time.
        /// Reference is: https://stackoverflow.com/questions/848669/allowmultiple-does-not-work-with-property-attributes/1365669#1365669
        /// </summary>
        public override object TypeId
        {
            get
            {
                return this;
            }
        }
    }
}
