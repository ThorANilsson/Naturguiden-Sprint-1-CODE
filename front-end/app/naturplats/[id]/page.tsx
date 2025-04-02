import { NatureSpot } from "@/types/NatureSpot";
import ClientPage from "./ClientPage";
import { getSessionCookie } from "@/lib/checkAuth";
const apiUrl = process.env.NEXT_PUBLIC_API_URL;

interface NatureSpotReponse {
  natureSpot: NatureSpot;
  userHasVisited?: boolean;
}

async function getNatureSpot(id: string): Promise<NatureSpotReponse | null> {
  try {
    const cookie = await getSessionCookie();
    const response = await fetch(`${apiUrl}/naturespots/${id}`, {
      cache: "no-cache",
      method: "GET",
      headers: {
        Cookie: cookie ? `${cookie.name}=${cookie.value}` : "",
      },
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

export default async function ViewNatureSpot({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  try {
    const natureSpot = await getNatureSpot(id);

    if (natureSpot) {
      return (
        <ClientPage
          natureSpot={natureSpot.natureSpot}
          userHasVisited={natureSpot.userHasVisited}
        />
      );
    }

    return <p>ERROR!</p>;
  } catch (error: any) {
    console.error("Error fetching courts:", error);
    return <p>ERROR! (catch)</p>;
  }
}
