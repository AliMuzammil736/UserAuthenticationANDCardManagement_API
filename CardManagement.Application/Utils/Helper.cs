using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardManagement.Application.Utils
{
    public class Helper
    {

        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".gif", ".tif", ".tiff", ".png", ".bmp" };

        public bool IsValidFileExtension(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return _allowedExtensions.Contains(fileExtension);
        }

        public string CleanExtraction(string text)
        {
            return Regex.Replace(text, @"[^a-zA-Z0-9/\-\s]", string.Empty);
        }

        public string ExtractExpiryDate(string text)
        {
            string[] FieldArray = { "Expiry Date", "ExpiryDate" };
            string format = "dd/MM/yyyy";
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            bool keyFound = false;

            foreach (var line in lines)
            {
                if (FieldArray.Any(key => line.Contains(key, StringComparison.OrdinalIgnoreCase)))
                {
                    keyFound = true;
                }
            }

            if (keyFound)
            {
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, @"(\d{2}/\d{2}/\d{4})");
                    if (match.Success)
                    {
                        var dateString = match.Groups[1].Value;
                        if (DateTime.ParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture) > DateTime.Now)
                        {
                            return dateString;
                        }
                    }
                }
            }

            return string.Empty;
        }

        public string ExtractEmiratesID(string text)
        {
            string[] FieldArray = { "1D Number", "Number", "card Number" };
            var lines = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                if (FieldArray.Any(key => lines[i].Contains(key, StringComparison.OrdinalIgnoreCase)))
                {

                    var IdNumber = ValidateLinebyParts(lines[i]);

                    if (string.IsNullOrEmpty(IdNumber) && i > 0)
                    {
                        var previousLine = lines[i - 1];
                        IdNumber = ValidateLinebyParts(previousLine);
                    }

                    if (string.IsNullOrEmpty(IdNumber) && i < lines.Length - 1)
                    {
                        var nextLine = lines[i + 1];
                        IdNumber = ValidateLinebyParts(nextLine);
                    }

                    return IdNumber;
                }
            }

            return string.Empty;
        }

        public string ValidateLinebyParts(string Line)
        {
            var parts = Line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (Regex.IsMatch(part, @"^\d{3}-\d{4}-\d{7}-\d{1}$"))
                {
                    return part;
                }
            }

            return string.Empty;
        }

    }
}
