SELECT contract."CarNumber"
FROM "Contracts" AS contract
GROUP BY contract."CarNumber"
HAVING COUNT(DISTINCT contract."ClientId") > 1
