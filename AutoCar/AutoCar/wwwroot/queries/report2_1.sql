SELECT contract."CarNumber"
FROM "Contracts" AS contract LEFT JOIN "Cars" car ON car."Number" = contract."CarNumber"
GROUP BY contract."CarNumber"
HAVING COUNT(DISTINCT contract."ClientId") > 1
