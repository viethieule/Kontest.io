using System;
using System.Collections.Generic;
using System.Linq;
using Kontest.Model.Entities;
using System.Threading.Tasks;
using Kontest.Model.Enums;

namespace Kontest.Data
{
    public class DbInit
    {
        private readonly KontestDbContext _context;

        public DbInit(KontestDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Organizations.Any())
            {
                _context.AddRange(new List<Organization>
                {
                    new Organization { Name = "CLB Tin học", Alias = "clb-tin-hoc", ProfilePicture="https://picsum.photos/199" },
                    new Organization { Name = "CLB Tài chính", Alias = "clb-tai-chinh", ProfilePicture="https://picsum.photos/200" },
                    new Organization { Name = "CLB Nhân sự", Alias = "clb-nhan-su", ProfilePicture="https://picsum.photos/201" },
                });
            }

            _context.SaveChanges();

            if (!_context.UserOrganizations.Any())
            {
                var alice = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == "alice");
                var bob = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == "bob");
                var david = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == "wtfdavid");
                var thao = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == "thao");

                var clbTinHoc = _context.Organizations.FirstOrDefault(o => o.Alias == "clb-tin-hoc");
                var clbTaiChinh = _context.Organizations.FirstOrDefault(o => o.Alias == "clb-tai-chinh");
                var clbNhanSu = _context.Organizations.FirstOrDefault(o => o.Alias == "clb-nhan-su");

                _context.UserOrganizations.AddRange(new List<UserOrganization>
                {
                    new UserOrganization { OrganizationId = clbTinHoc.Id, UserId = alice.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Creator, AssignedBy = "SYSTEM", AssignedDate = DateTime.Now },
                    new UserOrganization { OrganizationId = clbTinHoc.Id, UserId = bob.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Admin, AssignedBy = "Alice", AssignedDate = DateTime.Now.AddDays(3) },
                    new UserOrganization { OrganizationId = clbTinHoc.Id, UserId = david.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Admin, AssignedBy = "Bob", AssignedDate = DateTime.Now.AddDays(5) },
                    new UserOrganization { OrganizationId = clbTaiChinh.Id, UserId = bob.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Creator, AssignedBy = "SYSTEM", AssignedDate = DateTime.Now.AddDays(-3) },
                    new UserOrganization { OrganizationId = clbNhanSu.Id, UserId = thao.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Creator, AssignedBy = "SYSTEM", AssignedDate = DateTime.Now.AddDays(-10) },
                    new UserOrganization { OrganizationId = clbNhanSu.Id, UserId = alice.Id, OrgnizationUserRoleType = OrgnizationUserRoleType.Creator, AssignedBy = "thao", AssignedDate = DateTime.Now.AddDays(-5) },
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
