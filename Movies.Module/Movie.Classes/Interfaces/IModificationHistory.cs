﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Classes.Interfaces
{
    public interface IModificationHistory
    {
        DateTime DateModified { get; set; }

        DateTime DateAdded { get; set; }

        bool IsDirty { get; set; }
    }
}
