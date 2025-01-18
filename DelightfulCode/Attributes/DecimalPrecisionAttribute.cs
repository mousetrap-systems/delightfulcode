namespace DelightfulCode
{
    /// <summary>
    /// An extension for entity framework, avoids triggering warning 'warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]'
    /// 'No type was specified for the decimal column 'SomeDecimalProperty' on entity type 'SomeClassObject'.
    /// This will cause values to be silently truncated if they do not fit in the default precision and scale.
    /// Explicitly specify the SQL server column type that can accommodate all the values using 'ForHasColumnType()'.'
    /// Usage EXAMPLE: [DecimalPrecision(10,2)]
    /// </summary>
    [Author("Warren James", 2024)]
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [Health(CodeStability.RequiresExpertReview)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }

        public byte Precision { get; set; }

        public byte Scale { get; set; }
    }
}
