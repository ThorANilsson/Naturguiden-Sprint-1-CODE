"use client";

import { ArrowLeft, MapPin, TreePine } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import Link from "next/link";
import { format } from "date-fns";
import { NatureSpot } from "@/types/NatureSpot";
import Visited from "./Visited";

export default function NatureSpotDetail({
  natureSpot,
  userHasVisited,
}: {
  natureSpot: NatureSpot;
  userHasVisited?: boolean | null;
}) {
  const openInMaps = () => {
    window.open(
      `https://www.google.com/maps/search/?api=1&query=${natureSpot.latitude},${natureSpot.longitude}`,
      "_blank"
    );
  };

  return (
    <main className="container mx-auto py-8 px-4">
      <Link href="/">
        <Button
          variant="ghost"
          className="mb-6 pl-2 flex items-center gap-2 hover:cursor-pointer"
        >
          <ArrowLeft size={16} />
          Tillbaka till listan
        </Button>
      </Link>

      <div className="max-w-3xl mx-auto">
        <div className="flex items-center mb-6 justify-between">
          <div className="flex gap-3 items-center flex-row">
            <TreePine size={32} color="green" />
            <h1 className="text-3xl font-bold">{natureSpot.name}</h1>
          </div>
          {userHasVisited != null ? (
            <Visited visited={userHasVisited} placeId={natureSpot.id} />
          ) : null}
        </div>

        <Card className="gap-1">
          <CardHeader>
            <CardTitle className="text-xl">Om platsen</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-muted-foreground mb-6">
              {natureSpot.description}
            </p>

            <div className="flex flex-col gap-6">
              <div>
                <h3 className="font-medium mb-2">Bekvämligheter</h3>
                <div className="flex flex-wrap gap-2">
                  {natureSpot.hasElectricity && (
                    <Badge variant="default">El</Badge>
                  )}
                  {natureSpot.hasWater && (
                    <Badge variant="default">Vatten</Badge>
                  )}
                  {natureSpot.hasToilets && (
                    <Badge variant="default">Toaletter</Badge>
                  )}
                  {natureSpot.hasCarParking && (
                    <Badge variant="default">Bilparkering</Badge>
                  )}
                  {!natureSpot.hasElectricity &&
                    !natureSpot.hasWater &&
                    !natureSpot.hasToilets &&
                    !natureSpot.hasCarParking && (
                      <span className="text-muted-foreground">
                        Inga bekvämligheter tillgängliga
                      </span>
                    )}
                </div>
              </div>

              <Separator />

              <div>
                <h3 className="font-medium mb-2">Plats</h3>
                <div className="flex items-center gap-2 mb-4">
                  <MapPin size={16} className="text-muted-foreground" />
                  <span>
                    {natureSpot.latitude}, {natureSpot.longitude}
                  </span>
                </div>
                <Button
                  onClick={openInMaps}
                  className="w-full sm:w-auto hover:cursor-pointer"
                >
                  Öppna i Google Maps
                </Button>
              </div>

              <Separator />

              <div>
                <h3 className="font-medium mb-2">Tillagd</h3>
                <p className="text-muted-foreground">
                  {format(new Date(natureSpot.createdAt), "d MMMM yyyy")}
                </p>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </main>
  );
}
