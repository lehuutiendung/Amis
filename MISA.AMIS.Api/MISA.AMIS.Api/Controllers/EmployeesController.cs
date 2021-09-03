using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {

        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            _employeeService = employeeService;
        }

        #region Ghi đè API thêm mới để thay đổi Resouces Msg

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public override IActionResult Add(Employee employee)
        {
            try
            {
                var serviceResult = _employeeService.Add(employee);
                if (serviceResult.IsValid == true)
                {
                    if ((int)serviceResult.Data == 400)
                    {
                        var errorObj = new
                        {
                            devMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Duplicate_ErrorMsg.Replace("{}", "nhân viên"),
                            userMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Duplicate_ErrorMsg.Replace("{}", "nhân viên"),
                            errorCode = "",
                            moreInfo = "",
                            traceId = ""
                        };
                        return StatusCode(400, errorObj);
                    }
                    return StatusCode(201, serviceResult.Data); ;
                }
                else
                {
                    var errorObj = new
                    {
                        devMsg = serviceResult.Data,
                        userMsg = serviceResult.Data,
                        errorCode = "",
                        moreInfo = "",
                        traceId = ""
                    };
                    return BadRequest(errorObj);
                }

            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Exception_ErrorMsg,
                    errorCode = "",
                    moreInfo = "",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        #endregion

        #region Ghi đè API phân trang và bộ lọc theo tên, mã nhân viên, số điện thoại

        /// <summary>
        /// Bộ lọc theo Tên, Mã, Số điện thoại
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="code">Mã nhân viên/Mã khách hàng</param>
        /// <param name="phoneNumber"></param>
        /// <returns>
        ///     totalRecord
        ///     {entity}
        /// </returns>
        [HttpGet("Filter")]
        public override IActionResult Filter(int pageSize, int pageNumber, String filter, String departmentId)
        {
            try
            {
                var _serviceResult = _employeeService.Filter(pageSize, pageNumber, filter, departmentId);
                var response = StatusCode(200, _serviceResult.Data);
                return response;
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.ApplicationCore.Resources.ResourcesCommon.Exception_ErrorMsg,
                    errorCode = "",
                    moreInfo = "",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        #endregion

        #region API Export dữ liệu nhân viên
        [HttpPost("Export")]
        public IActionResult Export(CancellationToken cancellationToken, List<Employee> employeesList)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[3, 1].Value = "STT";
                workSheet.Cells[3, 2].Value = "Mã nhân viên";
                workSheet.Cells[3, 3].Value = "Tên nhân viên";
                workSheet.Cells[3, 4].Value = "Giới tính";
                workSheet.Cells[3, 5].Value = "Ngày sinh";
                workSheet.Cells[3, 6].Value = "Số CMND";
                workSheet.Cells[3, 7].Value = "Chức danh";
                workSheet.Cells[3, 8].Value = "Tên đơn vị";
                workSheet.Cells[3, 9].Value = "Số tài khoản";
                workSheet.Cells[3, 10].Value = "Tên ngân hàng";
                workSheet.Cells[3, 11].Value = "Chi nhánh TK ngân hàng";
                workSheet.Cells[3, 12].Value = "Địa chỉ";
                workSheet.Cells[3, 13].Value = "ĐT di động";
                workSheet.Cells[3, 14].Value = "ĐT cố định";
                workSheet.Cells[3, 15].Value = "Email";

                var i = 0;
                var totalRows = employeesList.Count();

                for (int row = 4; row <= totalRows + 1; row++)
                {
                    workSheet.Cells[row, 1].Value = i+1;
                    workSheet.Cells[row, 2].Value = employeesList[i].EmployeeCode;
                    workSheet.Cells[row, 3].Value = employeesList[i].FullName;
                    workSheet.Cells[row, 4].Value = employeesList[i].GenderName;
                    workSheet.Cells[row, 5].Value = employeesList[i].DateOfBirth;
                    //custom DateTime
                    workSheet.Cells[row, 5].Style.Numberformat.Format = "dd-MM-yyyy";
                    workSheet.Cells[row, 6].Value = employeesList[i].IdentityNumber;
                    workSheet.Cells[row, 7].Value = employeesList[i].PositionName;
                    workSheet.Cells[row, 8].Value = employeesList[i].DepartmentName;
                    workSheet.Cells[row, 9].Value = employeesList[i].BankAccount;
                    workSheet.Cells[row, 10].Value = employeesList[i].BankName;
                    workSheet.Cells[row, 11].Value = employeesList[i].BankPlace;
                    workSheet.Cells[row, 12].Value = employeesList[i].Address;
                    workSheet.Cells[row, 13].Value = employeesList[i].PhoneNumber;
                    workSheet.Cells[row, 14].Value = employeesList[i].LandlinePhoneNumber;
                    workSheet.Cells[row, 15].Value = employeesList[i].Email;
                    i++;
                }
                //Merge hàng đầu tiên
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                var titleRow = workSheet.Cells[1, 1, 1, 15];
                var mergeRow2 = workSheet.Cells[2, 1, 2, 15];
                titleRow.Merge = true;
                titleRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                mergeRow2.Merge = true;

                //Đặt font bold cho tiêu đề
                titleRow.Style.Font.Bold = true;
                titleRow.Style.Font.Size = 16;

                //Đặt font bold cho hàng 2
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Cells[3, 1, 3, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 1, 3, 15].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#E8E8E8"));

                //Đặt độ rộng vừa với nội dung các cột và border
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                workSheet.Cells[3, 1, i+3, 15].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1, i+3, 15].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1, i+3, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1, i+3, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Đặt căn giữa cho các cột
                workSheet.Cells[3, 1, i+3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[3, 5, i+3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                package.Save();
            }
            stream.Position = 0;
            string excelName = "Danh sách nhân viên.xlsx";
            //string fileGuid = Guid.NewGuid().ToString();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        #endregion
    }
}
