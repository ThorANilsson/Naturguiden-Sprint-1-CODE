import { checkAuth } from "@/lib/checkAuth";
import ClientPage from "./ClientPage";
import { redirect } from "next/navigation";

export default async function AuthPage() {
  let isAuthenticated = false;

  try {
    const authenticated = await checkAuth();

    if (authenticated) {
      isAuthenticated = true;
    }
  } catch (error: any) {
    console.error("Error during authentication check:", error.message);
  }

  // Redirect outside the try...catch block
  if (isAuthenticated) {
    redirect(`/`);
  } else {
    return <ClientPage />;
  }
}
