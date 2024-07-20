import { DocumentationFile } from "./documentation-file";

export interface Acquisition {
  id: string;
  budget: number;
  unit: string;
  type: string;
  quantity: number;
  unitValue: number;
  totalValue: number;
  acquisitionDate: Date;
  supplier: string;
  documentation: DocumentationFile[];
}
