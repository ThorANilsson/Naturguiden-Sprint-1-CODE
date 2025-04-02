"use client";

import { useState } from "react";
import { NatureSpot } from "@/types/NatureSpot";
import Link from "next/link";
import { Search, TreePine } from "lucide-react";
import { Input } from "@/components/ui/input";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";

export default function Home({ natureSpots }: { natureSpots: NatureSpot[] }) {
  const [searchTerm, setSearchTerm] = useState("");

  const filteredSpots = natureSpots.filter(
    (spot) =>
      spot.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      spot.description?.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <main className="container mx-auto py-8 px-4">
      <div className="text-center mb-10">
        <div className="flex flex-row items-center justify-center gap-2">
          <h1 className="text-4xl font-bold mb-1">NaturGuiden</h1>
          <TreePine size={40} color="green" />
        </div>
        <p className="text-muted-foreground max-w-2xl mx-auto">
          Samling av naturplatser i Skåne
        </p>
      </div>

      <div className="relative max-w-lg mx-auto mb-10">
        <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
        <Input
          type="text"
          placeholder="Sök"
          className="pl-10"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>

      {filteredSpots.length === 0 ? (
        <div className="text-center py-10">
          <p className="text-muted-foreground">Inga naturplatser hittades.</p>
        </div>
      ) : (
        <div className="flex flex-col space-y-4 max-w-3xl mx-auto">
          {filteredSpots.map((spot) => (
            <Link href={`/naturplats/${spot.id}`} key={spot.id}>
              <Card className="w-full gap-0 hover:border-primary transition">
                <CardHeader>
                  <CardTitle className="text-xl">{spot.name}</CardTitle>
                  {/*                   <CardDescription>
                    {spot.created_at.toLocaleString()}
                  </CardDescription> */}
                </CardHeader>
                <CardContent>
                  <p className="text-muted-foreground mb-4">
                    {spot.description}
                  </p>
                  <p className="text-sm mb-4">
                    <span className="font-medium">Koordinater: </span>
                    {spot.latitude}, {spot.longitude}
                  </p>
                </CardContent>
                <CardFooter>
                  <div className="flex flex-wrap gap-2">
                    {spot.hasElectricity && <Badge variant="default">El</Badge>}
                    {spot.hasWater && <Badge variant="default">Vatten</Badge>}
                    {spot.hasToilets && (
                      <Badge variant="default">Toaletter</Badge>
                    )}
                    {spot.hasCarParking && (
                      <Badge variant="default">Bilparkering</Badge>
                    )}
                  </div>
                </CardFooter>
              </Card>
            </Link>
          ))}
        </div>
      )}
    </main>
  );
}
