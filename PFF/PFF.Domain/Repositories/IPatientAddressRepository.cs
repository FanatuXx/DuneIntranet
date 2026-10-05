using PFF.Domain.Commands.PatientAddress;
using PFF.Domain.Model.Entities;
using PFF.Tools.CommandQuerySeparation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFF.Domain.Repositories
{
    public interface IPatientAddressRepository : 
        ICommandHandler<CreatePatientAddressCommand>
    {
    }
}
