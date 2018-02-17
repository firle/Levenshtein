using System;
namespace Levenshtein
{
    public class LevField
    {
        public int? Value { get; set; }

        public ELevDirection Direction {get; set; }

        public LevField(int? value = null)
        {
            Value = value;
        }

        public LevField((int?,ELevDirection) value)
        {
            Value = value.Item1;
            Direction = value.Item2;
        }

        public static implicit operator int?(LevField field)
        {
            return field?.Value;
        }
        public static implicit operator int (LevField field)
        {
            return field?.Value??-1;
        }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}
