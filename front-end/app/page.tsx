import { NatureSpot } from "@/types/NatureSpot";
import ClientPage from "./ClientPage";
const apiUrl = process.env.NEXT_PUBLIC_API_URL;

interface NatureSpotsResponse {
  success: boolean;
  natureSpots: NatureSpot[];
}

async function getNatureSpots(): Promise<NatureSpot[] | null> {
  try {
    const response = await fetch(`${apiUrl}/naturespots`, {
      cache: "no-cache",
      method: "GET",
    });

    if (!response.ok) {
      return null;
    }

    const json = await response.json();
    return json;
  } catch (error: any) {
    console.log(error.message);
    return null;
  }
}

export default async function Home() {
  try {
    const natureSpots = await getNatureSpots();

    if (natureSpots) {
      return <ClientPage natureSpots={natureSpots} />;
    }

    return <p>ERROR!</p>;
  } catch (error: any) {
    console.error("Error fetching courts:", error);
    return <p>ERROR! (catch)</p>;
  }
}
