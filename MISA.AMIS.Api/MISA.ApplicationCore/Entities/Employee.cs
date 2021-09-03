using MISA.ApplicationCore.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính EmployeeId
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MISARequired("Mã nhân viên")]

        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên lót
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Tên họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [MISARequired("Họ tên")]

        public string FullName { get; set; }

        /// <summary>
        /// Giới tính (int)
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Giới tính (string)
        /// </summary>
        [MISANotMap]
        public string GenderName { get; set; }

        /// <summary>
        /// Email
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>

        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string LandlinePhoneNumber { get; set; }

        /// <summary>
        /// Số CMTND/Căn cước
        /// </summary>

        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp căn cước
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        [MISARequired("Đơn vị")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        [MISANotMap]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string BankPlace { get; set; }
        #endregion
    }
}
