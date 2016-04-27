// Sagi Shoffer & Oren Yulzary

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Client.UserControls
{
    class CharCellDataInfoValidationRule : ValidationRule
    {
        private bool allowNull;

        public CharCellDataInfoValidationRule(bool allowNull = false)
        {
            this.allowNull = allowNull;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg = new Regex("^[A-Z][a-z]*$");

            String str = value.ToString();

            if (str.Length == 0 && !allowNull)
            {
                return new ValidationResult(false, "The value can not be empty");
            }
            else if (!reg.IsMatch(str) && str.Length != 0)
            {
                return new ValidationResult(false, "'" + value.ToString() + "' is not a whole chars.");
            }
            else
                return new ValidationResult(true, null);
        }
    }

    class StringCellDataInfoValidationRule : ValidationRule
    {
        private bool allowNull;

        public StringCellDataInfoValidationRule(bool allowNull = false)
        {
            this.allowNull = allowNull;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg = new Regex("^[A-Z][a-z]*([ ][A-Z][a-z]*)*$");

            String str = value.ToString();

            if (str.Length == 0 && !allowNull)
            {
                return new ValidationResult(false, "The value can not be empty");
            }
            else if (!reg.IsMatch(str) && str.Length != 0)
            {
                return new ValidationResult(false, "'" + value.ToString() + "' is not a whole chars.");
            }
            else
                return new ValidationResult(true, null);
        }
    }

    class IntCellDataInfoValidationRule : ValidationRule
    {
        private bool allowNull;

        public IntCellDataInfoValidationRule(bool allowNull = false)
        {
            this.allowNull = allowNull;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int proposedValue;
                if (!int.TryParse(value.ToString(), out proposedValue))
                {
                    return new ValidationResult(false, "'" + value.ToString() + "' is not a whole number.");
                }
            }
            return new ValidationResult(true, null);
        }
    }

    class CharIntCellDataInfoValidationRule : ValidationRule
    {
        private bool allowNull;

        public CharIntCellDataInfoValidationRule(bool allowNull = false)
        {
            this.allowNull = allowNull;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg = new Regex("^[a-zA-Z]+[a-zA-Z0-9]*$");

            String str = value.ToString();

            if (str.Length == 0 && !allowNull)
            {
                return new ValidationResult(false, "The value can not be empty");
            }
            else if (!reg.IsMatch(str) && str.Length != 0)
            {
                return new ValidationResult(false, "The value must start with char");
            }
            else
                return new ValidationResult(true, null);
        }
    }

    class DateCellDataInfoValidationRule : ValidationRule
    {
        private bool allowNull;

        public DateCellDataInfoValidationRule(bool allowNull = false)
        {
            this.allowNull = allowNull;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //TODO Regex For Date
            Regex reg = new Regex("^[0-3][0-9]/[01][0-9]/[0-2][0-9]{3}");

            String str = value.ToString();

            if (str.Length == 0 && !allowNull)
            {
                return new ValidationResult(false, "The value can not be empty");
            }
            else if (!reg.IsMatch(str) && str.Length != 0)
            {
                return new ValidationResult(false, "The value must be in the format: dd/MM/yyyy hh:mm:ss");
            }
            else
                return new ValidationResult(true, null);
        }
    }
}
