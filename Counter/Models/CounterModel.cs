using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Counter.Models
{
    public class CounterModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int InitialValue { get; set; }
        public string ColorString { get; set; }

        [JsonIgnore]
        public Color Color {
            get => Color.FromArgb(ColorString);
            set => ColorString = value.ToHex();
        }

        public CounterModel() 
        {
            Value = 0;
            InitialValue = 0;
        }

        public CounterModel(int _Value)
        {
            Value = _Value;
            InitialValue = _Value;
        }

        public CounterModel(string _Name, Color _Color, int _InitialValue)
        {
            Name = _Name;
            Color = _Color;
            InitialValue = _InitialValue;
            Value = _InitialValue;
        }

        public void Reset()
        {
            Value = InitialValue;
        }

    }
}
