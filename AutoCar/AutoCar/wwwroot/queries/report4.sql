SELECT SUM((SELECT SUM(PS."Price")
 FROM "Contracts" AS CT LEFT JOIN "ParkingSeats" PS on PS."Id" = CT."SeatId"
WHERE contract."Id" = CT."Id" AND CT."AccuralDate" BETWEEN @StartInterval AND @EndInterval
GROUP BY CT."CarNumber") -
       (SELECT SUM(CT."PaymentAmount")
  FROM "Contracts" AS CT
  WHERE contract."Id" = CT."Id" AND CT."PaymentDate" BETWEEN @StartInterval AND @EndInterval
  GROUP BY CT."CarNumber")) AS "TotalDebt"
FROM "Contracts" AS contract;
