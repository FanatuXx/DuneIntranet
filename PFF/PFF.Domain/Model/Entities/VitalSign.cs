using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Model.Entities
{
    public class VitalSign
    {
        public int Id { get; set; }
        public int HeartRate { get; set; }
        public string BloodPressure { get; set; }
        public decimal Temperature { get; set; }
        public int OxygenSaturation { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
        public float BloodSugar { get; set; }
        public int ConsultationId { get; set; }
        public virtual Consultation Consultation { get; set; }


        public float BMICalculator()
        {
            return BMI = Weight / (Height * Height);
        }
    }
}
