﻿using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository.IRepository
{
    public interface IAdminAnnouncementRepository : IRepository<AdminAnnouncement>
    {
        void Update(AdminAnnouncement obj);
    }
}
