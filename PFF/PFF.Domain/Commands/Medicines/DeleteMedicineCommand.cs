using PFF.Tools.CommandQuerySeparation;

namespace PFF.Domain.Commands.Medicines
{
    public record DeleteMedicineCommand(int Id) : ICommandDefinition
    {
    }
}
