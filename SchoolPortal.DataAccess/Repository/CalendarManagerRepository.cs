using SchoolPortal.DataAccess.Data;
using SchoolPortal.DataAccess.Repository.IRepository;
using SchoolPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal.DataAccess.Repository
{
    public class CalendarManagerRepository : Repository<CalendarManager>, ICalendarManagerRepository
    {
        private readonly ApplicationDbContext _db;
        public CalendarManagerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CalendarManager obj)
        {
            _db.CalendarManager.Update(obj);
        }
    }
}
