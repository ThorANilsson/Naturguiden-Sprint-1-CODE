"use client";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Check } from "lucide-react";
import { useState } from "react";
const apiUrl = process.env.NEXT_PUBLIC_API_URL;

export default function Visited({
  visited,
  placeId,
}: {
  visited: boolean;
  placeId: number;
}) {
  const [userHasVisited, setUserHasVisited] = useState(visited);

  async function markAsVisited(): Promise<boolean> {
    try {
      const response = await fetch(`${apiUrl}/naturespots/mark-as-visited`, {
        cache: "no-cache",
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          placeId: placeId,
        }),
      });

      if (!response.ok) {
        return false;
      }

      return true;
    } catch (error: any) {
      console.log(error.message);
      return false;
    }
  }

  async function handleMarkAsVisitedClick() {
    const success = await markAsVisited();
    if (success) {
      setUserHasVisited(true);
    } else {
      alert("Could not mark as visited");
    }
  }

  if (userHasVisited) {
    return (
      <Badge className="h-9 w-25 text-sm gap-2 bg-primary/60">
        <Check />
        Besökt
      </Badge>
    );
  } else {
    return (
      <Button onClick={handleMarkAsVisitedClick}>Markera som besökt</Button>
    );
  }
}
