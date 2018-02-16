using System;
namespace Levenshtein
{
    [Flags]
    public enum ELevDirection
    {
        None = 0,
        UpLeft = 1,
        Up = 2,
        Left=4
    }
}
