using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISubtitleService
    {
        Domain.Models.Subtitle GetByID(Guid ID);
    }
}
