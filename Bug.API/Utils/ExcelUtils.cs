using Bug.Entities.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Utils
{
    public class ExcelUtils
    {
        public async Task<Stream> CreateExcelFile
            (Issue issue,
            Stream stream = null)
        {
            if (issue == null)
                return null;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var excelPackage = new ExcelPackage(stream ?? new MemoryStream());
            // Tạo author cho file Excel
            excelPackage.Workbook.Properties.Author = "Hanker";
            // Tạo title cho file Excel
            excelPackage.Workbook.Properties.Title = "EPP test background";
            // thêm tí comments vào làm màu 
            excelPackage.Workbook.Properties.Comments = "This is my generated Comments";
            // Add Sheet vào file Excel
            excelPackage.Workbook.Worksheets.Add(issue.Title);
            // Lấy Sheet bạn vừa mới tạo ra để thao tác 
            var workSheet = excelPackage.Workbook.Worksheets[0];
            // Đổ data vào Excel file
            workSheet.Cells[1, 1].Value = "Title";
            workSheet.Cells[1, 2].Value = issue.Title;
            workSheet.Cells[2, 1].Value = "Status";
            workSheet.Cells[2, 2].Value = issue.Status?.Name;
            workSheet.Cells[3, 1].Value = "Project";
            workSheet.Cells[3, 2].Value = issue.Project?.Name;
            workSheet.Cells[4, 1].Value = "Code";
            workSheet.Cells[4, 2].Value = issue.Code;
            workSheet.Cells[5, 1].Value = "Created Date";
            workSheet.Cells[5, 2].Value = issue.CreatedDate;
            workSheet.Cells[6, 1].Value = "Due Date";
            workSheet.Cells[6, 2].Value = issue.DueDate;
            workSheet.Cells[7, 1].Value = "Origin Estimate Time";
            workSheet.Cells[7, 2].Value = issue.OriginEstimateTime;
            workSheet.Cells[8, 1].Value = "Remain Estimate Time";
            workSheet.Cells[8, 2].Value = issue.RemainEstimateTime;
            workSheet.Cells[9, 1].Value = "Environtment";
            workSheet.Cells[9, 2].Value = issue.Environment;
            workSheet.Cells[10, 1].Value = "Description";
            workSheet.Cells[10, 2].Value = issue.Description;
            workSheet.Cells[11, 1].Value = "Severity";
            workSheet.Cells[11, 2].Value = issue.Severity?.Name;
            workSheet.Cells[12, 1].Value = "Priority";
            workSheet.Cells[12, 2].Value = issue.Priority?.Name;
            workSheet.Cells[13, 1].Value = "Reporter";
            workSheet.Cells[13, 2].Value = issue.Reporter?.FullName;
            workSheet.Cells[14, 1].Value = "Assignee";
            workSheet.Cells[14, 2].Value = issue.Assignee?.FullName;
            BindingFormatForExcel(workSheet, issue);
            await excelPackage.SaveAsync();

            return excelPackage.Stream;
        }

        private void BindingFormatForExcel(ExcelWorksheet worksheet, Issue issue)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 10;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;

            
            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:A14"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 10));
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }
        }
    }
}
