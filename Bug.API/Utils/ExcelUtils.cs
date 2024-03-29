﻿using Bug.Entities.Model;
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
            workSheet.Cells[2, 1].Value = issue.Title;
            workSheet.Cells[1, 2].Value = "Status";
            workSheet.Cells[2, 2].Value = issue.Status?.Name;
            workSheet.Cells[1, 3].Value = "Project";
            workSheet.Cells[2, 3].Value = issue.Project?.Name;
            workSheet.Cells[1, 4].Value = "Code";
            workSheet.Cells[2, 4].Value = issue.Code;
            workSheet.Cells[1, 5].Value = "Created Date";
            workSheet.Cells[2, 5].Value = issue.CreatedDate;
            workSheet.Cells[1, 6].Value = "Due Date";
            workSheet.Cells[2, 6].Value = issue.DueDate;
            workSheet.Cells[1, 7].Value = "Origin Estimate Time";
            workSheet.Cells[2, 7].Value = issue.OriginEstimateTime;
            workSheet.Cells[1, 8].Value = "Remain Estimate Time";
            workSheet.Cells[2, 8].Value = issue.RemainEstimateTime;
            workSheet.Cells[1, 9].Value = "Environtment";
            workSheet.Cells[2, 9].Value = issue.Environment;
            workSheet.Cells[1, 10].Value = "Description";
            workSheet.Cells[2, 10].Value = issue.Description;
            workSheet.Cells[1, 11].Value = "Severity";
            workSheet.Cells[2, 11].Value = issue.Severity?.Name;
            workSheet.Cells[1, 12].Value = "Priority";
            workSheet.Cells[2, 12].Value = issue.Priority?.Name;
            workSheet.Cells[1, 13].Value = "Reporter";
            workSheet.Cells[2, 13].Value = issue.Reporter?.FullName;
            workSheet.Cells[1, 14].Value = "Assignee";
            workSheet.Cells[2, 14].Value = issue.Assignee?.FullName;
            BindingFormatForExcel(workSheet, issue);
            await excelPackage.SaveAsync();

            return excelPackage.Stream;
        }
        
        private void BindingFormatForExcel(ExcelWorksheet worksheet, Issue issue)
        {
            // Set default width cho tất cả column
            worksheet.Column(1).Width = 20;
            worksheet.Column(5).Width = 30;
            worksheet.Column(6).Width = 30;
            worksheet.Column(7).Width = 38;
            worksheet.Column(8).Width = 25;
            worksheet.Column(9).Width = 30;
            worksheet.Column(10).Width = 30;
            worksheet.Column(2).Width = 15;
            worksheet.Column(3).Width = 10;
            worksheet.Column(4).Width = 10;
            worksheet.Column(11).Width = 10;
            worksheet.Column(12).Width = 10;
            worksheet.Column(13).Width = 10;
            worksheet.Column(14).Width = 10;
           
            // worksheet.DefaultColWidth = 30;
            // worksheet.Cells["A2:H2"].AutoFitColumns();
            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;

            //Show full details for Title of issue
            int count_Issue = issue.Title != null ? issue.Title.Length : 0;
            int numberMergeOfIssue = count_Issue / 12 ;
            int finalMergeIssue = numberMergeOfIssue +2;
            string ColumnMergeIssue = "A" + finalMergeIssue;
            if (numberMergeOfIssue > 1)
            {
                worksheet.Cells["A2:" + ColumnMergeIssue].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["A2:" + ColumnMergeIssue].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }
            //Show full details for Status of issue
            int count_Status = issue.Status != null ? issue.Status.Name.Length : 0;
            int numberMergeOfStatus = count_Status / 6;
            int finalMergeStatus = numberMergeOfStatus +2;
            string ColumnMergeStatus = "B" + finalMergeStatus;
            if (numberMergeOfStatus > 1)
            {
                worksheet.Cells["B2:" + ColumnMergeStatus].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["B2:" + ColumnMergeStatus].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }
            //Show full details for Code of issue
            int count_Code = issue.Code != null ? issue.Code.Length : 0;
            int numberMergeOfCode = count_Code / 6 + 2;
            string ColumnMergeCode = "D" + numberMergeOfCode;
            if (numberMergeOfCode > 1)
            {
                worksheet.Cells["D2:" + ColumnMergeCode].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["D2:" + ColumnMergeCode].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }
            //Show full details for Project of issue
            int count_Project = issue.Project.Name != null ? issue.Project.Name.Length : 0;
            int numberMergeOfProject = count_Project / 6;
            int finalMergeProject = numberMergeOfProject +2;
            string ColumnMergeProject = "C" + finalMergeProject;
            if (numberMergeOfProject > 1)
            {
                worksheet.Cells["C2:" + ColumnMergeProject].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["C2:" + ColumnMergeProject].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }

            //Show full details for description
            int count_Description = issue.Description != null ? issue.Description.Length : 0;
            int numberMergeOfDes = count_Description / 18;
            int finalMergeDes = numberMergeOfDes +2;
            string ColumnMergeOfDes = "J" + finalMergeDes;
            if (numberMergeOfDes > 1)
            {
                worksheet.Cells["J2:" + ColumnMergeOfDes].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["J2:" + ColumnMergeOfDes].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }

            //Show full details for Environment
            int count_Enviroment = issue.Environment != null ? issue.Environment.Length : 0;
            int numberMergeOfEnvi = count_Enviroment / 18;
            int finalMergeEnvi = numberMergeOfEnvi =2;
            string ColumnMergeEnvi = "I" + finalMergeEnvi;
            if (numberMergeOfEnvi > 1)
            {
                worksheet.Cells["I2:" + ColumnMergeEnvi].Merge = true;
                worksheet.Cells.Style.WrapText = true;
                worksheet.Cells["I2:" + ColumnMergeEnvi].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            }

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:N1"])
            {
                // worksheet.Column(7).Width = 38;
                // range.AutoFitColumns();
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
