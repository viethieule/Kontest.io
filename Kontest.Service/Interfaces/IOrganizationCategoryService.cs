﻿using Kontest.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Service.Interfaces
{
    public interface IOrganizationCategoryService
    {
        List<OrganizationCategoryViewModel> GetAll();
    }
}
