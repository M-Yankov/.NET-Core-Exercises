﻿namespace CameraBazar.Data.Enums
{
    using System;

    [Flags]
    public enum LightMetering
    {
        None = 0,
        Spot = 1,
        CenterWeighted = 2,
        Evaluative = 4,
    }
}
