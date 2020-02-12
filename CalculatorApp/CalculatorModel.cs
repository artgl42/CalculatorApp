using System;

namespace CalculatorApp
{
    public class CalculatorModel
    {
        private string _LeftOperand { get; set; }
        private string _RightOperand { get; set; }
        private string _Operation { get; set; }

        public CalculatorModel()
        {
            _LeftOperand = _Operation = _RightOperand = string.Empty;
        }

        public string GetDisplayText()
        {
            return _LeftOperand + _Operation + _RightOperand;
        }

        public void SetDisplayText(string displayText)
        {

            if (decimal.TryParse(displayText, out decimal number) || displayText == ",")
            {
                SetOperand(displayText);
            }
            else
            {
                SetOperation(displayText);
            }
        }

        private void SetOperand(string displayText)
        {
            if (string.IsNullOrEmpty(_LeftOperand) || string.IsNullOrEmpty(_Operation))
            {
                _LeftOperand += (displayText != "," || _LeftOperand.IndexOf(',') == -1) ? displayText : string.Empty;
            }
            else
            {
                _RightOperand += (displayText != "," || _RightOperand.IndexOf(',') == -1) ? displayText : string.Empty;
            }
        }

        private void SetOperation(string displayText)
        {
            switch (displayText)
            {
                case "1/X":
                    if (!string.IsNullOrEmpty(_LeftOperand) && string.IsNullOrEmpty(_RightOperand))
                    {
                        _Operation = displayText;
                        _RightOperand = "0";
                        GetResult();
                    }
                    break;
                case "Sqrt":
                    if (!string.IsNullOrEmpty(_LeftOperand) && string.IsNullOrEmpty(_RightOperand))
                    {
                        _Operation = displayText;
                        _RightOperand = "0";
                        GetResult();
                    }
                    break;
                case "=":
                    if (!string.IsNullOrEmpty(_LeftOperand) && !string.IsNullOrEmpty(_Operation) && !string.IsNullOrEmpty(_RightOperand))
                    {
                        GetResult();
                    }
                    break;
                case "C":
                    _LeftOperand = _Operation = _RightOperand = string.Empty;
                    break;
                default:
                    if (!string.IsNullOrEmpty(_RightOperand))
                    {
                        GetResult();
                    }
                    _Operation = !string.IsNullOrEmpty(_LeftOperand) ? displayText : default(string);
                    break;
            }
        }

        private void GetResult()
        {
            if (decimal.TryParse(_LeftOperand, out decimal _leftNumber) && decimal.TryParse(_RightOperand, out decimal _rightNumber))
            {
                switch (_Operation)
                {
                    case "+":
                        _LeftOperand = (_leftNumber + _rightNumber).ToString();
                        break;
                    case "-":
                        _LeftOperand = (_leftNumber - _rightNumber).ToString();
                        break;
                    case "*":
                        _LeftOperand = Math.Round(_leftNumber * _rightNumber, 28).ToString();
                        break;
                    case "/":
                        _LeftOperand = _rightNumber != 0 ? Math.Round(_leftNumber / _rightNumber, 28).ToString() : "WTF?";
                        break;
                    case "^":
                        _LeftOperand = Math.Pow((double)_leftNumber, (double)_rightNumber).ToString();
                        break;
                    case "Sqrt":
                        _LeftOperand = Math.Sqrt((double)_leftNumber).ToString();
                        break;
                    case "1/X":
                        _LeftOperand = _leftNumber != 0 ? (1 / _leftNumber).ToString() : "WTF?";
                        break;
                }
            }

            _LeftOperand = TrimOfZeros(_LeftOperand);
            _Operation = _RightOperand = string.Empty;

            string TrimOfZeros(string textNumber)
            {
                int indx = textNumber.IndexOf(',');
                if (indx > -1)
                {
                    var subStr1 = textNumber.Substring(0, indx);
                    var subStr2 = textNumber.Substring(indx + 1);
                    subStr1 = subStr1.Length > 1 ? subStr1.TrimStart('0') : subStr1;
                    subStr2 = subStr2.TrimEnd('0');
                    textNumber = string.IsNullOrEmpty(subStr2) ? subStr1 : subStr1 + ',' + subStr2;
                }
                return textNumber;
            }
        }
    }
}
