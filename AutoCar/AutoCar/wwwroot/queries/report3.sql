SELECT car."Number", "Brand", "ReleaseYear",
((SELECT SUM(PS."Price")
 FROM "Contracts" AS contract LEFT JOIN "ParkingSeats" PS on PS."Id" = contract."SeatId"
WHERE car."Number" = contract."CarNumber" AND contract."AccuralDate" BETWEEN @StartInterval AND @EndInterval
GROUP BY contract."CarNumber") -
 (SELECT SUM(contract."PaymentAmount")
  FROM "Contracts" AS contract
  WHERE car."Number" = contract."CarNumber" AND contract."PaymentDate" BETWEEN @StartInterval AND @EndInterval
  GROUP BY contract."CarNumber")) AS "TotalDebt"
FROM "Cars" AS car
ORDER BY "TotalDebt"
LIMIT 1;
