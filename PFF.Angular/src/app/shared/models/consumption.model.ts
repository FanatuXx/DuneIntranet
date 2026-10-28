import { ConsumptionFrequency } from "../enums/consumption-frequency.enum";
import { DrugType } from "../enums/drug-type.enum";

export interface Consumption {
    id: number,
    drugType: keyof typeof DrugType | null,
    frequency: keyof typeof ConsumptionFrequency | null,
}