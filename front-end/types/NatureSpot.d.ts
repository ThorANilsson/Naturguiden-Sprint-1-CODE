export interface NatureSpot {
  id: number;
  name: string;
  description?: string;
  latitude: number;
  longitude: number;
  hasElectricity: boolean;
  hasWater: boolean;
  hasToilets: boolean;
  hasCarParking: boolean;
  createdAt: Date;
}
