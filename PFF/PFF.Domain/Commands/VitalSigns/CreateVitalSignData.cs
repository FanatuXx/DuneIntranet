using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Commands.VitalSigns
{
    public class CreateVitalSignData : ICommandDefinition
    {
        public int HeartRate { get; init; }
        public string? BloodPressure { get; init; }
        public decimal? Temperature { get; init; }
        public int OxygenSaturation { get; init; }
        public int? Height { get; init; }
        public float? Weight { get; init; }
        public float BMI { get; set; }
        public float? BloodSugar { get; init; }
        public DateTime Date { get; init; }

        public CreateVitalSignData(
            int heartRate,
            string? bloodPressure,
            decimal? temperature,
            int oxygenSaturation,
            int? height,
            float? weight,
            float? bloodSugar,
            DateTime date)
        {
            HeartRate = heartRate;
            BloodPressure = bloodPressure;
            Temperature = temperature;
            OxygenSaturation = oxygenSaturation;
            Height = height;
            Weight = weight;
            BloodSugar = bloodSugar;
            Date = date;
        }
        public float BMICalculator()
        {
            if (Height is not null && Weight is not null)
            {
                float height = (float)Height / 100;
                BMI = (float)(Weight / (height * height));
                return BMI;
            }
            return -1;
        }
    }
}
